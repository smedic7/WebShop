using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.Migrations
{
    public partial class AddedImageNameOnProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "baed6924-e44b-4960-b74a-b1bffce5ffce");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "01da9ccd-04cd-4d9d-b4c7-95bc90394c03", "ce924a47-531b-4d10-805b-44aa4b394496", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01da9ccd-04cd-4d9d-b4c7-95bc90394c03");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Products");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "baed6924-e44b-4960-b74a-b1bffce5ffce", "61f31c9f-2f7d-430f-8c7a-2f9f4e71616f", "Admin", "ADMIN" });
        }
    }
}
