﻿// <auto-generated />
using System;
using EmployeeDataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EmployeeDataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230401230723_KhoiTao")]
    partial class KhoiTao
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("EmployeeEntities.Employee", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateJoined")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Designation")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("EmployeeNo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NationalInsuranceNo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("PaymentMethod")
                        .HasColumnType("int");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Postcode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("StudentLoan")
                        .HasColumnType("int");

                    b.Property<int>("UnionMember")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("EmployeeEntities.PaymentRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("ContractualEarnings")
                        .HasColumnType("money");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("FullName")
                        .HasColumnType("longtext");

                    b.Property<decimal>("HourWorked")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("HourlyRate")
                        .HasColumnType("money");

                    b.Property<decimal>("NIC")
                        .HasColumnType("money");

                    b.Property<decimal>("Netpayment")
                        .HasColumnType("money");

                    b.Property<string>("NiNo")
                        .HasColumnType("longtext");

                    b.Property<decimal>("OvertimeEarnings")
                        .HasColumnType("money");

                    b.Property<decimal?>("SLC")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Tax")
                        .HasColumnType("money");

                    b.Property<int>("TaxYearId")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalDeduction")
                        .HasColumnType("money");

                    b.Property<decimal>("TotalEarnings")
                        .HasColumnType("money");

                    b.Property<decimal?>("UnionFees")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("TaxYearId");

                    b.ToTable("PaymentRecord");
                });

            modelBuilder.Entity("EmployeeEntities.TaxYear", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("YearOfTax")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("TaxYear");
                });

            modelBuilder.Entity("EmployeeEntities.PaymentRecord", b =>
                {
                    b.HasOne("EmployeeEntities.Employee", "Employee")
                        .WithMany("PaymentRecord")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmployeeEntities.TaxYear", "TaxYear")
                        .WithMany()
                        .HasForeignKey("TaxYearId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("TaxYear");
                });

            modelBuilder.Entity("EmployeeEntities.Employee", b =>
                {
                    b.Navigation("PaymentRecord");
                });
#pragma warning restore 612, 618
        }
    }
}
