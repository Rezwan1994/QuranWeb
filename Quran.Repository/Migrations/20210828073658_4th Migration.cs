using Microsoft.EntityFrameworkCore.Migrations;

namespace Quran.Repository.Migrations
{
    public partial class _4thMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NoInSurah",
                table: "Ayaths",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoInSurah",
                table: "Ayaths");
        }
    }
}
