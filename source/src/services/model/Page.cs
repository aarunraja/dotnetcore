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
        public string MainImage { get; set; }
        public string MenuType { get; set; }
        public int Position { get; set; }
        public string PageSlug { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string SEOTitle { get; set; }
        public string ParentPage { get; set; }
        public string Status { get; set; }
        public bool IsDelete { get; set; }
    }
}

