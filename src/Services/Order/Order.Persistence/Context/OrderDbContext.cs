using Microsoft.EntityFrameworkCore;
using Order.Application.Interfaces.Context;
using Order.Domain.Entities;

namespace Order.Persistence.Context
{
    public class OrderDbContext : DbContext, IOrderDbContext
    {
        public OrderDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<OrderDetail> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<OrderDetail>()
                .Property(x => x.Id)
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Entity<OrderDetail>()
                .Property(x => x.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
