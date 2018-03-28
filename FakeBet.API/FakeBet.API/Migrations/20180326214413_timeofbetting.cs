using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace FakeBet.API.Migrations
{
    public partial class timeofbetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBetting",
                table: "Bets",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBetting",
                table: "Bets");
        }
    }
}
