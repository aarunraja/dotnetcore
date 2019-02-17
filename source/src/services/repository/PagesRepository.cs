namespace Bapatla.CMS.Repository
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using Bapatla.CMS.Domain;
    using Bapatla.CMS.Core.Repository;
    using System.Linq.Expressions;
    using System;

    public class PagesRepository : MongoRepository<Page>, IPagesRepository
    {
        
        private const string PageCollectionName = CollectionNames.PageCollection;
        private readonly BapatlaDataContext _dataContext;

        public PagesRepository(BapatlaDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        protected override IMongoCollection<Page> Collection =>
            _dataContext.Database.GetCollection<Page>(PageCollectionName);
    }
}
