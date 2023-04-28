using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VodkaEntities;

namespace VodkaServices
{
    public interface ITransactdetailService
    {
        Task CreateAsSync(Transactdetail transactdetail);
        IEnumerable<Transactdetail> GetAll();
        Transactdetail GetById(string id);
        Task UpdateById(string id);
        Task DeleteById(string id);
        Task UpdateAsSync(Transactdetail transactdetail);
        int GetLastId();

        IEnumerable<Transactdetail> GetTransactdetailsByTransactheaderId(string id);
    }
}
