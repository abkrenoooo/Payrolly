using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payrolly.DAL.Migrations
{
    public partial class GrossPayTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GrossPayId",
                table: "Employees",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GrossPays",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RegularPay = table.Column<double>(type: "float", nullable: false),
                    HolidayPay = table.Column<double>(type: "float", nullable: false),
                    Bonus = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Commision = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalHours = table.Column<double>(type: "float", nullable: false),
                    Gross = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrossPays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GrossPays_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_GrossPayId",
                table: "Employees",
                column: "GrossPayId");

            migrationBuilder.CreateIndex(
                name: "IX_GrossPays_EmployeeId",
                table: "GrossPays",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_GrossPays_GrossPayId",
                table: "Employees",
                column: "GrossPayId",
                principalTable: "GrossPays",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_GrossPays_GrossPayId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "GrossPays");

            migrationBuilder.DropIndex(
                name: "IX_Employees_GrossPayId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "GrossPayId",
                table: "Employees");
        }
    }
}
