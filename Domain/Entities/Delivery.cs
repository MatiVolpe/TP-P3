using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Delivery
    {
        public int IdDelivery { get; set; }
        public int IdPerson { get; set; }
        public int IdProduct { get; set; }
        public DateTime Date { get; set; }

    }
}
