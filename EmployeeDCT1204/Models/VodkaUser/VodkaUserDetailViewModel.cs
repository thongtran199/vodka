using Microsoft.AspNetCore.Identity;

namespace Vodka.Models.VodkaUser
{

    public class VodkaUserDetailViewModel : IdentityUser
    {
        public int? AccessLevel { get; set; }

        public decimal? TotalCash { get; set; }

        public string? Address { get; set; }
    }
}
