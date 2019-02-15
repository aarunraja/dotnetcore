namespace Bapatla.CMS.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Bapatla.CMS.Contract;
    using Domain =  Bapatla.CMS.Domain;
    public class PageService : IPageService 
    {
        private Domain.IPagesRepository pagesRepository;
        public PageService(Domain.IPagesRepository pagesRepository)
        {
          this.pagesRepository = pagesRepository;
        }

        public async Task<IEnumerable<Page>> GetAllPages()
        {
          await pagesRepository.GetAllPages();
           return null;
        }

        public async Task<Page> GetPage(string path)
        {
            await pagesRepository.GetAllPages();
            return null;
        }
       
        public async Task Create(Page Page)
        {
            await pagesRepository.GetAllPages();
            return;
        }

        public async Task<bool> Update(Page Page)
        {
           await pagesRepository.GetAllPages();
            return true;
        }

        public async Task<bool> Delete(string path)
        {
            await pagesRepository.GetAllPages();
            return true;
        }
        
    }
}