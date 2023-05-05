using System.ComponentModel.DataAnnotations;

namespace Vodka.Models.Category
{
    public class CategoryDetailViewModel
    {
        public string? CategoryId { get; set; }

        public string? Name { get; set; }

        public string? Descript { get; set; }

        public int? IsActive { get; set; }
    }
}
