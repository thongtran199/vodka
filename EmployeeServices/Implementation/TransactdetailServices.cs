using VodkaDataAccess;
using VodkaEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VodkaServices.Implementation
{
    public class TransactdetailServices : ITransactdetailService
    {
        private ApplicationDbContext _context;
        public TransactdetailServices(ApplicationDbContext context){
            _context = context;
        }

        public async Task CreateAsSync(Transactdetail transactdetail)
        {
            await _context.Transactdetails.AddAsync(transactdetail);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Transactdetail> GetAll()
        {
            return _context.Transactdetails.ToList();
        }
        public IEnumerable<Transactdetail> GetTransactdetailsByTransactheaderId(string id)
        {
            return _context.Transactdetails.Where(x => x.TransactHeaderId.Equals(id)).ToList();
        }

        public Transactdetail GetById(string id)
        {
            return _context.Transactdetails.Where(x => x.TransactDetailId.Equals(id))
                                      .FirstOrDefault();
        }


        public async Task UpdateById(string id)
        {
            var transactdetail = GetById(id);
            if( transactdetail != null)
            {
                _context.Transactdetails.Update(transactdetail);
                await _context.SaveChangesAsync();
            }

        }

        public async Task DeleteById(string id)
        {
            var transactdetail = GetById(id);
            _context.Transactdetails.Remove(transactdetail);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsSync(Transactdetail transactdetail)
        {
            _context.Transactdetails.Update(transactdetail);
            await _context.SaveChangesAsync();
        }
        public int GetLastId()
        {
            var td = _context.Transactdetails.OrderByDescending(i => i.TransactDetailId).FirstOrDefault();
            return int.Parse(td.TransactDetailId.Replace("TD", ""));
        }
        
    }
}
