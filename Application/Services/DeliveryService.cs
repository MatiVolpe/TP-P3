using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public DeliveryService(IDeliveryRepository deliveryRepository, IProductRepository productRepository, IMapper mapper)
        {
            _deliveryRepository = deliveryRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<DeliveryDto> GetByIdDeliveryAsync(int id)
        {
            var delivery = await _deliveryRepository.GetByIdAsync(id);
            return _mapper.Map<DeliveryDto>(delivery);
        }

        public async Task CreateDeliveryAsync(DeliveryDto deliveryDto)
        {
            var delivery = _mapper.Map<Delivery>(deliveryDto);

            var product = await _productRepository.GetByIdAsync(delivery.IdProduct);
            if (product == null)
                throw new Exception("Product not found.");

            // Adjust product stock based on delivery type
            if (delivery.Type == 1) // Entrance
            {
                product.Stock += delivery.Quantity;
            }
            else if (delivery.Type == 2) // Exit
            {
                if (product.Stock < delivery.Quantity)
                    throw new Exception("Insufficient stock for delivery.");
                product.Stock -= delivery.Quantity;
            }

            await _productRepository.UpdateAsync(product);
            await _deliveryRepository.AddAsync(delivery);
        }

        public async Task DeleteDeliveryAsync(int id)
        {
            await _deliveryRepository.DeleteAsync(id);
        }
    }
}
