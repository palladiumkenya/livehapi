using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LiveHAPI.Infrastructure.Migrations
{
    public partial class Education : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Completion",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Education",
                table: "Clients",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completion",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Education",
                table: "Clients");
        }
    }
}
