using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VodkaEntities;
namespace VodkaDataAccess;

public class ApplicationDbContext : IdentityDbContext<Useraccount>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Paymentmethod> Paymentmethods { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Taxinfo> Taxinfos { get; set; }

    public DbSet<Transactdetail> Transactdetails { get; set; }

    public DbSet<Transactheader> Transactheaders { get; set; }

    //public DbSet<Useraccount> Useraccounts { get; set; }
    protected override void OnModelCreating(ModelBuilder model)
    {
        base.OnModelCreating(model);
    }

}
