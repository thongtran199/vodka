using System.ComponentModel.DataAnnotations;

namespace Vodka.Models.Category
{
    public class CategoryEditViewModel
    {
        public string? CategoryId { get; set; }

        public string? Name { get; set; }

        public string? Descript { get; set; }

    }
}
