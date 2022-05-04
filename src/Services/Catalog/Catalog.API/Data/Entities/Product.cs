using Catalog.API.Data.Entities.Common;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.API.Data.Entities
{
    public class Product : BaseEntity
    {
        [BsonElement("Name")]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public string CategoryId { get; set; }

        [BsonIgnore]
        public virtual Category Category { get; set; }
    }
}
