using Customer.API.Data.Entities;

namespace Customer.API.Repositories.Interfaces
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<Address> GetAddressByCustomerIdAsync(Guid customerId);
    }
}
