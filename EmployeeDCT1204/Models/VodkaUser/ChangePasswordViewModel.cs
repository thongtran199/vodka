namespace Vodka.Models.VodkaUser
{
    public class ChangePasswordViewModel
    {
        public string? userName { get; set; }
        public string? oldPassword { get; set; }
        public string? newPassword { get; set; }
    }
}
