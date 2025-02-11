using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ProductDto
    {
        public int IdProduct { get; set; }
        public string? ProductName { get; set; }
        public int Stock { get; set; }
        public int IdSupplier { get; set; }
    }

    public class CreateProductDto
    {
        public string ProductName { get; set; }
        public int Stock { get; set; }
        public int IdSupplier { get; set; } // Ensure this Supplier exists
    }

}
