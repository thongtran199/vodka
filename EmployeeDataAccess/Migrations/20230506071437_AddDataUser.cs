using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VodkaDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddDataUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "133f85fc-3e0c-4bd0-a820-d379c0bf9dc5", "6cc97249-bbc9-45e5-b088-71f5bc6e0c4f", "Admin", "ADMIN" },
                    { "13ae282b-4fbc-49e6-8deb-4a5e4e8bb130", "bf35ee4c-4f7e-4a50-af71-2e18b279139a", "Manager", "MANAGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "f139186b-6419-4cb1-8c80-32755a3f7c01", 0, "e90196eb-3a07-49fe-81d0-32210a7dddf0", "thongtran@gmail.com", false, false, null, "THONGTRAN@GMAIL.COM", "THONGTRAN", "AQAAAAEAACcQAAAAEB7r4zW0dlhxRn8M3FZ4806kKYm66tKrl1Zcsab1rj7gh36UnIy4ErYuKYxjya2i7A==", null, false, "0356322a-82e0-415c-815f-1938ff8133f6", false, "thongtran" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "133f85fc-3e0c-4bd0-a820-d379c0bf9dc5", "f139186b-6419-4cb1-8c80-32755a3f7c01" },
                    { "13ae282b-4fbc-49e6-8deb-4a5e4e8bb130", "f139186b-6419-4cb1-8c80-32755a3f7c01" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "133f85fc-3e0c-4bd0-a820-d379c0bf9dc5", "f139186b-6419-4cb1-8c80-32755a3f7c01" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "13ae282b-4fbc-49e6-8deb-4a5e4e8bb130", "f139186b-6419-4cb1-8c80-32755a3f7c01" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "133f85fc-3e0c-4bd0-a820-d379c0bf9dc5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "13ae282b-4fbc-49e6-8deb-4a5e4e8bb130");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f139186b-6419-4cb1-8c80-32755a3f7c01");
        }
    }
}
