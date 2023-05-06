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
                    { "133f85fc-3e0c-4bd0-a820-d379c0bf9dc5", "5f50e260-2e04-4320-bc75-95be97eec70a", "Admin", "ADMIN" },
                    { "13ae282b-4fbc-49e6-8deb-4a5e4e8bb130", "dc6b8266-4040-4464-ad86-71b27439607b", "Manager", "MANAGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "f139186b-6419-4cb1-8c80-32755a3f7c01", 0, "5aeea19b-3b87-4c58-b200-361d0b0b8a56", "IdentityUser", "thongtran@gmail.com", false, false, null, "THONGTRAN@GMAIL.COM", "THONGTRAN", "AQAAAAEAACcQAAAAEIrmdLGm2P50X/oWAwx59jllRdxHodqJlZ638WMIQZejIgJLmRJ0WyUiHBKUKoeZrw==", null, false, "469c64fa-8ef2-4c2a-8ae2-6cabcd985476", false, "thongtran" });

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
