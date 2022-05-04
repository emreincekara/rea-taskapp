using Catalog.API.Data.Entities.Common;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.API.Data.Entities
{
    public class Category : BaseEntity
    {
        public Category()
        {
            this.Products = new HashSet<Product>();
        }

        public string Name { get; set; }

        [BsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}
