using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VodkaEntities;

namespace VodkaServices
{
    public interface IPaymentmethodService
    {
        public IEnumerable<Paymentmethod> GetAll();
        public Paymentmethod GetById(string id);
        public Task DeleteById(string id);
        public Task UpdateAsSync(Paymentmethod paymentmethod);
        public Task UpdateById(string id);
        public Task CreateAsSync(Paymentmethod paymentmethod);

        int GetLastId();
    }
}
