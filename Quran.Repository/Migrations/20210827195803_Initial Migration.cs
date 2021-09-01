using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Quran.Repository.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AudioRecitations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AyathId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AudioUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuariId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudioRecitations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ayaths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AyathId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AyathDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EngDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BangDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AyathNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SurahId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ayaths", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Quaris",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuariId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quaris", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Surahs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurahId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SurahName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RevelationPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BanglaName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnglishName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surahs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AudioRecitations");

            migrationBuilder.DropTable(
                name: "Ayaths");

            migrationBuilder.DropTable(
                name: "Quaris");

            migrationBuilder.DropTable(
                name: "Surahs");
        }
    }
}
