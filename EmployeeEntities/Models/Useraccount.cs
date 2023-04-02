using System;
using System.Collections.Generic;

namespace VodkaEntities;
public class Useraccount
{
    public string UserId { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public int AccessLevel { get; set; }

    public int TotalCash { get; set; }

    public string IsActive { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;
}
