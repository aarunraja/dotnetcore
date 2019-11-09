namespace MLC.General.Repository
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MLC.General.Domain;
    using MLC.Core.Repository;
    using System.Linq.Expressions;
    using System;

    public class ApplicationSettingRepository : MongoRepository<ApplicationSetting>, IApplicationSettingRepository
    {
        
        private const string CollectionName = nameof(ApplicationSetting);
        private readonly MongoDatabaseContext _dataContext;

        public ApplicationSettingRepository(MongoDatabaseContext dataContext)
        {
            _dataContext = dataContext;
        }

      
        protected override IMongoCollection<ApplicationSetting> Collection =>
            _dataContext.Database.GetCollection<ApplicationSetting>(CollectionName);
    }
}
