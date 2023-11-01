using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payrolly.DAL.Migrations
{
    public partial class edit_gros_paaaaay02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PayFrequency",
                table: "PaySchedules",
                newName: "PayFrequencyTypes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PayFrequencyTypes",
                table: "PaySchedules",
                newName: "PayFrequency");
        }
    }
}
