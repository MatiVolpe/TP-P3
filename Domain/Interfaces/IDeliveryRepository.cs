using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IDeliveryRepository
    {
        Task<Delivery> GetByIdAsync(int id);
        Task AddAsync(Delivery delivery);
        Task DeleteAsync(int id);
    }
}
