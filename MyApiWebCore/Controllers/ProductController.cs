using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApiWebCore.Models;
using MyApiWebCore.Repositories;

namespace MyApiWebCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product =await _productRepository.GetProductAsync(id);
            return product==null ? NotFound() : Ok(product);
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
            if (id == model.Id)
            {
                 await _productRepository.UpdateProductAsync(id, model);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            
                await _productRepository.DeleteProductAsync(id);
                return Ok();
            
        }
    }
}
