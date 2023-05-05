using VodkaEntities;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Vodka.Models.Product
{
    public class ProductCreateViewModel
    {
        [Required(ErrorMessage = "Product Name is required"), StringLength(50, MinimumLength = 2)]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Descript is required"), StringLength(500, MinimumLength = 2)]
        public string? Descript { get; set; }
        [Required(ErrorMessage = "Price is required"), StringLength(50, MinimumLength = 4)]
        public decimal Price { get; set; }
        public string? ImageSource { get; set; }

        public int? Quan { get; set; }

        [Required(ErrorMessage = "Category Id is required")]
        public string? CategoryId { get; set; }
    }
}
