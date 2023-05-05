using VodkaDataAccess;
using VodkaEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace VodkaServices.Implementation
{
    public class TransactheaderServices : ITransactheaderService
    {
        private ApplicationDbContext _context;
        public TransactheaderServices(ApplicationDbContext context){
            _context = context;
        }

        public async Task CreateAsSync(Transactheader transactheader)
        {
            await _context.Transactheaders.AddAsync(transactheader);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Transactheader> GetAll()
        {
            return _context.Transactheaders.ToList();
        }

        public Transactheader GetById(string id)
        {
            return _context.Transactheaders.Where(x => x.TransactHeaderId.Equals(id))
                                      .FirstOrDefault();
        }


        public async Task UpdateById(string id)
        {
            var transactheader = GetById(id);
            if( transactheader != null)
            {
                _context.Transactheaders.Update(transactheader);
                await _context.SaveChangesAsync();
            }

        }

        public async Task DeleteById(string id)
        {
            var transactheader = GetById(id);
            if (transactheader != null)
            {
                transactheader.Status = 3;
                _context.Transactheaders.Update(transactheader);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsSync(Transactheader transactheader)
        {
            _context.Transactheaders.Update(transactheader);
            await _context.SaveChangesAsync();
        }
        public int GetLastId()
        {
            var th = _context.Transactheaders.OrderByDescending(i => i.TransactHeaderId).FirstOrDefault();
            return int.Parse(th.TransactHeaderId.Replace("TS", ""));
        }
        public async Task UpdateTotalCash(Transactheader transactheader, decimal totalRate)
        {
            var totalCash = (transactheader.Net + transactheader.Net) * (totalRate / 100);
            transactheader.Total = totalCash;
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Transactheader> GetTransactheadersByUserId(string userId)
        {
            return _context.Transactheaders.Where(x => x.UserId.Equals(userId)).ToList();
        }
    }
}
