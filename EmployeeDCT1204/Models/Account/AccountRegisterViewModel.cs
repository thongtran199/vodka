using Microsoft.Build.Framework;

namespace Vodka.Models.Account
{
    public class AccountRegisterViewModel
    {
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? ConfirmPassword { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? FirstName { get; set; }
    }
}
