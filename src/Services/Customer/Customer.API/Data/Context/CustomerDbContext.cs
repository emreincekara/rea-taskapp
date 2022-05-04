using Customer.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Data.Context
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CustomerDetail> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CustomerDetail>()
                .Property(x => x.Id)
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Entity<CustomerDetail>()
                .Property(x => x.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Entity<Address>()
                .Property(x => x.Id)
                .HasDefaultValueSql("gen_random_uuid()");
        }
    }
}
