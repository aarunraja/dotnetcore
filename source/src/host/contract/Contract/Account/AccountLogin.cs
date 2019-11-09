using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MLC.General.Contract
{
    public class AccountLogin
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, StringLength(25)]
        public string Password { get; set; }
    }
}
