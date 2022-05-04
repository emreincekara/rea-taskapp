using Customer.API.Data.Context;
using Customer.API.Data.Entities.Common;
using Customer.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Customer.API.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly CustomerDbContext _context;

        public Repository(CustomerDbContext context)
        {
            _context = context;
        }

        public DbSet<TEntity> Table()
        {
            return _context.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity model)
        {
            var addedModel = await Table().AddAsync(model);
            await SaveAsync();
            return addedModel.Entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            Table().Remove(await GetByIdAsync(id));
            await SaveAsync();
        }

        public async Task<IList<TEntity>> GetAllAsync()
        {
            return await Table().ToListAsync();
        }

        public async Task<IList<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Table().Where(predicate).ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await Table().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateAsync(TEntity model)
        {
            var entity = await GetByIdAsync(model.Id);
            if(entity == null)
                return false;
            _context.Entry(entity).CurrentValues.SetValues(model);
            await SaveAsync();
            return true;
        }

        private async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Table().AnyAsync(predicate);
        }
    }
}
