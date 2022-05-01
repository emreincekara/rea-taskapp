using Catalog.API.Data.Context.Interfaces;
using Catalog.API.Repositories.Interfaces;

namespace Catalog.API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ICatalogDbContext _context;
        public ICategoryRepository CategoryRepository { get; }
        public IProductRepository ProductRepository { get; }

        public UnitOfWork(ICatalogDbContext context, ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _context = context;
            CategoryRepository = categoryRepository;
            ProductRepository = productRepository;
        }
    }
}
