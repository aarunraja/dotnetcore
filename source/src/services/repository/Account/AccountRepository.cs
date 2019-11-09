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
    using MLC.Core;

    public class AccountRepository : MongoRepository<Account>, IAccountRepository
    {
        
        private const string CollectionName = nameof(Account);
        private readonly MongoDatabaseContext _dataContext;

        public AccountRepository(MongoDatabaseContext dataContext)
        {
            _dataContext = dataContext;
        }

      
        protected override IMongoCollection<Account> Collection =>
            _dataContext.Database.GetCollection<Account>(CollectionName);

        public async override Task<Result<Account>> GetByIdAsync(string id)
        {
            try
            {
                var result = await Collection.Find(x => x.Email.Equals(id)).FirstOrDefaultAsync();
                return result ?? Error.As<Account>(ErrorCodes.ResourceNotFound);
            }
            catch (Exception)
            {
                return Error.As<Account>(ErrorCodes.InternalServerError);
            }
        }
    }
}
