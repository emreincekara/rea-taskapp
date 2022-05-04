using Customer.API.Data.Context;
using Customer.API.Data.Entities;
using Customer.API.Repositories.Interfaces;

namespace Customer.API.Repositories
{
    public class CustomerRepository : Repository<CustomerDetail>, ICustomerRepository
    {
        public CustomerRepository(CustomerDbContext context) : base(context)
        {
        }
    }
}
