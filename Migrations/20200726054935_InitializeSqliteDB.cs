using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Discord_Bot.Migrations
{
    public partial class InitializeSqliteDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mutes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    userID = table.Column<ulong>(type: "INTEGER", nullable: false),
                    userName = table.Column<string>(type: "TEXT", nullable: true),
                    muteTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    unmuteTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    muteReason = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mutes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mutes");
        }
    }
}
