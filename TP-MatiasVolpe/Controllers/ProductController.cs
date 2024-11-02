using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace TP_MatiasVolpe.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            return Ok(await _productService.GetAllProductsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<ProductDto>> GetByName(string name)
        {
            var product = await _productService.GetProductByNameAsync(name);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductDto productDto)
        {
            await _productService.CreateProductAsync(productDto);
            return CreatedAtAction(nameof(GetById), new { id = productDto.IdProduct }, productDto);
        }

        [HttpPut("{id}/price")]
        public async Task<IActionResult> ChangePrice(int id, [FromBody] decimal newPrice)
        {
            await _productService.UpdateProductPriceAsync(id, newPrice);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
