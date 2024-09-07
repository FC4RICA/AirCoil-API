using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AirCoil_API.Migrations
{
    /// <inheritdoc />
    public partial class ImageModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "77f6c3a9-091f-4307-8f68-763b351f04d0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c43c20c7-4b33-41af-86c8-4657b64eebe3");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Results",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "URL",
                table: "Images",
                newName: "FilePath");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Images",
                newName: "UploadDate");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Images",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8903769c-9c67-4e61-b482-bef9b5a7e252", null, "Admin", "Admin" },
                    { "fdb7b2cd-9f0e-46e9-9ecf-005dbbfda85f", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8903769c-9c67-4e61-b482-bef9b5a7e252");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fdb7b2cd-9f0e-46e9-9ecf-005dbbfda85f");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Results",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "UploadDate",
                table: "Images",
                newName: "DateTime");

            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "Images",
                newName: "URL");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "77f6c3a9-091f-4307-8f68-763b351f04d0", null, "Admin", "Admin" },
                    { "c43c20c7-4b33-41af-86c8-4657b64eebe3", null, "User", "USER" }
                });
        }
    }
}
