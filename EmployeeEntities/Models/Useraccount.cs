using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VodkaEntities;
public class Useraccount
{
    [Key]
    public string? UserId { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public int AccessLevel { get; set; }

    public decimal? TotalCash { get; set; }

    public int? IsActive { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }
}
