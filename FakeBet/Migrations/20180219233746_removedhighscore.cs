using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FakeBet.Migrations
{
    public partial class removedhighscore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_HighScore_HighScoreTickOfUpdate",
                table: "Users");

            migrationBuilder.DropTable(
                name: "HighScore");

            migrationBuilder.DropIndex(
                name: "IX_Users_HighScoreTickOfUpdate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "HighScoreTickOfUpdate",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "HighScoreTickOfUpdate",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HighScore",
                columns: table => new
                {
                    TickOfUpdate = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LastUpdate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HighScore", x => x.TickOfUpdate);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_HighScoreTickOfUpdate",
                table: "Users",
                column: "HighScoreTickOfUpdate");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_HighScore_HighScoreTickOfUpdate",
                table: "Users",
                column: "HighScoreTickOfUpdate",
                principalTable: "HighScore",
                principalColumn: "TickOfUpdate",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
