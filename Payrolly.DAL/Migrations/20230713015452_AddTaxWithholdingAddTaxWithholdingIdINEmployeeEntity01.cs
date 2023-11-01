using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payrolly.DAL.Migrations
{
    public partial class AddTaxWithholdingAddTaxWithholdingIdINEmployeeEntity01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_TaxWithholdings_TaxWithholdingd",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_TaxWithholdingd",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "TaxWithholdingd",
                table: "Employees",
                newName: "TaxWithholdingId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_TaxWithholdingId",
                table: "Employees",
                column: "TaxWithholdingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_TaxWithholdings_TaxWithholdingId",
                table: "Employees",
                column: "TaxWithholdingId",
                principalTable: "TaxWithholdings",
                principalColumn: "FederalWithholdingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_TaxWithholdings_TaxWithholdingId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_TaxWithholdingId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "TaxWithholdingId",
                table: "Employees",
                newName: "TaxWithholdingd");

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
    }
}
