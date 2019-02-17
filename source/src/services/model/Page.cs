namespace Bapatla.CMS.Domain
{
    using System;
    using System.Collections.Generic;
    using Bapatla.CMS.Core.Base;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Page : DomainEntity
    {
        public string Title { get; set; }
        public string Images { get; set; }
        public string MenuPostion { get; set; }
        public int Position { get; set; }
        public string Slug { get; set; }
        public string Descripation { get; set; }
        public string RootPage { get; set; }
        //public PageStatus Stataus { get; set; }
        public bool IsDelete { get; set; }
    }
}

