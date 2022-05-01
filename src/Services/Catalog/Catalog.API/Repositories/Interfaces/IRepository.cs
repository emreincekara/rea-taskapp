using Catalog.API.Data.Common;
using System.Linq.Expressions;

namespace Catalog.API.Repositories.Interfaces
{
    public interface IRepository<T> : IDisposable where T : BaseEntity
    {
        Task<T> AddAsync(T model);
        Task<bool> UpdateAsync(T model, string id);
        Task<bool> DeleteAsync(string id);

        Task<T> GetByIdAsync(string id);

        Task<IList<T>> GetAllAsync();
        Task<IList<T>> GetByAsync(Expression<Func<T, bool>> predicate);
    }
}
