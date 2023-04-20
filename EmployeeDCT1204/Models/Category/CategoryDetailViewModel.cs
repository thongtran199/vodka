using System.ComponentModel.DataAnnotations;

namespace Vodka.Models.Category
{
    public class CategoryDetailViewModel
    {
        public string? CatId { get; set; }

        public string? CatName { get; set; }

        public string? Descript { get; set; }

        public string? IsActive { get; set; }
    }
}
