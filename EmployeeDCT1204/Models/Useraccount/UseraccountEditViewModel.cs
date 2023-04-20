namespace Vodka.Models.Useraccount
{
    public class UseraccountEditViewModel
    {
        public string? UserId { get; set; }

        public string? UserPassword { get; set; }

        public int AccessLevel { get; set; }

        public string? TotalCash { get; set; }

        public string? Address { get; set; }
    }
}
