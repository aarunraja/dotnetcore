namespace Bapatla.CMS.Contract
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IPageService
    {
         Task<IEnumerable<Page>> GetAllPages();
        Task<Page> GetPage(string path);
        Task Create(Page page);
        Task<bool> Update(Page page);
        Task<bool> Delete(string path);
    }
}