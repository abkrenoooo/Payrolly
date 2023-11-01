using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payrolly.DAL.Migrations
{
    public partial class AddListOfGrossPayToEmp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_GrossPays_GrossPayId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_GrossPayId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Payment",
                table: "PaySchedules");

            migrationBuilder.DropColumn(
                name: "GrossPayId",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "GrossPays",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "GrossPayDate",
                table: "GrossPays",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Payment",
                table: "GrossPays",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_GrossPays_EmployeeId",
                table: "GrossPays",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_GrossPays_Employees_EmployeeId",
                table: "GrossPays",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GrossPays_Employees_EmployeeId",
                table: "GrossPays");

            migrationBuilder.DropIndex(
                name: "IX_GrossPays_EmployeeId",
                table: "GrossPays");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "GrossPays");

            migrationBuilder.DropColumn(
                name: "GrossPayDate",
                table: "GrossPays");

            migrationBuilder.DropColumn(
                name: "Payment",
                table: "GrossPays");

            migrationBuilder.AddColumn<bool>(
                name: "Payment",
                table: "PaySchedules",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "GrossPayId",
                table: "Employees",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_GrossPayId",
                table: "Employees",
                column: "GrossPayId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_GrossPays_GrossPayId",
                table: "Employees",
                column: "GrossPayId",
                principalTable: "GrossPays",
                principalColumn: "Id");
        }
    }
}
