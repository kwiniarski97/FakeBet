using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace FakeBet.API.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    MatchId = table.Column<string>(nullable: false),
                    Category = table.Column<string>(nullable: true),
                    MatchTime = table.Column<DateTime>(nullable: false),
                    PointsRatio = table.Column<float>(nullable: false),
                    Status = table.Column<int>(nullable: true),
                    TeamAName = table.Column<string>(nullable: true),
                    TeamANationalityCode = table.Column<string>(maxLength: 2, nullable: true),
                    TeamAPoints = table.Column<int>(nullable: false),
                    TeamBName = table.Column<string>(nullable: true),
                    TeamBNationalityCode = table.Column<string>(maxLength: 2, nullable: true),
                    TeamBPoints = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.MatchId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    NickName = table.Column<string>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    FailedLoginsAttemps = table.Column<int>(nullable: false),
                    PasswordHash = table.Column<byte[]>(maxLength: 64, nullable: false),
                    Points = table.Column<int>(nullable: false),
                    Role = table.Column<int>(nullable: false),
                    Salt = table.Column<byte[]>(maxLength: 128, nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.NickName);
                });

            migrationBuilder.CreateTable(
                name: "Bets",
                columns: table => new
                {
                    Id = table.Column<ulong>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BetOnTeamA = table.Column<int>(nullable: false),
                    BetOnTeamB = table.Column<int>(nullable: false),
                    MatchId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bets_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "MatchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "NickName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bets_MatchId",
                table: "Bets",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Bets_UserId",
                table: "Bets",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bets");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
