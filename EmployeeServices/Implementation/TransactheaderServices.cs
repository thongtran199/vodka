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
        private ITransactdetailService _transactdetailService;
        private IVodkaUserService _vodkaUserService;
        private IProductService _productService;
        public TransactheaderServices(ApplicationDbContext context, ITransactdetailService transactdetailService, IProductService productService, IVodkaUserService vodkaUserService)
        {
            _context = context;
            _productService = productService;
            _transactdetailService = transactdetailService;
            _vodkaUserService = vodkaUserService;
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
            var userId = transactheader.UserId;
            if (transactheader != null)
            {
                transactheader.Status = 3;
                _context.Transactheaders.Update(transactheader);

                await CreateNewEmptyTransactheader(userId);

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
            var userId = transactheader.UserId;
            if (transactheader != null && transactheader.Status == 0)
            {
                transactheader.Status = 1;

                await CreateNewEmptyTransactheader(userId);

                await UpdateAsSync(transactheader);
            }
        }

        public async Task CreateNewEmptyTransactheader(string userId)
        {
            string new_str_id = "";
            int new_int_id = GetLastId() + 1;
            if (new_int_id >= 100 && new_int_id < 1000)
                new_str_id = "TS00" + new_int_id.ToString();
            else if (new_int_id < 10 && new_int_id >= 0)
                new_str_id = "TS0000" + new_int_id.ToString();
            else if (new_int_id < 100 && new_int_id >= 10)
                new_str_id = "TS000" + new_int_id.ToString();

            var transactheader = new Transactheader
            {
                TransactHeaderId = new_str_id,
                Net = 0,
                Tax1 = 0,
                Tax2 = 0,
                Tax3 = 0,
                Total = 0,
                TimePayment = DateTime.Now,
                UserId = userId,
                Status = 0
            };

            await CreateAsSync(transactheader);
        }

        public async Task<string> BanGiaoShipper(string transactHeaderId)
        {
            var transactheader = GetById(transactHeaderId);
            if (transactheader != null && transactheader.Status == 1)
            {
                var transactdetails = _transactdetailService.GetTransactdetailsByTransactheaderId(transactheader.TransactHeaderId);
                if (transactdetails != null)
                {
                    foreach (Transactdetail detail in transactdetails)
                    {
                        var product = _productService.GetById(detail.ProductId);
                        var sl_conlai = product.Quan - detail.Quan;
                        if (sl_conlai >= 0)
                        {
                            product.Quan = sl_conlai;
                        }
                        else
                            return "Số lượng sản phẩm " + product.ProductId + " không đủ";
                        await _productService.UpdateAsSync(product);
                    }
                }
                var user = await _vodkaUserService.FindByIdAsync(transactheader.UserId);
                user.TotalCash = user.TotalCash + transactheader.Total;

                await _vodkaUserService.UpdateAsSync(user);

                transactheader.Status = 2;

                await CreateNewEmptyTransactheader(user.Id);

                await UpdateAsSync(transactheader);
                return "";
            }
            return "Đơn hàng không tìm thấy hoặc chưa xác nhận mua hoặc đã giao cho shipper hoặc đã bị xóa !";
        }
    }
}
