using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payrolly.DAL.Migrations
{
    public partial class add_paycheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PayFrequency",
                table: "PaySchedules",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "PayChecks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RegularPay = table.Column<double>(type: "float", nullable: false),
                    OverTime = table.Column<double>(type: "float", nullable: false),
                    HolidayPay = table.Column<double>(type: "float", nullable: false),
                    Bonus = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Commission = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EmpTax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CheckNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PayScheduleId = table.Column<string>(type: "nvarchar(450)", nullable: true)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PayChecks");

            migrationBuilder.AlterColumn<string>(
                name: "PayFrequency",
                table: "PaySchedules",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
