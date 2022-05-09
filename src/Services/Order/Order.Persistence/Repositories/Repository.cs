using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Order.Application.Extentions;
using Order.Application.Interfaces.Repositories;
using Order.Domain.Common;
using Order.Persistence.Context;
using System.Linq.Expressions;

namespace Order.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly OrderDbContext _context;
        private readonly IMapper _mapper;

        public Repository(OrderDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TEntity> AddAsync(TEntity model)
        {
            var addedModel = await Table().AddAsync(model);
            await SaveAsync();
            return addedModel.Entity;
        }

        public async Task<A> AddAsync<A>(A model) where A : class
        {
            var addedModel = await AddAsync(_mapper.Map<TEntity>(model));
            return _mapper.Map<A>(addedModel);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Table().AnyAsync(predicate);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            TEntity model = await GetByIdAsync(id);

            if (model == null)
                return false;

            await Task.Run(() => { Table().Remove(model); });
            await SaveAsync();
            return true;
        }

        public async Task<IList<TEntity>> GetAllAsync()
        {
            return await Table().ToListAsync();
        }

        public async Task<IList<A>> GetAllAsync<A>() where A : class
        {
            return await Task.FromResult(Table().Select(_mapper.Map<A>).ToList());
        }

        public async Task<IList<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Table().Where(predicate).ToListAsync();
        }

        public async Task<IList<A>> GetByAsync<A>(Expression<Func<A, bool>> predicate) where A : class
        {
            return await Task.FromResult(_mapper.Map<IList<A>>(Table().Where(_mapper.Map<Expression<Func<TEntity, bool>>>(predicate)).ToList()));
        }

        public async Task<IList<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> predicate, string orderBy, bool isDesc = false)
        {
            return isDesc
                    ? await Task.FromResult(Table().Where(predicate).OrderByDescending(orderBy).ToList())
                    : await Task.FromResult(Table().Where(predicate).OrderBy(orderBy).ToList());
        }

        public async Task<IList<A>> GetByAsync<A>(Expression<Func<A, bool>> predicate, string orderBy, bool isDesc = false) where A : class
        {
            return isDesc
                    ? await Task.FromResult((await GetByAsync<A>(predicate)).AsQueryable().OrderByDescending(orderBy).ToList())
                    : await Task.FromResult((await GetByAsync<A>(predicate)).AsQueryable().OrderBy(orderBy).ToList());
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await GetSingleAsync(x => x.Id == id);
        }

        public async Task<A> GetByIdAsync<A>(Guid id) where A : class
        {
            return await Task.FromResult(_mapper.Map<A>(await GetByIdAsync(id)));
        }

        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = false)
        {
            var query = Table().AsQueryable();

            if (asNoTracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<A> GetSingleAsync<A>(Expression<Func<A, bool>> predicate, bool asNoTracking = false) where A : class
        {
            return _mapper.Map<A>(await GetSingleAsync(_mapper.Map<Expression<Func<TEntity, bool>>>(predicate), asNoTracking));
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public DbSet<TEntity> Table()
        {
            return _context.Set<TEntity>();
        }

        public async Task<bool> UpdateAsync(TEntity model)
        {
            TEntity updatedModel = await GetByIdAsync(model.Id);
            if (updatedModel == null)
                return false;

            foreach (var property in typeof(TEntity).GetProperties())
                property.SetValue(updatedModel, property.GetValue(model));

            await SaveAsync();
            return true;
        }

        public async Task<bool> UpdateAsync<A>(A model) where A : class
        {
            return await UpdateAsync(_mapper.Map<TEntity>(model));
        }
    }
}
