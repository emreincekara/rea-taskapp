namespace Customer.API.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        public ICustomerRepository CustomerRepository { get; }
        public IAddressRepository AddressRepository { get; set; }
    }
}
