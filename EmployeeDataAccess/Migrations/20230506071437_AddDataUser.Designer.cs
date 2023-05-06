﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VodkaDataAccess;

#nullable disable

namespace VodkaDataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230506071437_AddDataUser")]
    partial class AddDataUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "133f85fc-3e0c-4bd0-a820-d379c0bf9dc5",
                            ConcurrencyStamp = "6cc97249-bbc9-45e5-b088-71f5bc6e0c4f",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "13ae282b-4fbc-49e6-8deb-4a5e4e8bb130",
                            ConcurrencyStamp = "bf35ee4c-4f7e-4a50-af71-2e18b279139a",
                            Name = "Manager",
                            NormalizedName = "MANAGER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "f139186b-6419-4cb1-8c80-32755a3f7c01",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "e90196eb-3a07-49fe-81d0-32210a7dddf0",
                            Email = "vumvp@.hcmue.edu.vn",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "VUMVP@HCMUE.EDU.VN",
                            NormalizedUserName = "VUMVP",
                            PasswordHash = "AQAAAAEAACcQAAAAEB7r4zW0dlhxRn8M3FZ4806kKYm66tKrl1Zcsab1rj7gh36UnIy4ErYuKYxjya2i7A==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "0356322a-82e0-415c-815f-1938ff8133f6",
                            TwoFactorEnabled = false,
                            UserName = "vumvp"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "f139186b-6419-4cb1-8c80-32755a3f7c01",
                            RoleId = "133f85fc-3e0c-4bd0-a820-d379c0bf9dc5"
                        },
                        new
                        {
                            UserId = "f139186b-6419-4cb1-8c80-32755a3f7c01",
                            RoleId = "13ae282b-4fbc-49e6-8deb-4a5e4e8bb130"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("VodkaEntities.Category", b =>
                {
                    b.Property<string>("CategoryId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Descript")
                        .HasColumnType("longtext");

                    b.Property<int?>("IsActive")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("VodkaEntities.Paymentmethod", b =>
                {
                    b.Property<string>("PaymentMethodId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Descript")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("PaymentMethodId");

                    b.ToTable("Paymentmethods");
                });

            modelBuilder.Entity("VodkaEntities.Product", b =>
                {
                    b.Property<string>("ProductId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CategoryId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Descript")
                        .HasColumnType("longtext");

                    b.Property<string>("ImageSource")
                        .HasColumnType("longtext");

                    b.Property<int?>("IsActive")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("Quan")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("VodkaEntities.Taxinfo", b =>
                {
                    b.Property<string>("TaxId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Descript")
                        .HasColumnType("longtext");

                    b.Property<decimal>("Rate")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("TaxId");

                    b.ToTable("Taxinfos");
                });

            modelBuilder.Entity("VodkaEntities.Transactdetail", b =>
                {
                    b.Property<string>("TransactDetailId")
                        .HasColumnType("varchar(255)");

                    b.Property<decimal?>("CostEach")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductId")
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("Quan")
                        .HasColumnType("int");

                    b.Property<decimal?>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("TransactHeaderId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("TransactDetailId");

                    b.HasIndex("ProductId");

                    b.HasIndex("TransactHeaderId");

                    b.ToTable("Transactdetails");
                });

            modelBuilder.Entity("VodkaEntities.Transactheader", b =>
                {
                    b.Property<string>("TransactHeaderId")
                        .HasColumnType("varchar(255)");

                    b.Property<decimal?>("Net")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("Tax1")
                        .HasColumnType("int");

                    b.Property<int?>("Tax2")
                        .HasColumnType("int");

                    b.Property<int?>("Tax3")
                        .HasColumnType("int");

                    b.Property<DateTime?>("TimePayment")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal?>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("UserId")
                        .HasColumnType("longtext");

                    b.HasKey("TransactHeaderId");

                    b.ToTable("Transactheaders");
                });

            modelBuilder.Entity("VodkaEntities.Useraccount", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessLevel")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<int?>("IsActive")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<decimal?>("TotalCash")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext");

                    b.HasKey("UserId");

                    b.ToTable("Useraccounts");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VodkaEntities.Product", b =>
                {
                    b.HasOne("VodkaEntities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("VodkaEntities.Transactdetail", b =>
                {
                    b.HasOne("VodkaEntities.Product", "Product")
                        .WithMany("TransactDetails")
                        .HasForeignKey("ProductId");

                    b.HasOne("VodkaEntities.Transactheader", "TransactHeader")
                        .WithMany("TransactDetails")
                        .HasForeignKey("TransactHeaderId");

                    b.Navigation("Product");

                    b.Navigation("TransactHeader");
                });

            modelBuilder.Entity("VodkaEntities.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("VodkaEntities.Product", b =>
                {
                    b.Navigation("TransactDetails");
                });

            modelBuilder.Entity("VodkaEntities.Transactheader", b =>
                {
                    b.Navigation("TransactDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
