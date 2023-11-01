using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payrolly.DAL.Migrations
{
    public partial class AddTaxWithholdingAddTaxWithholdingIdINEmployeeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TaxWithholdingd",
                table: "Employees",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_TaxWithholdingd",
                table: "Employees",
                column: "TaxWithholdingd",
                unique: true,
                filter: "[TaxWithholdingd] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_TaxWithholdings_TaxWithholdingd",
                table: "Employees",
                column: "TaxWithholdingd",
                principalTable: "TaxWithholdings",
                principalColumn: "FederalWithholdingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_TaxWithholdings_TaxWithholdingd",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_TaxWithholdingd",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "TaxWithholdingd",
                table: "Employees");
        }
    }
}
