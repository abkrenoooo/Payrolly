using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payrolly.DAL.Migrations
{
    public partial class MakeReltionBetweenPaySchWithCompanyAndEditLastMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ISDeleted",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "CompanyId",
                table: "PaySchedules",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateOfBirth",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaySchedules_CompanyId",
                table: "PaySchedules",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaySchedules_Companies_CompanyId",
                table: "PaySchedules",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaySchedules_Companies_CompanyId",
                table: "PaySchedules");

            migrationBuilder.DropIndex(
                name: "IX_PaySchedules_CompanyId",
                table: "PaySchedules");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "PaySchedules");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Employees");

            migrationBuilder.AddColumn<bool>(
                name: "ISDeleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
