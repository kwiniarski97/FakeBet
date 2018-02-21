using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FakeBet.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    MatchId = table.Column<string>(nullable: false),
                    Category = table.Column<string>(nullable: true),
                    MatchTime = table.Column<DateTime>(nullable: false),
                    PointsRatio = table.Column<float>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    TeamAName = table.Column<string>(nullable: true),
                    TeamAPoints = table.Column<int>(nullable: false),
                    TeamBName = table.Column<string>(nullable: true),
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
                    HighScoreTickOfUpdate = table.Column<long>(nullable: true),
                    Password = table.Column<string>(nullable: false),
                    Points = table.Column<int>(nullable: false),
                    Salt = table.Column<byte[]>(maxLength: 32, nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.NickName);
                    table.ForeignKey(
                        name: "FK_Users_HighScore_HighScoreTickOfUpdate",
                        column: x => x.HighScoreTickOfUpdate,
                        principalTable: "HighScore",
                        principalColumn: "TickOfUpdate",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    VoteId = table.Column<Guid>(nullable: false),
                    MatchId = table.Column<Guid>(nullable: false),
                    MatchId1 = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    UserNickName = table.Column<string>(nullable: true),
                    UserPick = table.Column<int>(nullable: false),
                    UserPoints = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.VoteId);
                    table.ForeignKey(
                        name: "FK_Votes_Matches_MatchId1",
                        column: x => x.MatchId1,
                        principalTable: "Matches",
                        principalColumn: "MatchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Votes_Users_UserNickName",
                        column: x => x.UserNickName,
                        principalTable: "Users",
                        principalColumn: "NickName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_HighScoreTickOfUpdate",
                table: "Users",
                column: "HighScoreTickOfUpdate");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_MatchId1",
                table: "Votes",
                column: "MatchId1");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_UserNickName",
                table: "Votes",
                column: "UserNickName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "HighScore");
        }
    }
}
