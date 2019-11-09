namespace MLC.General.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using MLC.General.Contract;
    using MLC.Foundation;
    using Domain = MLC.General.Domain;


    public static class AccountAdapter
    {
        public static Account ToContract(this Domain.Account account)
        {
            return new Account()
            {
                Id = account.Id,

                FirstName = account.FirstName,
                LastName = account.LastName,
                Email = account.Email,
                Mobile = account.Mobile,
                Roles = account.Role,
                IsLockedOut = account.IsLockedOut,
                IsApproved = account.IsApproved,
                
                IsDelete = account.IsDelete,
                CreatedBy = account.CreatedBy,
                CreatedOn = account.CreatedOn,
                ModifiedOn = account.ModifiedOn,
                ModifiedBy = account.ModifiedBy
            };
        }
      
        public static Domain.Account Update(this Domain.Account DomAccount,string id, AccountUpsert account,string userId)
        {
            return new Domain.Account()
            {
                Id = id,

                FirstName = account.FirstName,
                LastName = account.LastName,
                Email = account.Email,
                Mobile = account.Mobile,
                Role = account.Roles,

                CreatedBy = DomAccount.CreatedBy,
                CreatedOn = DomAccount.CreatedOn,
                ModifiedOn = DateTime.Now,
                ModifiedBy = userId
            };
        }

        public static Domain.Account UpdatePassword(this Domain.Account domAccount, string newPassword, string userId)
        {
            domAccount.Password = newPassword;            
            domAccount.PasswordLastModify = DateTime.Now;
            return domAccount;
        }

        public static Domain.Account ToDomain(this AccountUpsert account, string userId)
        {
            return new Domain.Account()
            {
                FirstName = account.FirstName,
                LastName = account.LastName,
                Email = account.Email,
                Mobile = account.Mobile,
                Role = account.Roles,
                IsApproved = true,
                IsLockedOut = false,
                PasswordLastModify = DateTime.Now,
                Password = RandomPassword.Generate(),
              CreatedBy =   userId
              
        };
        }
      
    }
}
