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
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IProductRepository _productRepository;

        public SupplierService(ISupplierRepository supplierRepository, IProductRepository productRepository)
        {
            _supplierRepository = supplierRepository;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<SupplierDto>> GetAllAsync()
        {
            var suppliers = await _supplierRepository.GetAllAsync();
            return suppliers.Select(s => new SupplierDto { IdSupplier = s.IdSupplier, Company = s.Company });
        }

        public async Task<SupplierDto?> GetByIdAsync(int id)
        {
            var supplier = await _supplierRepository.GetByIdAsync(id);
            return supplier == null ? null : new SupplierDto { IdSupplier = supplier.IdSupplier, Company = supplier.Company };
        }

        public async Task<SupplierDto> CreateAsync(CreateSupplierDto supplierDto)
        {
            var supplier = new Supplier { Company = supplierDto.Company };

            await _supplierRepository.CreateAsync(supplier);

            return new SupplierDto { IdSupplier = supplier.IdSupplier, Company = supplier.Company };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var supplier = await _supplierRepository.GetByIdAsync(id);
            if (supplier == null)
            {
                return false;
            }

            bool hasProducts = await _productRepository.AnyAsync(p => p.IdSupplier == id);
            if (hasProducts)
            {
                throw new InvalidOperationException("Cannot delete supplier because there are products associated with it.");
            }

            await _supplierRepository.DeleteAsync(id);
            return true;
        }
    }
}
