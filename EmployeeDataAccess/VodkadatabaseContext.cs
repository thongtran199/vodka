using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VodkaEntities;
namespace VodkaDataAccess;

public partial class VodkadatabaseContext : DbContext
{
    public VodkadatabaseContext()
    {
    }

    public VodkadatabaseContext(DbContextOptions<VodkadatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Paymentmethod> Paymentmethods { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Productcombo> Productcombos { get; set; }

    public virtual DbSet<Taxinfo> Taxinfos { get; set; }

    public virtual DbSet<Transactdetail> Transactdetails { get; set; }

    public virtual DbSet<Transactheader> Transactheaders { get; set; }

    public virtual DbSet<Useraccount> Useraccounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL(" Server= db4free.net ;Port=3306;Database= vodkadatabase;User ID=vodkausername;Password=thongtrandp1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("category");

            entity.Property(e => e.CatId).HasMaxLength(10);
            entity.Property(e => e.CatName).HasMaxLength(100);
            entity.Property(e => e.Descript).HasMaxLength(10);
            entity.Property(e => e.IsActive).HasMaxLength(10);
        });

        modelBuilder.Entity<Paymentmethod>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("paymentmethod");

            entity.Property(e => e.Descript).HasMaxLength(10);
            entity.Property(e => e.PaymentId).HasMaxLength(10);
            entity.Property(e => e.PaymentName).HasMaxLength(100);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("product");

            entity.Property(e => e.CatId).HasMaxLength(100);
            entity.Property(e => e.Descript).HasMaxLength(10);
            entity.Property(e => e.ImageSource).HasMaxLength(1000);
            entity.Property(e => e.IsActive).HasMaxLength(10);
            entity.Property(e => e.Price).HasMaxLength(100);
            entity.Property(e => e.ProductName).HasMaxLength(100);
            entity.Property(e => e.ProductNum).HasMaxLength(10);
            entity.Property(e => e.Quan).HasMaxLength(100);
            entity.Property(e => e.Tax1).HasMaxLength(100);
            entity.Property(e => e.Tax2).HasMaxLength(100);
            entity.Property(e => e.Tax3).HasMaxLength(100);
        });

        modelBuilder.Entity<Productcombo>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("productcombo");
        });

        modelBuilder.Entity<Taxinfo>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("taxinfo");

            entity.Property(e => e.TaxDes).HasMaxLength(100);
            entity.Property(e => e.TaxId).HasMaxLength(10);
            entity.Property(e => e.TaxRate).HasMaxLength(100);
        });

        modelBuilder.Entity<Transactdetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("transactdetail");

            entity.Property(e => e.CostEach).HasMaxLength(1000);
            entity.Property(e => e.ProductNum).HasMaxLength(100);
            entity.Property(e => e.Status).HasMaxLength(1);
            entity.Property(e => e.Tax1).HasMaxLength(1);
            entity.Property(e => e.Tax2).HasMaxLength(1);
            entity.Property(e => e.Tax3).HasMaxLength(1);
            entity.Property(e => e.Total).HasMaxLength(1000);
            entity.Property(e => e.TransactDetailId).HasMaxLength(100);
            entity.Property(e => e.TransactId).HasMaxLength(100);
        });

        modelBuilder.Entity<Transactheader>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("transactheader");

            entity.Property(e => e.Net).HasMaxLength(1000);
            entity.Property(e => e.Status).HasMaxLength(1);
            entity.Property(e => e.Tax1).HasMaxLength(1);
            entity.Property(e => e.Tax2).HasMaxLength(1);
            entity.Property(e => e.Tax3).HasMaxLength(1);
            entity.Property(e => e.TimePayment).HasMaxLength(6);
            entity.Property(e => e.Total).HasMaxLength(1000);
            entity.Property(e => e.TransactId).HasMaxLength(100);
            entity.Property(e => e.WhoPay).HasMaxLength(1000);
        });

        modelBuilder.Entity<Useraccount>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("useraccount");

            entity.Property(e => e.Address).HasMaxLength(1000);
            entity.Property(e => e.Email).HasMaxLength(1000);
            entity.Property(e => e.IsActive).HasMaxLength(1);
            entity.Property(e => e.UserId).HasMaxLength(1000);
            entity.Property(e => e.UserName).HasMaxLength(1000);
            entity.Property(e => e.UserPassword).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
