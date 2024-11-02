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

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
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

        public async Task CreateAsync(SupplierDto supplierDto)
        {
            var supplier = new Supplier { Company = supplierDto.Company };
            await _supplierRepository.CreateAsync(supplier);
        }

        public async Task DeleteAsync(int id)
        {
            await _supplierRepository.DeleteAsync(id);
        }
    }
}
