using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VodkaEntities;
public class VodkaUser : IdentityUser
{
    public int? AccessLevel { get; set; }

    public decimal? TotalCash { get; set; }

    public string? Address { get; set; }

    public int? isActive { get; set; }
}
