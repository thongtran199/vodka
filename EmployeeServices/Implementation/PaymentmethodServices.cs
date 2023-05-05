using VodkaDataAccess;
using VodkaEntities;

namespace VodkaServices.Implementation
{
    public class PaymentmethodServices : IPaymentmethodService
    {
        private ApplicationDbContext _context;
        public PaymentmethodServices(ApplicationDbContext context){
            _context = context;
        }

        public async Task CreateAsSync(Paymentmethod paymentmethod)
        {
            await _context.Paymentmethods.AddAsync(paymentmethod);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Paymentmethod> GetAll()
        {
            return _context.Paymentmethods.ToList();
        }

        public Paymentmethod GetById(string id)
        {
            return _context.Paymentmethods.Where(x => x.PaymentMethodId.Equals(id)).FirstOrDefault();
        }


        public async Task UpdateById(string id)
        {
            var Paymentmethod = GetById(id);
            if( Paymentmethod != null)
            {
                _context.Paymentmethods.Update(Paymentmethod);
                await _context.SaveChangesAsync();
            }

        }

        public async Task DeleteById(string id)
        {
            var paymentmethod = GetById(id);
            _context.Paymentmethods.Update(paymentmethod);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsSync(Paymentmethod Paymentmethod)
        {
            _context.Paymentmethods.Update(Paymentmethod);
            await _context.SaveChangesAsync();
        }
        public int GetLastId()
        {
            var p = _context.Paymentmethods.OrderByDescending(i => i.PaymentMethodId).FirstOrDefault();
            return int.Parse(p.PaymentMethodId.Replace("P", ""));
        }

    }
}
