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

    protected override void OnModelCreating(ModelBuilder model)
    {
        base.OnModelCreating(model);

        model.Entity<VodkaUser>()
            .Property(u => u.Address)
            .HasColumnType("longtext");

        model.Entity<VodkaUser>()
            .Property(u => u.AccessLevel)
            .HasColumnType("int");

        model.Entity<VodkaUser>()
            .Property(u => u.TotalCash)
            .HasColumnType("decimal(18,2)");
        //model.Entity<VodkaUser>()
        //    .Property(u => u.Salt)
        //    .HasColumnType("longtext");


        model.Entity<IdentityUserRole<string>>().HasKey(ur => new { ur.UserId, ur.RoleId });
        model.Entity<IdentityRole>().HasData(
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

        var hasher = new PasswordHasher<VodkaUser>();
        model.Entity<VodkaUser>().HasData(
            new VodkaUser
            {
                Id = "f139186b-6419-4cb1-8c80-32755a3f7c01",
                UserName = "thongtran",
                Email = "thongtran@gmail.com",
                NormalizedUserName = "thongtran".ToUpper(),
                NormalizedEmail = "thongtran@gmail.com".ToUpper(),
                PasswordHash = hasher.HashPassword(null, "Admin@123")
            }
        );
        model.Entity<IdentityUserRole<string>>().HasData
        (
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
