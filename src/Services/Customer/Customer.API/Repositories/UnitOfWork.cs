using Customer.API.Data.Context;
using Customer.API.Repositories.Interfaces;

namespace Customer.API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CustomerDbContext _context;
        public ICustomerRepository CustomerRepository { get; set; }
        public IAddressRepository AddressRepository { get; set; }

        public UnitOfWork(CustomerDbContext context, ICustomerRepository customerRepository, IAddressRepository addressRepository)
        {
            _context = context;
            CustomerRepository = customerRepository;
            AddressRepository = addressRepository;
        }
    }
}
