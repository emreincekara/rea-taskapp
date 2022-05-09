using Order.Application.DTOs.OrderDTOs;
using Order.Domain.Entities;

namespace Order.Application.Interfaces.Repositories
{
    public interface IOrderRepository : IRepository<OrderDetail>
    {
        Task<DetailOrderDTO> AddAsync(AddOrderDTO model);
        Task<bool> UpdateAsync(EditOrderDTO model);
    }
}
