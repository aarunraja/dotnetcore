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
                MainImage = page.MainImage,
                MenuType = page.MenuType,
                Position = page.Position,
                PageSlug = page.PageSlug,
                Description = page.Description,
                ParentPage = page.ParentPage,
                ShortDescription = page.ShortDescription,
                Status = page.Status,
                IsDelete = page.IsDelete,
                SEOTitle = page.SEOTitle
            };
        }
        public static Domain.Page ToDomain(this Page page)
        {
            return new Domain.Page()
            {
                Title = page.Title,
                MainImage = page.MainImage,
                MenuType = page.MenuType,
                Position = page.Position,
                PageSlug = page.PageSlug,
                Description = page.Description,
                ParentPage = page.ParentPage,
                ShortDescription = page.ShortDescription,
                IsDelete = page.IsDelete,
                Status= page.Status,
                SEOTitle = page.SEOTitle
            };
        }
      
    }
}
