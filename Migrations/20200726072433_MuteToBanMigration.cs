using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Discord_Bot.Migrations
{
    public partial class MuteToBanMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mutes");

            migrationBuilder.CreateTable(
                name: "Bans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    userID = table.Column<ulong>(type: "INTEGER", nullable: false),
                    userName = table.Column<string>(type: "TEXT", nullable: true),
                    banTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    unbanTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    banReason = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bans", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bans");

            migrationBuilder.CreateTable(
                name: "Mutes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    muteReason = table.Column<string>(type: "TEXT", nullable: true),
                    muteTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    unmuteTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    userID = table.Column<ulong>(type: "INTEGER", nullable: false),
                    userName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mutes", x => x.Id);
                });
        }
    }
}
