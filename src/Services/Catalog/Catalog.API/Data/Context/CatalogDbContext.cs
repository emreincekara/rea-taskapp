using Catalog.API.Configurations.Interfaces;
using Catalog.API.Data.Context.Interfaces;
using Catalog.API.Data.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data.Context
{
    public class CatalogDbContext : ICatalogDbContext
    {
        private IMongoDatabase MongoDatabase { get; set; }
        private MongoClient MongoClient { get; set; }

        public CatalogDbContext(IDatabaseSettings databaseSettings)
        {
            MongoClient = new MongoClient(databaseSettings.ConnectionString);
            MongoDatabase = MongoClient.GetDatabase(databaseSettings.DatabaseName);

            Categories = MongoDatabase.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            Products = MongoDatabase.GetCollection<Product>(databaseSettings.ProductCollectionName);

            CatalogDbContextSeed<Category>.SeedData(Categories);
            CatalogDbContextSeed<Product>.SeedData(Products);
        }

        public IMongoCollection<Category> Categories { get; set; }
        public IMongoCollection<Product> Products { get; set; }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return MongoDatabase.GetCollection<T>(name);
        }
    }
}
