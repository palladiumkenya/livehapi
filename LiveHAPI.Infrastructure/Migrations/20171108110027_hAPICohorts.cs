using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LiveHAPI.Infrastructure.Migrations
{
    public partial class hAPICohorts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubscriberCohorts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Display = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rank = table.Column<int>(type: "int", nullable: false),
                    SubscriberSystemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    View = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriberCohorts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriberCohorts_SubscriberSystems_SubscriberSystemId",
                        column: x => x.SubscriberSystemId,
                        principalTable: "SubscriberSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriberCohorts_SubscriberSystemId",
                table: "SubscriberCohorts",
                column: "SubscriberSystemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriberCohorts");
        }
    }
}
