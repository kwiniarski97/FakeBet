using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FakeBet.Migrations
{
    public partial class updsaltsize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Salt",
                table: "Users",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldMaxLength: 32);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Salt",
                table: "Users",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(byte[]),
                oldMaxLength: 64);
        }
    }
}
