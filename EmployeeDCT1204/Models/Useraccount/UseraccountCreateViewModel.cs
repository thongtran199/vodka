﻿namespace Vodka.Models.Useraccount
{
    public class UseraccountCreateViewModel
    {
        public string? UserName { get; set; }

        public string? Password { get; set; }

        public int AccessLevel { get; set; }

        public decimal? TotalCash { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }
    }
}
