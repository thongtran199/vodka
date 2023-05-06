using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VodkaDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSalt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salt",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "133f85fc-3e0c-4bd0-a820-d379c0bf9dc5",
                column: "ConcurrencyStamp",
                value: "d4e57c9c-9ae6-49b5-acd1-021486ee03d5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "13ae282b-4fbc-49e6-8deb-4a5e4e8bb130",
                column: "ConcurrencyStamp",
                value: "87f6fbc1-1d20-459a-a091-c62aa1e9f077");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f139186b-6419-4cb1-8c80-32755a3f7c01",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6791c60f-8ab9-4ae1-a88d-3443aa3e3eb6", "AQAAAAEAACcQAAAAEBpG2XKTommxblRerdoQ9hbsBCDsIFtNs/ltYf8zsRXQn69Rd26/hta9ONScDI5Xow==", "e3f20b96-f2c0-4837-bfde-b84e4562de29" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f139186b-6419-4cb1-8c80-32755a3f7c01",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Salt", "SecurityStamp" },
                values: new object[] { "0bec5210-86e1-4e37-b493-b3eae562a2fe", "AQAAAAEAACcQAAAAEFHmMLhwonJxLobod+EhKf/Aq3FKnNCengbbUKUMwliBiKd/xZiDg8sGCR97l5RRVQ==", null, "fe5baa6b-eba6-440f-9b0c-6ec171216af7" });
        }
    }
}
