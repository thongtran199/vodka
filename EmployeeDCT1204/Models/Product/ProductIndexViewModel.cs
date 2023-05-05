using VodkaEntities;

namespace Vodka.Models.Product
{
    public class ProductIndexViewModel
    {
        public string? ProductId { get; set; }

        public string? Name { get; set; }

        public string? Descript { get; set; }

        public decimal? Price { get; set; }

        public int? Quan { get; set; }

        public int? IsActive { get; set; }

        public string? ImageSource { get; set; }
        public string? CategoryId { get; set; }
        public VodkaEntities.Category? Category { get; set; }
        public ICollection<VodkaEntities.Transactdetail>? TransactDetails { get; set; }
    }
}
