using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payrolly.DAL.Migrations
{
    public partial class AddTaxWithholding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PayScheduleId",
                table: "GrossPays",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TaxWithholdings",
                columns: table => new
                {
                    FederalWithholdingId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FilingStatusFederal = table.Column<int>(type: "int", nullable: false),
                    FederalCheck = table.Column<bool>(type: "bit", nullable: false),
                    ClaimedDependent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Deducations = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExtraWithholding = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FilingStatusState = table.Column<int>(type: "int", nullable: false),
                    WithholdingAllowance = table.Column<int>(type: "int", nullable: false),
                    AdditionAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FUTA = table.Column<bool>(type: "bit", nullable: false),
                    SocialSecurity = table.Column<bool>(type: "bit", nullable: false),
                    CASUIAndETT = table.Column<bool>(type: "bit", nullable: false),
                    CASDI = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxWithholdings", x => x.FederalWithholdingId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GrossPays_PayScheduleId",
                table: "GrossPays",
                column: "PayScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_GrossPays_PaySchedules_PayScheduleId",
                table: "GrossPays",
                column: "PayScheduleId",
                principalTable: "PaySchedules",
                principalColumn: "PayScheduleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GrossPays_PaySchedules_PayScheduleId",
                table: "GrossPays");

            migrationBuilder.DropTable(
                name: "TaxWithholdings");

            migrationBuilder.DropIndex(
                name: "IX_GrossPays_PayScheduleId",
                table: "GrossPays");

            migrationBuilder.DropColumn(
                name: "PayScheduleId",
                table: "GrossPays");
        }
    }
}
