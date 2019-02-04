using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LiveHAPI.Infrastructure.Migrations
{
    public partial class PhoneReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Phone",
                table: "PersonContacts",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Phone",
                table: "PersonContacts",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}
