using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payrolly.DAL.Migrations
{
    public partial class EINColInFedTaxInsteadCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EIN",
                table: "Companies");

            migrationBuilder.AddColumn<string>(
                name: "EIN",
                table: "FederalTaxes",
                type: "nvarchar(9)",
                maxLength: 9,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EIN",
                table: "FederalTaxes");

            migrationBuilder.AddColumn<string>(
                name: "EIN",
                table: "Companies",
                type: "nvarchar(9)",
                maxLength: 9,
                nullable: true);
        }
    }
}
