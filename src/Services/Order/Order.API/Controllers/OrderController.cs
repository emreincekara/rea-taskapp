using Microsoft.AspNetCore.Mvc;
using Order.Application.DTOs.OrderDTOs;
using Order.Application.Interfaces.UnitOfWork;
using System.Net;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IList<DetailOrderDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<DetailOrderDTO>>> GetOrders()
        {
            var orders = await _unitOfWork.OrderRepository.GetAllAsync<DetailOrderDTO>();

            if (orders == null)
                return NotFound();

            return Ok(orders);
        }

        [HttpGet("{id:length(36)}", Name = "GetOrder")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(DetailOrderDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<DetailOrderDTO>> GetOrdersById(string id)
        {
            var order = await _unitOfWork.OrderRepository.GetSingleAsync<DetailOrderDTO>(x => x.Id.ToString() == id);

            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpGet("[action]/{customerId:length(36)}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IList<DetailOrderDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<DetailOrderDTO>>> GetOrdersByCustomerId(string customerId)
        {
            var orders = await _unitOfWork.OrderRepository.GetByAsync<DetailOrderDTO>(x => x.CustomerId.ToString() == customerId);

            if (orders == null)
                return NotFound();

            return Ok(orders);
        }

        [HttpPost]
        [ProducesResponseType(typeof(DetailOrderDTO), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(DetailOrderDTO), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<DetailOrderDTO>> AddOrder([FromBody] AddOrderDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var addedOrder = await _unitOfWork.OrderRepository.AddAsync(model);
            return CreatedAtRoute("GetOrder", new { id = addedOrder.Id }, addedOrder);
        }

        [HttpPut]
        [ProducesResponseType(typeof(DetailOrderDTO), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateOrder([FromBody] EditOrderDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(await _unitOfWork.OrderRepository.UpdateAsync(model));
        }

        [HttpDelete("{id:length(36)}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteOrder(string id)
        {
            if (!await _unitOfWork.OrderRepository.AnyAsync(x => x.Id.ToString() == id))
                return NotFound();
            return Ok(await _unitOfWork.OrderRepository.DeleteAsync(new Guid(id)));
        }

        [HttpPut("{id:length(36)}/{status}", Name = "ChangeStatus")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> ChangeStatus(string id, string status)
        {
            var order = await _unitOfWork.OrderRepository.GetSingleAsync(x => x.Id.ToString() == id);

            if (order == null)
                return NotFound();

            order.Status = status;
            order.UpdatedAt = DateTime.UtcNow;

            return Ok(await _unitOfWork.OrderRepository.UpdateAsync(order));
        }
    }
}
