using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payrolly.DAL.Migrations
{
    public partial class edit_gros_paaaaay01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NetPay",
                table: "GrossPays");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "NetPay",
                table: "GrossPays",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
