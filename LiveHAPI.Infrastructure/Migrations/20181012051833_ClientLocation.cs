using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LiveHAPI.Infrastructure.Migrations
{
    public partial class ClientLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "County",
                table: "ClientStages",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubCounty",
                table: "ClientStages",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Ward",
                table: "ClientStages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "County",
                table: "ClientStages");

            migrationBuilder.DropColumn(
                name: "SubCounty",
                table: "ClientStages");

            migrationBuilder.DropColumn(
                name: "Ward",
                table: "ClientStages");
        }
    }
}
