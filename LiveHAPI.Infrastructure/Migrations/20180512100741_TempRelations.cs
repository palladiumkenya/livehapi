using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LiveHAPI.Infrastructure.Migrations
{
    public partial class TempRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TempClientRelationships",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    IsIndex = table.Column<bool>(nullable: true),
                    Preferred = table.Column<bool>(nullable: false),
                    RelatedClientId = table.Column<Guid>(nullable: false),
                    RelationshipTypeId = table.Column<string>(maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempClientRelationships", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TempClientRelationships");
        }
    }
}
