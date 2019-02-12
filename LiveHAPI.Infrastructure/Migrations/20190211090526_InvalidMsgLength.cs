using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LiveHAPI.Infrastructure.Migrations
{
    public partial class InvalidMsgLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "InvalidMessages",
                type: "Text",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 4000,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "InvalidMessages",
                maxLength: 4000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "Text",
                oldNullable: true);
        }
    }
}
