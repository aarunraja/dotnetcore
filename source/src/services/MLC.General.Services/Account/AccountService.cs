namespace MLC.General.Services
{
    using MLC.Core;
    using MLC.General.Contract;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Domain = MLC.General.Domain;

    /// <summary>
    /// Account Service
    /// </summary>
    public class AccountService
    {
        private readonly Domain.IAccountRepository _accountsRepository;
        private readonly IEmailService _emailService;
        private readonly ApplicationContext _appContext;

        /// <summary>
        /// Account Service
        /// </summary>
        /// <param name="appContext"></param>
        /// <param name="AccountsRepository"></param>
        /// <param name="emailService"></param>
        public AccountService(ApplicationContext appContext, Domain.IAccountRepository AccountsRepository, IEmailService emailService)
        {
            _accountsRepository = AccountsRepository;
            _emailService = emailService;
            _appContext = appContext;
        }

        /// <summary>
        /// GetAllUsers
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public async Task<Result<IEnumerable<Account>>> GetAllUsers(string q = "")
        {
            var AccountSpec = IsActiveAccount();
            var lstAccount = await _accountsRepository.FindAllAsync(AccountSpec);
            if (lstAccount.HasValue)
            {
                return lstAccount.value.Select(p => p.ToContract()).ToList();
            }
            return lstAccount.Error;
        }

        /// <summary>
        /// GetUser
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result<Account>> GetUser(string id)
        {
            var account = await _accountsRepository.GetByIdAsync(id);
            if (account.HasValue)
            {
                return account.value.ToContract();
            }
            return account.Error;
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public async Task<Result<Account>> Create(AccountUpsert account)
        {
            if (await ValidateUserExisted(FilterByEmail(account.Email)))
            {
                return Error.As<Account>(ErrorCodes.InputExists,$"{account.Email} is already existed.");
            }
            var strPassword = RandomPassword.Generate();
            var domAccount = new Domain.Account(account.Email, account.FirstName, account.LastName, account.Mobile, strPassword);
            var result = await _accountsRepository.SaveAsync(domAccount);
            if (result.HasValue)
            {
                await SendUserCreationMail(result.value, strPassword);
                return result.value.ToContract();
            }
            return result.Error;
        }


        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Account"></param>
        /// <returns></returns>
        public async Task<Result<Account>> Update(string id, AccountUpsert Account)
        {
            var result = await _accountsRepository.GetByIdAsync(id);
            if (result.HasValue)
            {
                result = result.value.Update(id, Account, _appContext.Email);
                result = await _accountsRepository.SaveAsync(result.value);
                if (!result.HasError)
                {
                    return result.value.ToContract();
                }
            }
            return result.Error;
        }

        /// <summary>
        /// ValidateUser
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<Result<Account>> ValidateUser(AccountLogin login)
        {
            var ValidateUserSpec = FilterByEmail(login.Email);
            var result = await _accountsRepository.FindAllAsync(ValidateUserSpec);
            if (result.HasValue)
            {
                if (result.value.Count > 0)
                {
                    var user = result.value.FirstOrDefault();
                    if (PasswordHashing.CompareHash(login.Password, user.Password))
                    {
                        return user.ToContract();
                    }
                }
                return Error.As<Account>(ErrorCodes.NotAuthorised);
            }
            return result.Error;
        }

        /// <summary>
        /// ChangePassword
        /// </summary>
        /// <param name="changePassword"></param>
        /// <returns></returns>
        public async Task<Result<Account>> ChangePassword(ChangePassword changePassword)
        {
            var ValidateUserSpec = FilterByEmail(changePassword.Email, changePassword.OldPassword);
            var result = await _accountsRepository.FindAllAsync(ValidateUserSpec);
            if (result.HasError)
            {
                return Error.As<Account>(AccountErrorCode.UserNotExisted);
            }
            var domAccount = result.value.FirstOrDefault();
            if (domAccount.IsLockedOut)
            {
                return Error.As<Account>(AccountErrorCode.UserIsLocked);
            }
            domAccount = domAccount.UpdatePassword(changePassword.NewPassword, _appContext.Email);
            var saveResult = await _accountsRepository.SaveAsync(domAccount);
            if (!saveResult.HasError)
            {
                return saveResult.value.ToContract();
            }
            return result.Error;
        }

        /// <summary>
        /// ResetPassword
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<Result<Account>> ResetPassword(string email)
        {
            var ValidateUserSpec = FilterByEmail(email);
            var newPassword = RandomPassword.Generate();

            var result = await _accountsRepository.FindAllAsync(ValidateUserSpec);
            if (result.HasError)
            {
                return Error.As<Account>(AccountErrorCode.UserNotExisted);
            }
            var domAccount = result.value.FirstOrDefault();
            if (domAccount.IsLockedOut)
            {
                return Error.As<Account>(AccountErrorCode.UserIsLocked);
            }
            domAccount = domAccount.UpdatePassword(newPassword, _appContext.Email);
            var saveResult = await _accountsRepository.SaveAsync(domAccount);
            if (!saveResult.HasError)
            {
                return saveResult.value.ToContract();
            }
            return result.Error;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<Result<bool>> Delete(string path)
        {
            var result = await _accountsRepository.DeleteAsync(path);
            if (result.HasError)
            {
                return result.Error;

            }
            return true;
        }

        #region Private Methods

        private Specification<Domain.Account> IsActiveAccount() => new Specification<Domain.Account>(p => p.IsActive && p.IsDelete);
        private Specification<Domain.Account> FilterByEmail(string email) => new Specification<Domain.Account>(p => p.Email == email && !p.IsDelete);
        private Specification<Domain.Account> FilterByEmail(string email, string password) => new Specification<Domain.Account>(p => p.Email == email && p.Password == password && p.IsDelete);

        private async Task<bool> ValidateUserExisted(Specification<Domain.Account> accountSpec)
        {
            var validResult = await _accountsRepository.FindAllAsync(accountSpec);
            return validResult.HasValue && validResult.value.Count > 0 ;
        }


        private async Task<Result<Domain.Account>> SendUserCreationMail(Domain.Account account,string password)
        {
            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = Path.Combine(Path.GetDirectoryName(exePath), "EmailTemplate/UserCreationEmailTemplate.html");
            string siteServicetemplate = string.Empty;
            using (StreamReader reader = new StreamReader(path))
            {
                siteServicetemplate = reader.ReadToEnd();
            }
            var usrName = $"{account.FirstName} {account.LastName}";
            var loginUrl = "#";
            var message = string.Format(siteServicetemplate, usrName, password, loginUrl);
            await _emailService.SendEmail(account.Email, "User Created Sucess", message);
            return account;
        }
        #endregion

    }
}