using Catalog.API.Data.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data.Context.Interfaces
{
    public interface ICatalogDbContext
    {
        IMongoCollection<Category> Categories { get; }
        IMongoCollection<Product> Products { get; }

        IMongoCollection<T> GetCollection<T>(string name);
    }
}
