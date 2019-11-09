using System;
using System.Collections.Generic;
using System.Text;

namespace MLC.General.Contract
{
    public class Account: BaseModel
    {
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Roles { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsLockedOut { get; set; }
        public bool IsApproved { get; set; }
    }
}
