using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public IEnumerable<PersonDto> GetAll()
        {
            var persons = _personRepository.GetAll();
            return persons.Select(p => new PersonDto
            {
                IdPerson = p.IdPerson,
                Name = p.Name,
                Email = p.Email,
                Phone = p.Phone,
                Role = p.Role,
                Shift = p.Shift
            });
        }

        public PersonDto? GetById(int id)
        {
            var person = _personRepository.GetById(id);
            if (person == null)
            {
                return null; 
            }

            return new PersonDto
            {
                IdPerson = person.IdPerson,
                Name = person.Name,
                Email = person.Email,
                Phone = person.Phone,
                Role = person.Role,
                Shift = person.Shift
            };
        }

        public void ChangePassword(int id, string newPassword)
        {
            var person = _personRepository.GetById(id);
            if (person != null)
            {
                person.Password = newPassword; 
                _personRepository.Update(person);
            }
        }

        public void ChangeShift(int id, string newShift)
        {
            var person = _personRepository.GetById(id);
            if (person != null)
            {
                person.Shift = newShift;
                _personRepository.Update(person);
            }
        }

        public void Create(CreatePersonDto dto)
        {
            var person = new Person
            {
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Password = dto.Password, 
                Role = dto.Role,
                Shift = dto.Shift
            };

            _personRepository.Add(person);
        }

        public void Delete(int id)
        {
            _personRepository.Delete(id);
        }
    }

}
