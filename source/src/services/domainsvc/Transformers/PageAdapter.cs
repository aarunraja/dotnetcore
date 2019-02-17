namespace Bapatla.CMS.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Bapatla.CMS.Contract;
    using Domain = Bapatla.CMS.Domain;


    public static class PageAdapter
    {
        public static Page ToContract(this Domain.Page page)
        {
            return new Page()
            {
                Id = page.Id.ToString(),
                Title = page.Title,
                Images = page.Images,
                MenuPostion = page.MenuPostion,
                Position = page.Position,
                Slug = page.Slug,
                Descripation = page.Descripation,
                RootPage = page.RootPage,
                IsDelete = page.IsDelete
            };
        }
        public static Domain.Page ToDomain(this Page page)
        {
            return new Domain.Page()
            {  
                Title = page.Title,
                Images = page.Images,
                MenuPostion = page.MenuPostion,
                Position = page.Position,
                Slug = page.Slug,
                Descripation = page.Descripation,
                RootPage = page.RootPage,
                IsDelete = page.IsDelete
            };
        }
      
    }
}
