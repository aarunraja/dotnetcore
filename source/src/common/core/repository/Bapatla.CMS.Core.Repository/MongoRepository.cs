namespace Bapatla.CMS.Core.Repository
{
    using Bapatla.CMS.Core.Base;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class MongoRepository<TEntity> : IRepository<TEntity> where TEntity : DomainEntity
    {
        bool disposed = false;

       protected abstract IMongoCollection<TEntity> Collection { get; }

        public virtual async Task<TEntity> SaveAsync(TEntity entity)
        {
            entity.GenerateNewIdentity(entity.Id);

            await Collection.ReplaceOneAsync(
                x => x.Id.Equals(entity.Id),
                entity,
                new UpdateOptions
                {
                    IsUpsert = true
                });

            return entity;
        }

        public virtual async Task<bool> DeleteAsync(string id)
        {
            var deleteResult = await Collection.DeleteOneAsync(x => x.Id.Equals(id));
            return deleteResult.IsAcknowledged;
        }

        public virtual async Task<ICollection<TEntity>> FindAllAsync(
            Expression<Func<TEntity, bool>> predicate)
        {
            return await Collection.Find(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(string id)
        {
            return await Collection.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
               disposed = true;

            }
        }

    }
}
