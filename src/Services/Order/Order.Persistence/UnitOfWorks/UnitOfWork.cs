using Order.Application.Interfaces.Repositories;
using Order.Application.Interfaces.UnitOfWork;
using Order.Persistence.Context;

namespace Order.Persistence.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OrderDbContext _context;
        public IOrderRepository OrderRepository { get; set; }

        public UnitOfWork(OrderDbContext context, IOrderRepository orderRepository)
        {
            _context = context;
            OrderRepository = orderRepository;
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
