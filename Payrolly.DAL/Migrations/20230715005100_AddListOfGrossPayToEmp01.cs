using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payrolly.DAL.Migrations
{
    public partial class AddListOfGrossPayToEmp01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyId",
                table: "GrossPays",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GrossPays_CompanyId",
                table: "GrossPays",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_GrossPays_Companies_CompanyId",
                table: "GrossPays",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GrossPays_Companies_CompanyId",
                table: "GrossPays");

            migrationBuilder.DropIndex(
                name: "IX_GrossPays_CompanyId",
                table: "GrossPays");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "GrossPays");
        }
    }
}
