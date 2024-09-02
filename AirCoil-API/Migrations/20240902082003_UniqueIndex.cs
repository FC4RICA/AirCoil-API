using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AirCoil_API.Migrations
{
    /// <inheritdoc />
    public partial class UniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8e1b1257-ca1d-40bf-aba1-d139b9d34dac");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "df776236-097a-4112-b267-04df6ecc520d");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Provinces",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Brands",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "77f6c3a9-091f-4307-8f68-763b351f04d0", null, "Admin", "Admin" },
                    { "c43c20c7-4b33-41af-86c8-4657b64eebe3", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_Name",
                table: "Provinces",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brands_Name",
                table: "Brands",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Provinces_Name",
                table: "Provinces");

            migrationBuilder.DropIndex(
                name: "IX_Brands_Name",
                table: "Brands");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "77f6c3a9-091f-4307-8f68-763b351f04d0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c43c20c7-4b33-41af-86c8-4657b64eebe3");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Provinces",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8e1b1257-ca1d-40bf-aba1-d139b9d34dac", null, "Admin", "Admin" },
                    { "df776236-097a-4112-b267-04df6ecc520d", null, "User", "USER" }
                });
        }
    }
}
