﻿using Microsoft.AspNetCore.Identity;

namespace Vodka.Models.VodkaUser
{
    public class VodkaUserIndexViewModel : IdentityUser
    {
        public int? AccessLevel { get; set; }

        public decimal? TotalCash { get; set; }

        public string? Address { get; set; }
        public IList<string>? Roles { get; set; }

        public int? isActive { get; set; }
    }
}
