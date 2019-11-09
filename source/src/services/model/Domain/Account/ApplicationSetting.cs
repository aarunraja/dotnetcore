namespace MLC.General.Domain
{
    using MLC.Core;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ApplicationSetting : DomainEntity
    {
        
        public string CollegeName { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Map { get; set; }
        public string Fax { get; set; }
        public string Logo { get; set; }
        public string FaceBookLink { get; set; }
        public string TwitterLink { get; set; }
        public string GooglePlusLink { get; set; }
        public string YouTubeLink { get; set; }
        public string RSSFeedLink { get; set; }
        public int NumberOfStudents { get; set; }
        public int NumberOfCourses { get; set; }
        public int NumberOfStaffs { get; set; }
        public int NumberOfCompanies { get; set; }
    }
}
