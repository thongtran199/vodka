using EmployeeEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeServices
{
    public interface ITaxYearService
    {
        Task UpdateAsSync(TaxYear taxYear);
        Task UpdateById(int id);
        Task DeleteById(int id);
        Task CreateAsSync(TaxYear taxYear);

        TaxYear GetById(int id);

        IEnumerable<TaxYear> GetAll();
    }
}
