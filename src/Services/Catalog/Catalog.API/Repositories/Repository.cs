using Catalog.API.Data.Common;
using Catalog.API.Data.Context.Interfaces;
using Catalog.API.Repositories.Interfaces;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Catalog.API.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ICatalogDbContext _context;
        private readonly IMongoCollection<T> _collection;
        public Repository(ICatalogDbContext context)
        {
            _context = context;
            _collection = _context.GetCollection<T>(typeof(T).Name);
        }

        public async Task<T> AddAsync(T model)
        {
            model.Id = new Guid().ToString();
            await _collection.InsertOneAsync(model);
            return model;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var deleteResult = await _collection.DeleteOneAsync(x => x.Id == id);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await _collection.Find(x => true).ToListAsync();
        }

        public async Task<IList<T>> GetByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _collection.Find(predicate).ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(T model, string id)
        {
            var updateResult = await _collection.ReplaceOneAsync(filter: x => x.Id == id, replacement: model);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
