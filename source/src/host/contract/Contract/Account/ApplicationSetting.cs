namespace MLC.General.Contract
{
    using MLC.Core;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ApplicationSetting : BaseModel
    {
        public string Title { get; set; }
        public string CollegeName { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Map { get; set; }
        public string Fax { get; set; }
        public string Logo { get; set; }
    }
}
