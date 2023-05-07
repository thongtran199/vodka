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


        model.Entity<IdentityUserRole<string>>().HasKey(ur => new { ur.UserId, ur.RoleId });
    }

}
