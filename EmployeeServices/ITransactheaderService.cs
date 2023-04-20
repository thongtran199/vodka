﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VodkaEntities;
namespace VodkaServices
{
    public interface ITransactheaderService
    {
        public IEnumerable<Transactheader> GetAll();
        public Transactheader GetById(string id);
        public Task DeleteById(string id);
        public Task UpdateAsSync(Transactheader transactheader);
        public Task UpdateById(string id);
        public Task CreateAsSync(Transactheader transactheader);

        int GetLastId();
    }
}
