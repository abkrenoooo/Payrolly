using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payrolly.DAL.Migrations
{
    public partial class AddTaxWithholdingAddEmpId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "TaxWithholdings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaxWithholdings_EmployeeId",
                table: "TaxWithholdings",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaxWithholdings_Employees_EmployeeId",
                table: "TaxWithholdings",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaxWithholdings_Employees_EmployeeId",
                table: "TaxWithholdings");

            migrationBuilder.DropIndex(
                name: "IX_TaxWithholdings_EmployeeId",
                table: "TaxWithholdings");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "TaxWithholdings");
        }
    }
}
