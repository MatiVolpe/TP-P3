using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class SupplierDto
    {
        public int IdSupplier { get; set; }
        public string? Company { get; set; }
    }

    public class CreateSupplierDto
    {
        public string Company { get; set; }
    }

}
