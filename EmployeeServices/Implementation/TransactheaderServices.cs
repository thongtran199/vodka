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
            var totalCash = transactheader.Net + (transactheader.Net * (totalRate / 100));
            transactheader.Total = totalCash;
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Transactheader> GetTransactheadersByUserId(string userId)
        {
            return _context.Transactheaders.Where(x => x.UserId.Equals(userId)).ToList();
        }

        public IEnumerable<Transactheader> GetTransactheadersByTimePayment(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public decimal? GetTotalRevenueByTimePayment(DateTime start, DateTime end)
        {
           return _context.Transactheaders
                        .Where(ts => ts.TimePayment >= start && ts.TimePayment <= end)
                        .ToList()
                        .Sum(ts => ts.Total);
            
        }

        public decimal? GetTotalRevenueLastMonth()
        {
            DateTime now = DateTime.Now;
            DateTime start = new DateTime(now.Year, now.Month, 1);
            DateTime end = now;

            return _context.Transactheaders
                .Where(ts => ts.TimePayment >= start && ts.TimePayment <= end)
                .Sum(ts => ts.Total);
        }

        public decimal? GetTotalRevenueLastYear()
        {
            DateTime now = DateTime.Now;
            DateTime start = new DateTime(now.Year, 1, 1);
            DateTime end = now;

            // Filter the transaction headers for the previous year and calculate the total
            return _context.Transactheaders
                .Where(ts => ts.TimePayment >= start && ts.TimePayment <= end)
                .Sum(ts => ts.Total);
        }

        public decimal? GetTotalRevenueLastWeek()
        {
            DateTime now = DateTime.Now;
            DateTime start = now.Date.AddDays(-(int)now.DayOfWeek - 6);
            DateTime end = start.AddDays(6);

            // Filter the transaction headers for the previous week and calculate the total
            return _context.Transactheaders
                .Where(ts => ts.TimePayment >= start && ts.TimePayment <= end)
                .Sum(ts => ts.Total);
        }
        public Transactheader GetGioHangHienTaiByUserId(string userid)
        {
            var giohang =  _context.Transactheaders
                .Where(ts => ts.Status == 0 && ts.UserId == userid)
                .FirstOrDefault();
            return giohang;
        }
        public async Task XacNhanMuaHang(string id)
        {
            var transactheader = GetById(id);
            if (transactheader != null && transactheader.Status == 0)
            {
                transactheader.Status = 1;
                await UpdateAsSync(transactheader);
            }
        }
    }
}
