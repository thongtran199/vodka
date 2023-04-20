using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VodkaEntities;

namespace VodkaServices
{
    public interface IUseraccountService
    {
        public IEnumerable<Useraccount> GetAll();
        public Useraccount GetById(string id);
        public Task DeleteById(string id);
        public Task UpdateAsSync(Useraccount useraccount);
        public Task UpdateById(string id);
        public Task CreateAsSync(Useraccount useraccount);

        int GetLastId();
    }
}
