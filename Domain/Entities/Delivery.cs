using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Delivery
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDelivery { get; set; }
        public int IdPerson { get; set; }
        public int IdProduct { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public int Type { get; set; }

    }
}
