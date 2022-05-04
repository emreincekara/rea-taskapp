using Customer.API.Data.Entities.Common;
using System.Linq.Expressions;

namespace Customer.API.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> AddAsync(TEntity model);
        Task<bool> UpdateAsync(TEntity model);
        Task DeleteAsync(Guid id);

        Task<TEntity> GetByIdAsync(Guid id);

        Task<IList<TEntity>> GetAllAsync();
        Task<IList<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> predicate);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
