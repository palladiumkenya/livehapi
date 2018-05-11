using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LiveHAPI.Infrastructure.Migrations
{
    public partial class clienrelation002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientStageRelationships",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IndexClientId = table.Column<Guid>(nullable: false),
                    IsPartner = table.Column<bool>(nullable: false),
                    Relation = table.Column<int>(nullable: false),
                    SecondaryClientId = table.Column<Guid>(nullable: false),
                    StatusDate = table.Column<DateTime>(nullable: false),
                    SyncStatus = table.Column<int>(nullable: false),
                    SyncStatusInfo = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientStageRelationships", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientStageRelationships");
        }
    }
}
