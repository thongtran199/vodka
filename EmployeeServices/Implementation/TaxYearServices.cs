using EmployeeDataAccess;
using EmployeeEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeServices.Implementation
{
    public class TaxYearServices : ITaxYearService
    {
        private ApplicationDbContext _context;
        public TaxYearServices(ApplicationDbContext context){
            _context = context;
        }

        public async Task CreateAsSync(TaxYear taxYear)
        {
            await _context.TaxYear.AddAsync(taxYear);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<TaxYear> GetAll()
        {
            return _context.TaxYear.ToList();
        }

        public TaxYear GetById(int id)
        {
            return _context.TaxYear.Where(x => x.Id == id).FirstOrDefault();
        }


        public async Task UpdateById(int id)
        {
            var taxYear = GetById(id);
            if( taxYear != null)
            {
                _context.TaxYear.Update(taxYear);
                await _context.SaveChangesAsync();
            }

        }

        public async Task DeleteById(int id)
        {
            var taxYear = GetById(id);
            _context.TaxYear.Remove(taxYear);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsSync(TaxYear taxYear)
        {
            _context.TaxYear.Update(taxYear);
            await _context.SaveChangesAsync();
        }
    }
}
