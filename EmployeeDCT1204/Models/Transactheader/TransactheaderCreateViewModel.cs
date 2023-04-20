namespace Vodka.Models.Transactheader
{
    public class TransactheaderCreateViewModel
    {
        public string? Net { get; set; }

        public string? Tax1 { get; set; }

        public string? Tax2 { get; set; }

        public string? Tax3 { get; set; }

        public string? Total { get; set; }

        public DateTime? TimePayment { get; set; }

        public string? WhoPay { get; set; }

        public string? Status { get; set; }
    }
}
