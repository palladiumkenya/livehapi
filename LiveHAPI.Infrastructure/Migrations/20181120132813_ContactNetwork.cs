using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LiveHAPI.Infrastructure.Migrations
{
    public partial class ContactNetwork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RelationName",
                table: "ClientStageRelationships",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClientContactNetworks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClientContactNetworkId = table.Column<Guid>(nullable: true),
                    ClientId = table.Column<Guid>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    Generated = table.Column<DateTime>(nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    Relation = table.Column<string>(nullable: true),
                    Serial = table.Column<string>(nullable: true),
                    Sex = table.Column<int>(nullable: false),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientContactNetworks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientContactNetworks_ClientContactNetworks_ClientContactNetworkId",
                        column: x => x.ClientContactNetworkId,
                        principalTable: "ClientContactNetworks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientContactNetworks_ClientContactNetworkId",
                table: "ClientContactNetworks",
                column: "ClientContactNetworkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientContactNetworks");

            migrationBuilder.DropColumn(
                name: "RelationName",
                table: "ClientStageRelationships");
        }
    }
}
