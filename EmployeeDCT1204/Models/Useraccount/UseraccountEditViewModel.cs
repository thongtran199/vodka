namespace Vodka.Models.Useraccount
{
    public class UseraccountEditViewModel
    {
        public string? UserId { get; set; }

        public string? Password { get; set; }

        public int AccessLevel { get; set; }

        public decimal? TotalCash { get; set; }

        public string? Address { get; set; }
    }
}
