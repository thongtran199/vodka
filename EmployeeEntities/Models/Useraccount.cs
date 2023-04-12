
using Microsoft.AspNetCore.Identity;


namespace VodkaEntities;
public class Useraccount:IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int? AccessLevel { get; set; }

    public string? TotalCash { get; set; }

    public string? Address { get; set; }
}
