using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FakeBet.API.Migrations
{
    public partial class update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FailedLoginsAttemps",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TeamANationalityCode",
                table: "Matches",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeamBNationalityCode",
                table: "Matches",
                maxLength: 2,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FailedLoginsAttemps",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TeamANationalityCode",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "TeamBNationalityCode",
                table: "Matches");
        }
    }
}
