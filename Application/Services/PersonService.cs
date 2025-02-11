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

        public bool ChangePassword(int id, string newPassword)
        {
            var person = _personRepository.GetById(id);
            if (person == null)
            {
                return false;
            }

            person.Password = newPassword;
            _personRepository.Update(person);
            return true;
        }


        public bool ChangeShift(int id, string newShift)
        {
            if (!string.Equals(newShift, "day", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(newShift, "night", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("El turno debe ser 'day' o 'night'.");
            }

            var person = _personRepository.GetById(id);
            if (person == null)
            {
                return false;
            }

            person.Shift = newShift;
            _personRepository.Update(person);
            return true;
        }


        public void Create(CreatePersonDto dto)
        {
            if (dto.Role != 1 && dto.Role != 2)
            {
                throw new ArgumentException("El rol debe ser 1 (usuario normal) o 2 (administrador).");
            }

            if (!string.Equals(dto.Shift, "day", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(dto.Shift, "night", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("El turno a elegir debe ser day (para el turno de día) o night (para el turno noche).");
            }

            var existingPerson = _personRepository.GetByEmail(dto.Email);
            if (existingPerson != null)
            {
                throw new ArgumentException("El correo electrónico ya está en uso.");
            }

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


        public bool Delete(int id)
        {
            var person = _personRepository.GetById(id);
            if (person == null)
            {
                return false;
            }

            _personRepository.Delete(id);
            return true;
        }
    }

}
