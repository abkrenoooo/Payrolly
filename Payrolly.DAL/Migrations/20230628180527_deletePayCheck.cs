using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payrolly.DAL.Migrations
{
    public partial class deletePayCheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PayChecks");

            migrationBuilder.DropColumn(
                name: "TotalHours",
                table: "GrossPays");

            migrationBuilder.RenameColumn(
                name: "Gross",
                table: "GrossPays",
                newName: "PayRate");

            migrationBuilder.AddColumn<decimal>(
                name: "EmpTax",
                table: "GrossPays",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "NetPay",
                table: "GrossPays",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PayScheduleId",
                table: "GrossPays",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GrossPays_PayScheduleId",
                table: "GrossPays",
                column: "PayScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_GrossPays_PaySchedules_PayScheduleId",
                table: "GrossPays",
                column: "PayScheduleId",
                principalTable: "PaySchedules",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GrossPays_PaySchedules_PayScheduleId",
                table: "GrossPays");

            migrationBuilder.DropIndex(
                name: "IX_GrossPays_PayScheduleId",
                table: "GrossPays");

            migrationBuilder.DropColumn(
                name: "EmpTax",
                table: "GrossPays");

            migrationBuilder.DropColumn(
                name: "NetPay",
                table: "GrossPays");

            migrationBuilder.DropColumn(
                name: "PayScheduleId",
                table: "GrossPays");

            migrationBuilder.RenameColumn(
                name: "PayRate",
                table: "GrossPays",
                newName: "Gross");

            migrationBuilder.AddColumn<double>(
                name: "TotalHours",
                table: "GrossPays",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "PayChecks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PayScheduleId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Bonus = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CheckNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Commission = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EmpTax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HolidayPay = table.Column<double>(type: "float", nullable: false),
                    OverTime = table.Column<double>(type: "float", nullable: false),
                    RegularPay = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayChecks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayChecks_PaySchedules_PayScheduleId",
                        column: x => x.PayScheduleId,
                        principalTable: "PaySchedules",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PayChecks_PayScheduleId",
                table: "PayChecks",
                column: "PayScheduleId");
        }
    }
}
