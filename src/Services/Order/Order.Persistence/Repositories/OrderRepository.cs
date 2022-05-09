using AutoMapper;
using Order.Application.DTOs.OrderDTOs;
using Order.Application.Interfaces.Repositories;
using Order.Domain.Entities;
using Order.Persistence.Context;

namespace Order.Persistence.Repositories
{
    public class OrderRepository : Repository<OrderDetail>, IOrderRepository
    {
        private readonly IMapper _mapper;
        public OrderRepository(OrderDbContext context, IMapper mapper) : base(context, mapper)
        {
            _mapper = mapper;
        }

        public async Task<DetailOrderDTO> AddAsync(AddOrderDTO model)
        {
            var addedOrder = await AddAsync(_mapper.Map<OrderDetail>(model));
            return _mapper.Map<DetailOrderDTO>(addedOrder);
        }

        public async Task<bool> UpdateAsync(EditOrderDTO model)
        {
            var order = await GetByIdAsync(model.Id);
            if(order == null)
                return false;

            order.CustomerId = model.CustomerId;
            order.AddressId = model.AddressId;
            order.ProductId = model.ProductId;
            order.Price = model.Price;
            order.Quantity = model.Quantity;
            order.Status = model.Status;
            order.UpdatedAt = DateTime.UtcNow;

            await SaveAsync();
            return true;
        }
    }
}
