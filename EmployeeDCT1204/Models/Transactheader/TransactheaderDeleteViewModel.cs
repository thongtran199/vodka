namespace Vodka.Models.Transactheader
{
    public class TransactheaderDeleteViewModel
    {
        public string? TransactHeaderId { get; set; }
        public decimal? Net { get; set; }

        public int? Tax1 { get; set; }

        public int? Tax2 { get; set; }

        public int? Tax3 { get; set; }

        public decimal? Total { get; set; }

        public DateTime? TimePayment { get; set; }

        public int? Status { get; set; }
        public string? UserId { get; set; }


    }
}
