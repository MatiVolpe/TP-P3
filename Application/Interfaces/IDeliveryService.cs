using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDeliveryService
    {
        Task<DeliveryDto> GetByIdDeliveryAsync(int id);
        Task<DeliveryDto> CreateDeliveryAsync(CreateDeliveryDto deliveryDto);
        Task<bool> DeleteDeliveryAsync(int id);
    }
}
