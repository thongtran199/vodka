using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VodkaEntities;
namespace VodkaServices
{
    public interface ITransactheaderService
    {
        public IEnumerable<Transactheader> GetAll();
        public Transactheader GetById(string id);
        public IEnumerable<Transactheader> GetTransactheadersByUserId(string userId);
        public Task DeleteById(string id);
        public Task UpdateAsSync(Transactheader transactheader);
        public Task UpdateById(string id);
        public Task CreateAsSync(Transactheader transactheader);

        int GetLastId();
        Task UpdateTotalCash(Transactheader transactheader, decimal totalRate);
        IEnumerable<Transactheader> GetTransactheadersByTimePayment(DateTime start, DateTime end);

        decimal? GetTotalRevenueByTimePayment(DateTime start, DateTime end);

        decimal? GetTotalRevenueLastMonth();
        decimal? GetTotalRevenueLastYear();
        decimal? GetTotalRevenueLastWeek();

        public Transactheader GetGioHangHienTaiByUserId(string userid);

        Task XacNhanMuaHang(string id);

        Task CreateNewEmptyTransactheader(string userId);


        Task<string> BanGiaoShipper(string transactHeaderId);

    }
}
