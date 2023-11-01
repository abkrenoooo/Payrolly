using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payrolly.DAL.Migrations
{
    public partial class SeedRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "98f4df00-a720-4794-bacb-921996ae6d22", "294d9109-735e-4481-9754-52c8b4c87ed8", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "98f4df00-a720-4794-bacb-921996ae6d22");
        }
    }
}
