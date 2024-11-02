using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto?> GetProductByIdAsync(int id);
        Task<ProductDto?> GetProductByNameAsync(string name);
        Task CreateProductAsync(ProductDto productDto);
        Task UpdateProductPriceAsync(int id, decimal newPrice);
        Task DeleteProductAsync(int id);
    }
}
