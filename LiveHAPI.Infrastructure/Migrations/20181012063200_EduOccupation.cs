using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LiveHAPI.Infrastructure.Migrations
{
    public partial class EduOccupation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Completion",
                table: "ClientStages",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Education",
                table: "ClientStages",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Occupation",
                table: "ClientStages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completion",
                table: "ClientStages");

            migrationBuilder.DropColumn(
                name: "Education",
                table: "ClientStages");

            migrationBuilder.DropColumn(
                name: "Occupation",
                table: "ClientStages");
        }
    }
}
