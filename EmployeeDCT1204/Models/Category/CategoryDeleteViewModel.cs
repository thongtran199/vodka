using System.ComponentModel.DataAnnotations;

namespace Vodka.Models.Category
{
    public class CategoryDeleteViewModel
    {
        public string? CatId { get; set; }

        public string? CatName { get; set; }

        public string? Descript { get; set; }
    }
}
