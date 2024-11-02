using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPersonService
    {
        IEnumerable<PersonDto> GetAll();
        PersonDto? GetById(int id);
        void ChangePassword(int id, string newPassword);
        void ChangeShift(int id, string newShift);
        void Create(CreatePersonDto dto);
        void Delete(int id);
    }

}
