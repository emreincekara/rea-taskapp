using Catalog.API.Data.Context.Interfaces;
using Catalog.API.Data.Entities;
using Catalog.API.Repositories.Interfaces;

namespace Catalog.API.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ICatalogDbContext context) : base(context)
        {
        }
    }
}
