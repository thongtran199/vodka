using System.ComponentModel.DataAnnotations.Schema;

namespace Vodka.Models.Transactdetail
{
    public class TransactdetailDeleteViewModel
    {
        public string? TransactDetailId { get; set; }

        public string? CostEach { get; set; }

        public int? Quan { get; set; }

        public string? Status { get; set; }

        public string? TransactId { get; set; }

        public string? ProductNum { get; set; }
    }
}
