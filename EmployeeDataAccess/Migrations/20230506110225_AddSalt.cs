using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VodkaDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddSalt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f139186b-6419-4cb1-8c80-32755a3f7c01");

            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "133f85fc-3e0c-4bd0-a820-d379c0bf9dc5",
                column: "ConcurrencyStamp",
                value: "20968556-8b30-4928-87ff-3b711d138071");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "13ae282b-4fbc-49e6-8deb-4a5e4e8bb130",
                column: "ConcurrencyStamp",
                value: "fcbbb4ba-2526-4e95-bebd-5ec047b124e7");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AccessLevel", "Address", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Salt", "SecurityStamp", "TotalCash", "TwoFactorEnabled", "UserName" },
                values: new object[] { "f139186b-6419-4cb1-8c80-32755a3f7c01", 0, null, null, "0bec5210-86e1-4e37-b493-b3eae562a2fe", "VodkaUser", "thongtran@gmail.com", false, false, null, "THONGTRAN@GMAIL.COM", "THONGTRAN", "AQAAAAEAACcQAAAAEFHmMLhwonJxLobod+EhKf/Aq3FKnNCengbbUKUMwliBiKd/xZiDg8sGCR97l5RRVQ==", null, false, null, "fe5baa6b-eba6-440f-9b0c-6ec171216af7", null, false, "thongtran" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f139186b-6419-4cb1-8c80-32755a3f7c01");

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "133f85fc-3e0c-4bd0-a820-d379c0bf9dc5",
                column: "ConcurrencyStamp",
                value: "5f50e260-2e04-4320-bc75-95be97eec70a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "13ae282b-4fbc-49e6-8deb-4a5e4e8bb130",
                column: "ConcurrencyStamp",
                value: "dc6b8266-4040-4464-ad86-71b27439607b");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "f139186b-6419-4cb1-8c80-32755a3f7c01", 0, "5aeea19b-3b87-4c58-b200-361d0b0b8a56", "IdentityUser", "thongtran@gmail.com", false, false, null, "THONGTRAN@GMAIL.COM", "THONGTRAN", "AQAAAAEAACcQAAAAEIrmdLGm2P50X/oWAwx59jllRdxHodqJlZ638WMIQZejIgJLmRJ0WyUiHBKUKoeZrw==", null, false, "469c64fa-8ef2-4c2a-8ae2-6cabcd985476", false, "thongtran" });
        }
    }
}
