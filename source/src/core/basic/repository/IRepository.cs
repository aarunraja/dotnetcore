
namespace MLC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;

    public interface IRepository<TEntity> : IDisposable where TEntity : DomainEntity
    {
        Task<Result<List<TEntity>>> FindAllAsync(Specification<TEntity> predicate);
        Task<Result<TEntity>> GetByIdAsync(string id);
        Task<Result<TEntity>> SaveAsync(TEntity entity);
        Task<Result<bool>> DeleteAsync(string id);
        Task<Result<long>> GetCountAsync(Specification<TEntity> predicate);
    }
}
