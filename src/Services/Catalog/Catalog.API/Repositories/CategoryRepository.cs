using Catalog.API.Configurations.Interfaces;
using Catalog.API.Data.Context.Interfaces;
using Catalog.API.Data.Entities;
using Catalog.API.Repositories.Interfaces;

namespace Catalog.API.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(IDatabaseSettings databaseSettings, ICatalogDbContext context) : base(databaseSettings, context)
        {
        }
    }
}
