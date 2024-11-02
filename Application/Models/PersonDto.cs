using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class PersonDto
    {
        public int IdPerson { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int Role { get; set; }
        public string? Shift { get; set; }
    }
    public class CreatePersonDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public int Role { get; set; }
        public string? Shift { get; set; }
    }
    public class UpdatePersonDto
    {
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Shift { get; set; }
    }
}
