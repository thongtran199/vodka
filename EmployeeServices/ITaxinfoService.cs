using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VodkaEntities;

namespace VodkaServices
{
    public interface ITaxinfoService
    {
        Task CreateAsSync(Taxinfo taxinfo);
        IEnumerable<Taxinfo> GetAll();
        Taxinfo GetById(string id);
        Task UpdateById(string id);
        Task DeleteById(string id);
        Task UpdateAsSync(Taxinfo taxinfo);
        int GetLastId();
    }
}
