namespace MLC.General.Domain
{
    using MLC.Core;
    using System;

    public class Account: DomainEntity
    {
        public Account()
        { }

        public Account(string email, string firstName, string lastName,string mobile,string password)
        {
            GenerateNewIdentity();
            this.Email = email;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Mobile = mobile;
            this.Password = PasswordHashing.Hash(password);
            this.PasswordLastModify = DateTime.Now;
        }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime PasswordLastModify { get; set; }
        public bool IsApproved { get; set; }
        public bool IsLockedOut { get; set; }
    }
}
