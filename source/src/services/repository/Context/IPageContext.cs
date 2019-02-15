namespace Bapatla.CMS.Repository
{
    using Bapatla.CMS.Domain;
    using MongoDB.Driver;
    public interface IPageContext
    {
         IMongoCollection<Page> Pages { get; }
    }
}