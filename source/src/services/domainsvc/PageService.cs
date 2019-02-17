namespace Bapatla.CMS.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Bapatla.CMS.Contract;
    using Domain =  Bapatla.CMS.Domain;
    using System.Linq;
    using System;
    using System.Linq.Expressions;

    public class PageService : IPageService 
    {
        private Domain.IPagesRepository _pagesRepository;
        public PageService(Domain.IPagesRepository pagesRepository)
        {
          _pagesRepository = pagesRepository;
        }

        public async Task<IEnumerable<Page>> GetAllPages()
        {
            Expression<Func<Domain.Page, bool>> isDeleted = p => p.IsDelete == false;

            var lstPage = (await _pagesRepository.FindAllAsync(isDeleted)).ToList();
            return lstPage.Select(p => p.ToContract());
        }

        public async Task<Page> GetPage(string path)
        {
            var page = await _pagesRepository.GetByIdAsync(path);
            return page.ToContract();
        }
       
        public async Task Create(Page Page)
        {
            Page.Id = new Guid().ToString("N");
            Page.PageSlug = StringHelper.GenerateSlug(Page.Title);
            await _pagesRepository.SaveAsync(Page.ToDomain());
            return;
        }

        public async Task<bool> Update(Page Page)
        {
           await _pagesRepository.SaveAsync(Page.ToDomain());
            return true;
        }

        public async Task<bool> Delete(string path)
        {
            await _pagesRepository.DeleteAsync(path);
            return true;
        }


        
    }
}