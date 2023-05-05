using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VodkaDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    Descript = table.Column<string>(type: "longtext", nullable: true),
                    IsActive = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Paymentmethods",
                columns: table => new
                {
                    PaymentMethodId = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    Descript = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paymentmethods", x => x.PaymentMethodId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Taxinfos",
                columns: table => new
                {
                    TaxId = table.Column<string>(type: "varchar(255)", nullable: false),
                    Descript = table.Column<string>(type: "longtext", nullable: true),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxinfos", x => x.TaxId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Transactheaders",
                columns: table => new
                {
                    TransactHeaderId = table.Column<string>(type: "varchar(255)", nullable: false),
                    Net = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Tax1 = table.Column<int>(type: "int", nullable: true),
                    Tax2 = table.Column<int>(type: "int", nullable: true),
                    Tax3 = table.Column<int>(type: "int", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TimePayment = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UserId = table.Column<string>(type: "longtext", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactheaders", x => x.TransactHeaderId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Useraccounts",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    UserName = table.Column<string>(type: "longtext", nullable: true),
                    Password = table.Column<string>(type: "longtext", nullable: true),
                    AccessLevel = table.Column<int>(type: "int", nullable: false),
                    TotalCash = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsActive = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "longtext", nullable: true),
                    Address = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Useraccounts", x => x.UserId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    Descript = table.Column<string>(type: "longtext", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quan = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<int>(type: "int", nullable: true),
                    ImageSource = table.Column<string>(type: "longtext", nullable: true),
                    CategoryId = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Transactdetails",
                columns: table => new
                {
                    TransactDetailId = table.Column<string>(type: "varchar(255)", nullable: false),
                    CostEach = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quan = table.Column<int>(type: "int", nullable: true),
                    TransactHeaderId = table.Column<string>(type: "varchar(255)", nullable: true),
                    ProductId = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactdetails", x => x.TransactDetailId);
                    table.ForeignKey(
                        name: "FK_Transactdetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId");
                    table.ForeignKey(
                        name: "FK_Transactdetails_Transactheaders_TransactHeaderId",
                        column: x => x.TransactHeaderId,
                        principalTable: "Transactheaders",
                        principalColumn: "TransactHeaderId");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactdetails_ProductId",
                table: "Transactdetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactdetails_TransactHeaderId",
                table: "Transactdetails",
                column: "TransactHeaderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Paymentmethods");

            migrationBuilder.DropTable(
                name: "Taxinfos");

            migrationBuilder.DropTable(
                name: "Transactdetails");

            migrationBuilder.DropTable(
                name: "Useraccounts");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Transactheaders");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
