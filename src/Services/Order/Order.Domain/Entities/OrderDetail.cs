using Order.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Order.Domain.Entities
{
    [Table("Order")]
    public class OrderDetail : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Status { get; set; }
        public Guid AddressId { get; set; }
        public Guid ProductId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
