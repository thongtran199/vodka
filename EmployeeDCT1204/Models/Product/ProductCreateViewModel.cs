using VodkaEntities;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Vodka.Models.Product
{
    public class ProductCreateViewModel
    {
        public string ProductNum { get; set; }
        [Required(ErrorMessage = "Product Name is required"), StringLength(50, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z][a-zA-Z""'\s-]*$"), Display(Name = "First Name")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Descript is required"), StringLength(500, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z][a-zA-Z""'\s-]*$"), Display(Name = "Last Name")]
        public string Descript { get; set; }
        [Required(ErrorMessage = "Price is required"), StringLength(50, MinimumLength = 4)]
        public string Price { get; set; }
        public string ImageSource { get; set; }
        public string? Tax1 { get; set; }

        public string? Tax2 { get; set; }

        public string? Tax3 { get; set; }

        public string? Quan { get; set; }

        public string? IsActive { get; set; }
        [Required(ErrorMessage = "Category Id is required")]
        public string? CatId { get; set; }
    }
}
