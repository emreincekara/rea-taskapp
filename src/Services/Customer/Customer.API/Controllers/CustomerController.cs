using Customer.API.Data.Entities;
using Customer.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Customer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IList<CustomerDetail>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<CustomerDetail>>> GetCustomers()
        {
            var customers = (await _unitOfWork.CustomerRepository.GetAllAsync()).Select(x =>
            {
                x.Address = _unitOfWork.AddressRepository.GetByAsync(i => i.CustomerId == x.Id).Result.FirstOrDefault();
                return x;
            });

            return Ok(customers);
        }

        [HttpGet("{id:length(36)}", Name = "GetCustomer")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CustomerDetail), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CustomerDetail>> GetCustomerById(string id)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(new Guid(id));
            if (customer == null)
                return NotFound();
            customer.Address = await _unitOfWork.AddressRepository.GetAddressByCustomerIdAsync(customer.Id);
            return Ok(customer);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerDetail), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<CustomerDetail>> AddCustomer([FromBody] CustomerDetail customer)
        {
            var addedCustomer = await _unitOfWork.CustomerRepository.AddAsync(customer);
            return CreatedAtRoute("GetCustomer", new { id = addedCustomer.Id.ToString() }, addedCustomer);
        }

        [HttpPut]
        [ProducesResponseType(typeof(CustomerDetail), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerDetail model)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(model.Id);
            if (customer == null)
                return NotFound();

            customer.Name = model.Name;
            customer.Email = model.Email;
            customer.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.CustomerRepository.UpdateAsync(customer);

            var address = await _unitOfWork.AddressRepository.GetAddressByCustomerIdAsync(customer.Id);

            model.Address.Id = address.Id;
            model.Address.CustomerId = customer.Id;

            await _unitOfWork.AddressRepository.UpdateAsync(model.Address);
            return Ok(true);
        }

        [HttpDelete("{id:length(36)}", Name = "DeleteCustomer")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            if (!await _unitOfWork.CustomerRepository.AnyAsync(x => x.Id.ToString() == id))
                return NotFound();
            await _unitOfWork.CustomerRepository.DeleteAsync(new Guid(id));
            return Ok(true);
        }

        [HttpGet("validate/{id:length(36)}", Name = "ValidateCustomer")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> ValidateCustomerById(string id)
        {
            return await _unitOfWork.CustomerRepository.AnyAsync(x => x.Id.ToString() == id);
        }
    }
}
