using VodkaEntities;
using System.ComponentModel.DataAnnotations;

namespace Vodka.Models.Product
{
    public class ProductEditViewModel
    {
        public string? ProductNum { get; set; }

        public string? ProductName { get; set; }

        public string? Descript { get; set; }

        public string? Price { get; set; }
        public string? Tax1 { get; set; }

        public string? Tax2 { get; set; }

        public string? Tax3 { get; set; }

        public string? Quan { get; set; }

        public string? ImageSource { get; set; }
        public string? CatId { get; set; }


    }
}
