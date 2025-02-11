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
        bool ChangePassword(int id, string newPassword);
        bool ChangeShift(int id, string newShift);
        void Create(CreatePersonDto dto);
        bool Delete(int id);
    }

}
