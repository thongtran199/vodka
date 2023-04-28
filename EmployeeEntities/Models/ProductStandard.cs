using VodkaEntities;
namespace Vodka.Models.Product
    {
        public class ProductStandard
        {
            public string? ProductNum { get; set; }

            public string? ProductName { get; set; }

            public string? Descript { get; set; }

            public float Price { get; set; }
            public string? Tax1 { get; set; }

            public string? Tax2 { get; set; }

            public string? Tax3 { get; set; }

            public string? Quan { get; set; }

            public string? IsActive { get; set; }

            public string? ImageSource { get; set; }
            public string? CatId { get; set; }
            public VodkaEntities.Category? Category { get; set; }
            public ICollection<VodkaEntities.Transactdetail>? Transactdetail { get; set; }
        }
    }


