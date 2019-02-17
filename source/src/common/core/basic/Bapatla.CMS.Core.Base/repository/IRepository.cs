
namespace Bapatla.CMS.Core.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;

    public interface IRepository<TEntity> : IDisposable where TEntity : DomainEntity
    {
        Task<ICollection<TEntity>> FindAllAsync(
            Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetByIdAsync(string id);
        Task<TEntity> SaveAsync(TEntity entity);
        Task<bool> DeleteAsync(string id);
    }
}
