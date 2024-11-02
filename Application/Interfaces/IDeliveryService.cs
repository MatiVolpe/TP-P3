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
        Task CreateDeliveryAsync(DeliveryDto deliveryDto);
        Task DeleteDeliveryAsync(int id);
    }
}
