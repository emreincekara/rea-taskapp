using Order.Domain.Common;
using System.Linq.Expressions;

namespace Order.Application.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> AddAsync(TEntity model);
        Task<A> AddAsync<A>(A model) where A : class;
        Task<bool> UpdateAsync(TEntity model);
        Task<bool> UpdateAsync<A>(A model) where A : class;
        Task<bool> DeleteAsync(Guid id);

        Task<TEntity> GetByIdAsync(Guid id);
        Task<A> GetByIdAsync<A>(Guid id) where A : class;

        Task<IList<TEntity>> GetAllAsync();
        Task<IList<A>> GetAllAsync<A>() where A : class;

        Task<IList<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IList<A>> GetByAsync<A>(Expression<Func<A, bool>> predicate) where A : class;
        Task<IList<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> predicate, string orderBy, bool isDesc = false);
        Task<IList<A>> GetByAsync<A>(Expression<Func<A, bool>> predicate, string orderBy, bool isDesc = false) where A : class;

        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = false);
        Task<A> GetSingleAsync<A>(Expression<Func<A, bool>> predicate, bool asNoTracking = false) where A : class;

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

        Task<int> SaveAsync();
    }
}
