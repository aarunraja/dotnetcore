namespace MLC.Core.Repository
{
    using MLC.Core;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public abstract class MongoRepository<TEntity> : IRepository<TEntity> where TEntity : DomainEntity
    {
        bool disposed = false;

        protected abstract IMongoCollection<TEntity> Collection { get; }
        public virtual async Task<Result<List<TEntity>>> FindAllAsync(
                  Specification<TEntity> predicate)
        {
            try
            {
                return await Collection.Find(predicate.predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                return Error.As<List<TEntity>>(ErrorCodes.InternalServerError);
            }
        }

        public virtual async Task<Result<TEntity>> GetByIdAsync(string id)
        {
            try
            {
                var result = await Collection.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync();
                return result ?? Error.As<TEntity>(ErrorCodes.ResourceNotFound);
            }
            catch (Exception)
            {
                return Error.As<TEntity>(ErrorCodes.InternalServerError);
            }
        }

        public virtual async Task<Result<TEntity>> SaveAsync(TEntity entity)
        {
            try
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
            catch (Exception)
            {
                return Error.As<TEntity>(ErrorCodes.InternalServerError);
            }
        }

        public virtual async Task<Result<long>> GetCountAsync(Specification<TEntity> predicate)
        {
            try
            {
                return await Collection.CountDocumentsAsync(predicate.predicate);
            }
            catch (Exception)
            {
                return Error.As<long>(ErrorCodes.InternalServerError);
            }
        }

        public virtual async Task<Result<bool>> DeleteAsync(string id)
        {
            try
            {
                var deleteResult = await Collection.DeleteOneAsync(x => x.Id.Equals(id));
                return deleteResult.IsAcknowledged;
            }
            catch (Exception)
            {
                return Error.As<bool>(ErrorCodes.InternalServerError);
            }
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
