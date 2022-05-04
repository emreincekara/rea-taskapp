using System.ComponentModel.DataAnnotations.Schema;

namespace Customer.API.Data.Entities.Common
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
    }
}
