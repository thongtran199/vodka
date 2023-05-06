using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VodkaEntities;
namespace VodkaDataAccess;

public partial class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Paymentmethod> Paymentmethods { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Taxinfo> Taxinfos { get; set; }

    public DbSet<Transactdetail> Transactdetails { get; set; }

    public DbSet<Transactheader> Transactheaders { get; set; }

    public DbSet<Useraccount> Useraccounts { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var hasher = new PasswordHasher<IdentityUser>();
        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = "133f85fc-3e0c-4bd0-a820-d379c0bf9dc5",
                Name = "Admin",
                NormalizedName = "ADMIN".ToUpper()
            },
            new IdentityRole
            {
                Id = "13ae282b-4fbc-49e6-8deb-4a5e4e8bb130",
                Name = "Manager",
                NormalizedName = "MANAGER".ToUpper()
            }
        );
        modelBuilder.Entity<IdentityUser>().HasData(
            new IdentityUser
            {
                Id = "f139186b-6419-4cb1-8c80-32755a3f7c01",
                UserName = "thongtran",
                Email = "thongtran@gmail.com",
                NormalizedUserName = "THONGTRAN".ToUpper(),
                NormalizedEmail = "THONGTRAN@GMAIL.COM".ToUpper(),
                PasswordHash = hasher.HashPassword(null, "Admin@123")
            }
        );
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                UserId = "f139186b-6419-4cb1-8c80-32755a3f7c01",
                RoleId = "133f85fc-3e0c-4bd0-a820-d379c0bf9dc5"
            },
             new IdentityUserRole<string>
             {
                 UserId = "f139186b-6419-4cb1-8c80-32755a3f7c01",
                 RoleId = "13ae282b-4fbc-49e6-8deb-4a5e4e8bb130"
             }
        );
    }

}
