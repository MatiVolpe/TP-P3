using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class DeliveryDto
    {
        public int IdDelivery { get; set; }
        public int IdPerson { get; set; }
        public int IdProduct { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public int Type { get; set; }
    }

    public class CreateDeliveryDto
    {
        public int IdPerson { get; set; }
        public int IdProduct { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public int Type { get; set; }
    }

}
