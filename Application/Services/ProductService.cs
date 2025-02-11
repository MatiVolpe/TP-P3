using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ISupplierRepository _supplierRepository;

        public ProductService(IProductRepository productRepository, ISupplierRepository supplierRepository)
        {
            _productRepository = productRepository;
            _supplierRepository = supplierRepository;
        }


        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(product => new ProductDto
            {
                IdProduct = product.IdProduct,
                ProductName = product.ProductName,
                Stock = product.Stock,
                IdSupplier = product.IdSupplier
            }).ToList();
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return null;

            return new ProductDto
            {
                IdProduct = product.IdProduct,
                ProductName = product.ProductName,
                Stock = product.Stock,
                IdSupplier = product.IdSupplier
            };
        }

        public async Task<IEnumerable<ProductDto>> GetProductByNameAsync(string name)
        {
            var products = await _productRepository.GetByNameAsync(name);

            return products.Select(product => new ProductDto
            {
                IdProduct = product.IdProduct,
                ProductName = product.ProductName,
                Stock = product.Stock,
                IdSupplier = product.IdSupplier
            }).ToList();
        }


        public async Task<ProductDto> CreateProductAsync(CreateProductDto productDto)
        {
            var supplierExists = await _supplierRepository.GetByIdAsync(productDto.IdSupplier);
            if (supplierExists == null)
            {
                throw new ArgumentException($"No se encontró un proveedor con ID = {productDto.IdSupplier}.");
            }

            var product = new Product
            {
                ProductName = productDto.ProductName,
                Stock = productDto.Stock,
                IdSupplier = productDto.IdSupplier
            };

            await _productRepository.CreateAsync(product);

            return new ProductDto
            {
                IdProduct = product.IdProduct,
                ProductName = product.ProductName,
                Stock = product.Stock,
                IdSupplier = product.IdSupplier
            };
        }



        public async Task DeleteProductAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
        }
    }
}

