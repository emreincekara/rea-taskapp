using Order.Application.Interfaces.Repositories;

namespace Order.Application.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IOrderRepository OrderRepository { get; }
    }
}
