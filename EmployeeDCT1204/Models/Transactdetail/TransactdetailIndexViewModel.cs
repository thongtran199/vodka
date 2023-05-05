using System.ComponentModel.DataAnnotations.Schema;

namespace Vodka.Models.Transactdetail
{
    public class TransactdetailIndexViewModel
    {
        public string? TransactDetailId { get; set; }

        public decimal? CostEach { get; set; }

        public decimal? Total { get; set; }

        public int? Quan { get; set; }


        public string? TransactHeaderId { get; set; }

        public string? ProductId { get; set; }
    }
}
