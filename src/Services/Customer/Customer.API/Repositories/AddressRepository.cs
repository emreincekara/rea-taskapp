using Customer.API.Data.Context;
using Customer.API.Data.Entities;
using Customer.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Repositories
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(CustomerDbContext context) : base(context)
        {
        }

        public async Task<Address> GetAddressByCustomerIdAsync(Guid customerId)
        {
            return await Table().FirstOrDefaultAsync(x => x.CustomerId == customerId);
        }
    }
}
