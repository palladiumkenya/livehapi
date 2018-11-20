using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LiveHAPI.Infrastructure.Migrations
{
    public partial class ContactRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RelationName",
                table: "ClientStageRelationships",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClientNetworks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Generated = table.Column<DateTime>(nullable: false),
                    Voided = table.Column<bool>(nullable: false),
                    Primary_DateOfBirth = table.Column<DateTime>(nullable: false),
                    Primary_FirstName = table.Column<string>(nullable: true),
                    Primary_Id = table.Column<Guid>(nullable: false),
                    Primary_LastName = table.Column<string>(nullable: true),
                    Primary_MiddleName = table.Column<string>(nullable: true),
                    Primary_Relation = table.Column<string>(nullable: true),
                    Primary_Serial = table.Column<string>(nullable: true),
                    Primary_Sex = table.Column<int>(nullable: false),
                    Secondary_DateOfBirth = table.Column<DateTime>(nullable: false),
                    Secondary_FirstName = table.Column<string>(nullable: true),
                    Secondary_Id = table.Column<Guid>(nullable: false),
                    Secondary_LastName = table.Column<string>(nullable: true),
                    Secondary_MiddleName = table.Column<string>(nullable: true),
                    Secondary_Relation = table.Column<string>(nullable: true),
                    Secondary_Serial = table.Column<string>(nullable: true),
                    Secondary_Sex = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientNetworks", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientNetworks");

            migrationBuilder.DropColumn(
                name: "RelationName",
                table: "ClientStageRelationships");
        }
    }
}
