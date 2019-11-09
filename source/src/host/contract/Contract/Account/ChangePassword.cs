namespace MLC.General.Contract
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;


    public class ChangePassword
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, StringLength(50)]
        public string OldPassword { get; set; }
        [Required, StringLength(50)]
        public string NewPassword { get; set; }
    }
}
