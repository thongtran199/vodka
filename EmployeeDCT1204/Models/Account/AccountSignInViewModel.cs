using System.ComponentModel.DataAnnotations;

namespace Vodka.Models.Account
{
    public class AccountSignInViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
