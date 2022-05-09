namespace Order.Application.DTOs.OrderDTOs
{
    public class EditOrderDTO : BaseDTO
    {
        public Guid CustomerId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Status { get; set; }
        public Guid AddressId { get; set; }
        public Guid ProductId { get; set; }
    }
}
