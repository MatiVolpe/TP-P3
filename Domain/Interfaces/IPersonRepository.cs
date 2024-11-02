﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPersonRepository
    {
        IEnumerable<Person> GetAll();
        Person? GetById(int id);
        void Add(Person person);
        void Update(Person person);
        void Delete(int id);
    }
}
