using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LiveHAPI.Infrastructure.Migrations
{
    public partial class CountyInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubCountyId",
                table: "PersonAddresss",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WardId",
                table: "PersonAddresss",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubCountyId",
                table: "PersonAddresss");

            migrationBuilder.DropColumn(
                name: "WardId",
                table: "PersonAddresss");
        }
    }
}
