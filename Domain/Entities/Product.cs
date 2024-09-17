using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product
    {
        public int IdProduct { get; set; }
        public string ProductName { get; set; }
        public float Price { get; set; }
        public int Stock {  get; set; }
        public int IdPerson { get; set; }

    }
}
