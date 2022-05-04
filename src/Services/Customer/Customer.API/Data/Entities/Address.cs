using Customer.API.Data.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Customer.API.Data.Entities
{
    [Table("Address")]
    public class Address : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public string? AddressLine { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        public int CityCode { get; set; }

        [JsonIgnore]
        [ForeignKey("CustomerId")]
        public virtual CustomerDetail? Customer { get; set; }
    }
}
