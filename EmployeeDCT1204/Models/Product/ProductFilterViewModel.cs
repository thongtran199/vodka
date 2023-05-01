using Org.BouncyCastle.Crypto.Parameters;
using VodkaEntities;

namespace Vodka.Models.Product
{
    public class ProductFilterViewModel
    {
        public string? minPrice { get; set; }

        public string? maxPrice { get; set; }

        public string? CatId { get; set; }
    }
}
