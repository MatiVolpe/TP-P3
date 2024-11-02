using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Person> GetAll()
        {
            return _context.Persons.ToList();
        }

        public Person? GetById(int id)
        {
            return _context.Persons.Find(id);
        }

        public void Add(Person person)
        {
            _context.Persons.Add(person); 
            _context.SaveChanges();
        }

        public void Update(Person person)
        {
            _context.Persons.Update(person); 
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var person = _context.Persons.Find(id);
            if (person != null)
            {
                _context.Persons.Remove(person);
                _context.SaveChanges();
            }
        }

        public Person? Authenticate(string email, string password, int role)
        {
            Person? personToAuthenticate = _context.Persons.FirstOrDefault(p => p.Email == email && p.Password == password && p.Role == role);
            return personToAuthenticate;
        }
    }

}
