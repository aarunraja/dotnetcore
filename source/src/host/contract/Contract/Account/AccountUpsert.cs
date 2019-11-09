using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MLC.General.Contract
{
    public class AccountUpsert
    {
        [Required,EmailAddress]
       public string Email { get; set; }
        [Required,Phone]
        public string Mobile { get; set; }
        [Required, StringLength(25)]
        public string Roles { get; set; }
        [Required, StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
    }
}
