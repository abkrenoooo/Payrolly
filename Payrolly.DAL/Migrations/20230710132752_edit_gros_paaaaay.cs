using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payrolly.DAL.Migrations
{
    public partial class edit_gros_paaaaay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GrossPays_Employees_EmployeeId",
                table: "GrossPays");

            migrationBuilder.DropForeignKey(
                name: "FK_GrossPays_PaySchedules_PayScheduleId",
                table: "GrossPays");

            migrationBuilder.DropIndex(
                name: "IX_GrossPays_EmployeeId",
                table: "GrossPays");

            migrationBuilder.DropIndex(
                name: "IX_GrossPays_PayScheduleId",
                table: "GrossPays");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "GrossPays");

            migrationBuilder.DropColumn(
                name: "PayScheduleId",
                table: "GrossPays");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "GrossPays",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PayScheduleId",
                table: "GrossPays",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GrossPays_EmployeeId",
                table: "GrossPays",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_GrossPays_PayScheduleId",
                table: "GrossPays",
                column: "PayScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_GrossPays_Employees_EmployeeId",
                table: "GrossPays",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GrossPays_PaySchedules_PayScheduleId",
                table: "GrossPays",
                column: "PayScheduleId",
                principalTable: "PaySchedules",
                principalColumn: "PayScheduleId");
        }
    }
}
