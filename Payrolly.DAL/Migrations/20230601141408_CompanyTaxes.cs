using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payrolly.DAL.Migrations
{
    public partial class CompanyTaxes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EIN",
                table: "Companies",
                type: "nvarchar(9)",
                maxLength: 9,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FedrialTaxId",
                table: "Companies",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FederalTaxes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TaxRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxForm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FederalTaxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FederalTaxes_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StateTaxes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StateName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployerAccountNumber = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    UnEmploymentInsuranceRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrainingTaxRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CompanyId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateTaxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StateTaxes_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_FedrialTaxId",
                table: "Companies",
                column: "FedrialTaxId",
                unique: true,
                filter: "[FedrialTaxId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FederalTaxes_CompanyId",
                table: "FederalTaxes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_StateTaxes_CompanyId",
                table: "StateTaxes",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_FederalTaxes_FedrialTaxId",
                table: "Companies",
                column: "FedrialTaxId",
                principalTable: "FederalTaxes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_FederalTaxes_FedrialTaxId",
                table: "Companies");

            migrationBuilder.DropTable(
                name: "FederalTaxes");

            migrationBuilder.DropTable(
                name: "StateTaxes");

            migrationBuilder.DropIndex(
                name: "IX_Companies_FedrialTaxId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "EIN",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "FedrialTaxId",
                table: "Companies");
        }
    }
}
