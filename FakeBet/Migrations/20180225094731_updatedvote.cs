using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FakeBet.Migrations
{
    public partial class updatedvote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Matches_MatchId1",
                table: "Votes");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Users_UserNickName",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_MatchId1",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_UserNickName",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "MatchId1",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "UserNickName",
                table: "Votes");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Votes",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<string>(
                name: "MatchId",
                table: "Votes",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Matches",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Votes_MatchId",
                table: "Votes",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_UserId",
                table: "Votes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Matches_MatchId",
                table: "Votes",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "MatchId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Users_UserId",
                table: "Votes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "NickName",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Matches_MatchId",
                table: "Votes");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Users_UserId",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_MatchId",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_UserId",
                table: "Votes");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Votes",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "MatchId",
                table: "Votes",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MatchId1",
                table: "Votes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserNickName",
                table: "Votes",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Matches",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Votes_MatchId1",
                table: "Votes",
                column: "MatchId1");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_UserNickName",
                table: "Votes",
                column: "UserNickName");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Matches_MatchId1",
                table: "Votes",
                column: "MatchId1",
                principalTable: "Matches",
                principalColumn: "MatchId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Users_UserNickName",
                table: "Votes",
                column: "UserNickName",
                principalTable: "Users",
                principalColumn: "NickName",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
