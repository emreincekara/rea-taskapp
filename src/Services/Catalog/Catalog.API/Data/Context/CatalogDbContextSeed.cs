using Catalog.API.Data.Entities.Common;
using Catalog.API.Data.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data.Context
{
    public class CatalogDbContextSeed<T> where T : BaseEntity
    {
        public static void SeedData(IMongoCollection<T> collection)
        {
            var isExist = collection.Find(x => true).Any();
            if (!isExist)
            {
                if (collection.CollectionNamespace.CollectionName == typeof(Category).Name)
                    collection.InsertMany((IEnumerable<T>)GetPreConfiguredCategories());
                else if(collection.CollectionNamespace.CollectionName == typeof(Product).Name)
                    collection.InsertMany((IEnumerable<T>)GetPreConfiguredProducts());
            }
        }

        private static IEnumerable<Category> GetPreConfiguredCategories()
        {
            return new List<Category>()
            {
                new Category()
                {
                    Id = "4eacd885-5b87-493b-befb-4bc53dc49eb7",
                    Name = "Bilgisayar"
                },
                new Category()
                {
                    Id = "584bd799-19e1-49c6-b61e-b3718d61d84e",
                    Name = "Telefon"
                }
            };
        }

        private static IEnumerable<Product> GetPreConfiguredProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = "82d9a1cb-8db1-46ec-b940-21767ce8a05f",
                    Name = "iPhone 11 128 GB",
                    ImageUrl = "",
                    Price = 12199.01,
                    CategoryId = "584bd799-19e1-49c6-b61e-b3718d61d84e"
                },
                new Product()
                {
                    Id = "442ce399-772e-42d7-8a69-3030f160fb13",
                    Name = "Samsung Galaxy S21 128 GB",
                    ImageUrl = "",
                    Price = 8989,
                    CategoryId = "584bd799-19e1-49c6-b61e-b3718d61d84e"
                },
                new Product()
                {
                    Id = "4a0483d9-79c9-4867-bffc-5cd93bedc112",
                    Name = "iPhone 11 64 GB",
                    ImageUrl = "",
                    Price = 11295.50,
                    CategoryId = "584bd799-19e1-49c6-b61e-b3718d61d84e"
                },
                new Product()
                {
                    Id = "afbc9806-c320-4beb-8bae-6ea5e6310fea",
                    Name = "iPhone 13 128 GB",
                    ImageUrl = "",
                    Price = 17980.80,
                    CategoryId = "584bd799-19e1-49c6-b61e-b3718d61d84e"
                },
                new Product()
                {
                    Id = "ee1c863e-c3fd-4585-9f46-f28355866f8d",
                    Name = "Oppo A74 128 GB",
                    ImageUrl = "",
                    Price = 3799,
                    CategoryId = "584bd799-19e1-49c6-b61e-b3718d61d84e"
                },
                new Product()
                {
                    Id = "d3e224ea-f0d1-4b79-93ad-a0e7bedd5de5",
                    Name = "Lenovo Ideapad",
                    ImageUrl = "",
                    Price = 3569,
                    CategoryId = "4eacd885-5b87-493b-befb-4bc53dc49eb7"
                },
                new Product()
                {
                    Id = "8d16ceb7-3e2c-4373-9bc8-bb830fd79b27",
                    Name = "MSI Katana",
                    ImageUrl = "",
                    Price = 27250.88,
                    CategoryId = "4eacd885-5b87-493b-befb-4bc53dc49eb7"
                }
            };
        }
    }
}
