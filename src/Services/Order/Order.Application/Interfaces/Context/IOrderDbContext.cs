using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;

namespace Order.Application.Interfaces.Context
{
    public interface IOrderDbContext
    {
        DbSet<OrderDetail> Orders { get; set; }
    }
}
