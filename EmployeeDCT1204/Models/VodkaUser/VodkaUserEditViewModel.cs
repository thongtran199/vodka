using Microsoft.AspNetCore.Identity;

namespace Vodka.Models.VodkaUser
{

    public class VodkaUserEditViewModel
    {
        public string? Id { get; set; }
        public int AccessLevel { get; set; }

        public decimal? TotalCash { get; set; }

        public string? Address { get; set; }
    }
}
