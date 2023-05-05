using VodkaEntities;
using System.ComponentModel.DataAnnotations;

namespace Vodka.Models.Product
{
    public class ProductEditViewModel
    {
        public string? ProductId { get; set; }

        public string? Name { get; set; }

        public string? Descript { get; set; }

        public decimal? Price { get; set; }

        public int? Quan { get; set; }

        public string? ImageSource { get; set; }
        public string? CategoryId { get; set; }


    }
}
