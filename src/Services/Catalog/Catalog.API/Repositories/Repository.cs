using Catalog.API.Configurations.Interfaces;
using Catalog.API.Data.Entities.Common;
using Catalog.API.Data.Context.Interfaces;
using Catalog.API.Repositories.Interfaces;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Catalog.API.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ICatalogDbContext _context;
        private readonly IMongoCollection<TEntity> _collection;
        public Repository(IDatabaseSettings databaseSettings, ICatalogDbContext context)
        {
            _context = context;
            _collection = _context.GetCollection<TEntity>(typeof(TEntity).Name == "Category" ? 
                databaseSettings.CategoryCollectionName : 
                databaseSettings.ProductCollectionName);
        }

        public async Task<TEntity> AddAsync(TEntity model)
        {
            model.Id = Guid.NewGuid().ToString();
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

        public async Task<IList<TEntity>> GetAllAsync()
        {
            return await _collection.Find(x => true).ToListAsync();
        }

        public async Task<IList<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _collection.Find(predicate).ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateAsync(TEntity model)
        {
            var updateResult = await _collection.ReplaceOneAsync(filter: x => x.Id == model.Id, replacement: model);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
