namespace MLC.General.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MLC.General.Contract;
    using Domain =  MLC.General.Domain;
    using System.Linq;
    using System;
    using System.Linq.Expressions;
    using static MLC.Foundation.Core;
    using MLC.Foundation;
    using System.IO;

    public class AccountService : BaseService,  IAccountService 
    {
        private readonly Domain.IAccountRepository _accountsRepository;
        private readonly IEmailService _emailService;

        public AccountService(ApplicationContext appContext, Domain.IAccountRepository AccountsRepository,IEmailService emailService) 
            :base(appContext)
        {
            _accountsRepository = AccountsRepository;
            _emailService = emailService;
        }

        public async Task<Result<IEnumerable<Account>>> GetAllUsers(string q = "")
        {
            var AccountSpec = IsActiveAccount(q);

            return await Async(AccountSpec)
                .Map(_accountsRepository.FindAllAsync)
              .Map(lstAccount => lstAccount.Select(p => p.ToContract()));
        }
      
        public async Task<Result<Account>> GetUser(string id)
        {
            return await _accountsRepository.GetByIdAsync(id)
               .Map(pg => pg.ToContract());
        }

        public async Task<Result<Account>> Create(AccountUpsert Account)
        {
            return  await Async(Account.ToDomain(_appContext.Email))
                     .Map(Validate)
                    .Map(_accountsRepository.SaveAsync)
                    .Map(SendUserCreationMail)
                    .Map(domAccount => domAccount.ToContract());
        }

        public async Task<Result<Account>> Update(string id, AccountUpsert Account)
        {
           
            return await Async(id)
                .Map(_accountsRepository.GetByIdAsync)
                .Map(domAccount => domAccount.Update(id, Account,_appContext.Email))
                .Map(_accountsRepository.SaveAsync)
                .Map(domAccount => domAccount.ToContract());
        }

        public async Task<Result<Account>> ValidateUser(AccountLogin login)
        {
            var ValidateUserSpec = FilterByEmail(login.Email, login.Password);
            return await Async(ValidateUserSpec)
                .Map(_accountsRepository.FindAllAsync)
                 .Map(ValidateDomainUser)
                .Map(domAccount => domAccount.ToContract());
        }

        public async Task<Result<Account>> ChangePassword(ChangePassword changePassword)
        {
            var ValidateUserSpec = FilterByEmail(changePassword.Email, changePassword.OldPassword);

            return await Async(ValidateUserSpec)
                .Map(_accountsRepository.FindAllAsync)
                .Map(ValidateDomainUser)
                .Map(domUser => domUser.UpdatePassword(changePassword.NewPassword, _appContext.Email))
                .Map(_accountsRepository.SaveAsync)
                .Map(domAccount => domAccount.ToContract());
        }

        public async Task<Result<Account>> ResetPassword(string email)
        {
            var ValidateUserSpec = FilterByEmail(email);

            var newPassword = RandomPassword.Generate();

            return await Async(ValidateUserSpec)
                .Map(_accountsRepository.FindAllAsync)
                .Map(ValidateDomainUser)
                .Map(domUser => domUser.UpdatePassword(newPassword, _appContext.Email))
                .Map(_accountsRepository.SaveAsync)
                 .Map(SendUserCreationMail)
                .Map(domAccount => domAccount.ToContract());
        }


        public async Task<Result<bool>> Delete(string path)
        {
            return await Async(path)
                .Map(_accountsRepository.DeleteAsync);
        }

        #region Private Methods

        private Task<Result<Domain.Account>> ValidateDomainUser(List<Domain.Account> domAccounts)
        {
            if (domAccounts.Count == 0)
            {
                return Async(Error.As<Domain.Account>(AccountErrorCode.UserNotExisted));
            }
            else
            {
                var domAccount = domAccounts.FirstOrDefault();
                if (domAccount.IsLockedOut)
                {
                    return Async(Error.As<Domain.Account>(AccountErrorCode.UserIsLocked));
                }
                return Async(Result(domAccount));
            }
        }

        private async Task<Result<Domain.Account>> SendUserCreationMail(Domain.Account account)
        {
            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = Path.Combine(Path.GetDirectoryName(exePath), "EmailTemplate/UserCreationEmailTemplate.html");
            string siteServicetemplate = string.Empty;
            using (StreamReader reader = new StreamReader(path))
            {
                siteServicetemplate = reader.ReadToEnd();
            }
            var usrName = $"{account.FirstName} {account.LastName}";
            var loginUrl = "";
            var message = string.Format(siteServicetemplate, usrName, account.Password, loginUrl);
            await _emailService.SendEmail(account.Email, "User Created Sucess", message);
            return account;
        }

        private async Task<Result<Domain.Account>> Validate(Domain.Account Account)
        {
            var AccountSpec = FilterByEmail(Account.Email);
            var AccountTitleExist = Spec<List<Domain.Account>>(pl => pl.Count <= 0);
            var domAccounts = await Async(AccountSpec)
               .Map(_accountsRepository.FindAllAsync);
           return domAccounts.Map(AccountTitleExist, () => Error.Of(ErrorCodes.InputExists)).Match(fail: e => e, pass: (p) => Result(Account));
        }

     

        private Specification<Domain.Account> IsActiveAccount(string q)
        {
            return new Specification<Domain.Account>(p => p.IsActive == true && p.IsDelete == false);
        }

        private Specification<Domain.Account> FilterByEmail(string email)
        {
            return new Specification<Domain.Account>(p => p.Email == email && p.IsDelete == false);
        }

        private Specification<Domain.Account> FilterByEmail(string email,string password)
        {
            return new Specification<Domain.Account>(p => p.Email == email && p.Password == password && p.IsDelete == false);
        }

        #endregion

    }
}