using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LiveHAPI.Infrastructure.Migrations
{
    public partial class hAPIPersonUserProvider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceRef",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceSys",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "Providers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceRef",
                table: "Providers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceSys",
                table: "Providers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "PersonName",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceSys",
                table: "PersonName",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "PersonContacts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceRef",
                table: "PersonContacts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceSys",
                table: "PersonContacts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "PersonAddresss",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceRef",
                table: "PersonAddresss",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceSys",
                table: "PersonAddresss",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Source",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SourceRef",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SourceSys",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Source",
                table: "Providers");

            migrationBuilder.DropColumn(
                name: "SourceRef",
                table: "Providers");

            migrationBuilder.DropColumn(
                name: "SourceSys",
                table: "Providers");

            migrationBuilder.DropColumn(
                name: "Source",
                table: "PersonName");

            migrationBuilder.DropColumn(
                name: "SourceSys",
                table: "PersonName");

            migrationBuilder.DropColumn(
                name: "Source",
                table: "PersonContacts");

            migrationBuilder.DropColumn(
                name: "SourceRef",
                table: "PersonContacts");

            migrationBuilder.DropColumn(
                name: "SourceSys",
                table: "PersonContacts");

            migrationBuilder.DropColumn(
                name: "Source",
                table: "PersonAddresss");

            migrationBuilder.DropColumn(
                name: "SourceRef",
                table: "PersonAddresss");

            migrationBuilder.DropColumn(
                name: "SourceSys",
                table: "PersonAddresss");
        }
    }
}
