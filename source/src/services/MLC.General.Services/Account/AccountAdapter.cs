namespace MLC.General.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using MLC.General.Contract;
    using MLC.Core;
    using Domain = MLC.General.Domain;

    /// <summary>
    /// Account Adapter
    /// </summary>
    public static class AccountAdapter
    {
        /// <summary>
        /// To Contract
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="DomAccount"></param>
        /// <param name="id"></param>
        /// <param name="account"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Update Password
        /// </summary>
        /// <param name="domAccount"></param>
        /// <param name="newPassword"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static Domain.Account UpdatePassword(this Domain.Account domAccount, string newPassword, string userId)
        {
            domAccount.Password = newPassword;            
            domAccount.PasswordLastModify = DateTime.Now;
            return domAccount;
        }

        /// <summary>
        /// To Domain
        /// </summary>
        /// <param name="account"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
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
