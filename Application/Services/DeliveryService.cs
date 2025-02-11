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
    public class DeliveryService : IDeliveryService
    {
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IPersonRepository _personRepository;

        public DeliveryService(IDeliveryRepository deliveryRepository, IProductRepository productRepository, IPersonRepository personRepository)
        {
            _deliveryRepository = deliveryRepository;
            _productRepository = productRepository;
            _personRepository = personRepository;
        }

        public async Task<DeliveryDto?> GetByIdDeliveryAsync(int id)
        {
            var delivery = await _deliveryRepository.GetByIdAsync(id);
            if (delivery == null) return null;

            return new DeliveryDto
            {
                IdDelivery = delivery.IdDelivery,
                IdPerson = delivery.IdPerson,
                IdProduct = delivery.IdProduct,
                Quantity = delivery.Quantity,
                Date = delivery.Date,
                Type = delivery.Type
            };
        }

        public async Task<DeliveryDto> CreateDeliveryAsync(CreateDeliveryDto deliveryDto)
        {
            // Validate if Person exists
            var personExists =  _personRepository.GetById(deliveryDto.IdPerson);
            if (personExists == null)
            {
                throw new ArgumentException($"No se encontró una persona con ID = {deliveryDto.IdPerson}.");
            }

            // Validate if Product exists
            var product = await _productRepository.GetByIdAsync(deliveryDto.IdProduct);
            if (product == null)
            {
                throw new ArgumentException($"No se encontró un producto con ID = {deliveryDto.IdProduct}.");
            }

            // Create Delivery Object
            var delivery = new Delivery
            {
                IdPerson = deliveryDto.IdPerson,
                IdProduct = deliveryDto.IdProduct,
                Quantity = deliveryDto.Quantity,
                Date = deliveryDto.Date,
                Type = deliveryDto.Type
            };

            // Stock Logic
            if (delivery.Type == 1) // Entrada de producto (Incrementa Stock)
            {
                product.Stock += delivery.Quantity;
            }
            else if (delivery.Type == 2) // Salida de Producto (Reduce Stock)
            {
                if (product.Stock < delivery.Quantity)
                    throw new ArgumentException("Stock insuficiente para realizar la entrega.");

                product.Stock -= delivery.Quantity;
            }

            await _productRepository.UpdateAsync(product);
            await _deliveryRepository.AddAsync(delivery);

            return new DeliveryDto
            {
                IdDelivery = delivery.IdDelivery,
                IdPerson = delivery.IdPerson,
                IdProduct = delivery.IdProduct,
                Quantity = delivery.Quantity,
                Date = delivery.Date,
                Type = delivery.Type
            };
        }


        public async Task<bool> DeleteDeliveryAsync(int id)
        {
            var delivery = await _deliveryRepository.GetByIdAsync(id);
            if (delivery == null)
            {
                return false; // Delivery does not exist
            }

            await _deliveryRepository.DeleteAsync(id);
            return true; // Delivery deleted successfully
        }

    }
}

