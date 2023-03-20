using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApiWebCore.Data;
using MyApiWebCore.Models;
using MyApiWebCore.Repositories.IRepository;

namespace MyApiWebCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.Admin)]
    public class ProductController : ControllerBase
    {
        private IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            try
            {
                return Ok(await _productRepository.GetAllProductAsync());
            } 
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{page}/{pageSize}/{filter}")]
        public async Task<IActionResult> GetProductsFilter(int page,int pageSize, string filter)
        {
            try
            {
                return Ok(await _productRepository.GetProductFilter(page,pageSize,filter));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _productRepository.GetProductAsync(id);
                return product == null ? NotFound() : Ok(product);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductModel model)
        {
            try
            {
                var newProduct = await _productRepository.AddProductAsync(model);
                var product = await _productRepository.GetProductAsync(newProduct);
                return product == null ? NotFound() : Ok(product);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id,ProductModel model)
        {
            try
            {
               await _productRepository.UpdateProductAsync(id, model);
               return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productRepository.DeleteProductAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
            
        }
    }
}
