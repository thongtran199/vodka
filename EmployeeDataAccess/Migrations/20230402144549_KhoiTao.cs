using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VodkaDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class KhoiTao : Migration
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
                    CatId = table.Column<string>(type: "varchar(255)", nullable: false),
                    CatName = table.Column<string>(type: "longtext", nullable: true),
                    Descript = table.Column<string>(type: "longtext", nullable: true),
                    IsActive = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CatId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Paymentmethods",
                columns: table => new
                {
                    PaymentId = table.Column<string>(type: "varchar(255)", nullable: false),
                    PaymentName = table.Column<string>(type: "longtext", nullable: true),
                    Descript = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paymentmethods", x => x.PaymentId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Taxinfos",
                columns: table => new
                {
                    TaxId = table.Column<string>(type: "varchar(255)", nullable: false),
                    TaxDes = table.Column<string>(type: "longtext", nullable: true),
                    TaxRate = table.Column<string>(type: "longtext", nullable: true)
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
                    TransactId = table.Column<string>(type: "varchar(255)", nullable: false),
                    Net = table.Column<string>(type: "longtext", nullable: true),
                    Tax1 = table.Column<string>(type: "longtext", nullable: true),
                    Tax2 = table.Column<string>(type: "longtext", nullable: true),
                    Tax3 = table.Column<string>(type: "longtext", nullable: true),
                    Total = table.Column<string>(type: "longtext", nullable: true),
                    TimePayment = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    WhoPay = table.Column<string>(type: "longtext", nullable: true),
                    Status = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactheaders", x => x.TransactId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Useraccounts",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    UserName = table.Column<string>(type: "longtext", nullable: false),
                    UserPassword = table.Column<string>(type: "longtext", nullable: false),
                    AccessLevel = table.Column<int>(type: "int", nullable: false),
                    TotalCash = table.Column<string>(type: "longtext", nullable: true),
                    IsActive = table.Column<string>(type: "longtext", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    Address = table.Column<string>(type: "longtext", nullable: false)
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
                    ProductNum = table.Column<string>(type: "varchar(255)", nullable: false),
                    ProductName = table.Column<string>(type: "longtext", nullable: true),
                    Descript = table.Column<string>(type: "longtext", nullable: true),
                    Price = table.Column<string>(type: "longtext", nullable: true),
                    Tax1 = table.Column<string>(type: "longtext", nullable: true),
                    Tax2 = table.Column<string>(type: "longtext", nullable: true),
                    Tax3 = table.Column<string>(type: "longtext", nullable: true),
                    Quan = table.Column<string>(type: "longtext", nullable: true),
                    IsActive = table.Column<string>(type: "longtext", nullable: true),
                    ImageSource = table.Column<string>(type: "longtext", nullable: true),
                    CatId = table.Column<string>(type: "longtext", nullable: true),
                    CategoryCatId = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductNum);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryCatId",
                        column: x => x.CategoryCatId,
                        principalTable: "Categories",
                        principalColumn: "CatId");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Transactdetails",
                columns: table => new
                {
                    TransactDetailId = table.Column<string>(type: "varchar(255)", nullable: false),
                    CostEach = table.Column<string>(type: "longtext", nullable: true),
                    Tax1 = table.Column<string>(type: "longtext", nullable: true),
                    Tax2 = table.Column<string>(type: "longtext", nullable: true),
                    Tax3 = table.Column<string>(type: "longtext", nullable: true),
                    Total = table.Column<string>(type: "longtext", nullable: true),
                    Quan = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "longtext", nullable: true),
                    TransactId = table.Column<string>(type: "varchar(255)", nullable: true),
                    ProductNum = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactdetails", x => x.TransactDetailId);
                    table.ForeignKey(
                        name: "FK_Transactdetails_Products_ProductNum",
                        column: x => x.ProductNum,
                        principalTable: "Products",
                        principalColumn: "ProductNum");
                    table.ForeignKey(
                        name: "FK_Transactdetails_Transactheaders_TransactId",
                        column: x => x.TransactId,
                        principalTable: "Transactheaders",
                        principalColumn: "TransactId");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryCatId",
                table: "Products",
                column: "CategoryCatId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactdetails_ProductNum",
                table: "Transactdetails",
                column: "ProductNum");

            migrationBuilder.CreateIndex(
                name: "IX_Transactdetails_TransactId",
                table: "Transactdetails",
                column: "TransactId");
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
