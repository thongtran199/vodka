namespace Vodka.Models.Useraccount
{
    public class UseraccountCreateViewModel
    {
        public string? UserName { get; set; }

        public string? UserPassword { get; set; }

        public int AccessLevel { get; set; }

        public string? TotalCash { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }
    }
}
