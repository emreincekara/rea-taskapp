using Catalog.API.Data.Entities;
using Catalog.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CatalogController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IList<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<Product>>> GetProducts()
        {
            var products = (await _unitOfWork.ProductRepository.GetAllAsync()).Select(x =>
            {
                x.Category = _unitOfWork.CategoryRepository.GetByIdAsync(x.CategoryId).Result;
                return x;
            });

            return Ok(products);
        }

        [HttpGet("{id:length(36)}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null)
                return NotFound();
            product.Category = await _unitOfWork.CategoryRepository.GetByIdAsync(product.CategoryId);
            return Ok(product);
        }

        [HttpGet("[action]/{name}", Name = "GetProductByName")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IList<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IList<Product>>> GetProductByName(string name)
        {
            var products = (await _unitOfWork.ProductRepository.GetByAsync(x => x.Name.ToLower().Contains(name.ToLower()))).Select(x =>
            {
                x.Category = _unitOfWork.CategoryRepository.GetByIdAsync(x.CategoryId).Result;
                return x;
            });
            if (products == null)
                return NotFound();
            return Ok(products);
        }

        [HttpGet("[action]/{categoryName}", Name = "GetProductByCategory")]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string categoryName)
        {
            var category = (await _unitOfWork.CategoryRepository.GetByAsync(x => x.Name == categoryName)).FirstOrDefault();
            if (category == null)
                return NotFound();
            var products = (await _unitOfWork.ProductRepository.GetByAsync(x => x.CategoryId == category.Id)).Select(x =>
            {
                x.Category = _unitOfWork.CategoryRepository.GetByIdAsync(x.CategoryId).Result;
                return x;
            });
            return Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Product>> AddProduct([FromBody] Product product)
        {
            var addedProduct = await _unitOfWork.ProductRepository.AddAsync(product);
            return CreatedAtRoute("GetProduct", new { id = addedProduct.Id }, addedProduct);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            return Ok(await _unitOfWork.ProductRepository.UpdateAsync(product));
        }

        [HttpDelete("{id:length(36)}", Name = "DeleteProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProductById(string id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null)
                return NotFound();
            return Ok(await _unitOfWork.ProductRepository.DeleteAsync(id));
        }
    }
}
