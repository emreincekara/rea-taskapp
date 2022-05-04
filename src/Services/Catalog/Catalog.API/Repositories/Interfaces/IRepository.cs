using Catalog.API.Data.Entities.Common;
using System.Linq.Expressions;

namespace Catalog.API.Repositories.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : BaseEntity
    {
        Task<TEntity> AddAsync(TEntity model);
        Task<bool> UpdateAsync(TEntity model);
        Task<bool> DeleteAsync(string id);

        Task<TEntity> GetByIdAsync(string id);

        Task<IList<TEntity>> GetAllAsync();
        Task<IList<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
