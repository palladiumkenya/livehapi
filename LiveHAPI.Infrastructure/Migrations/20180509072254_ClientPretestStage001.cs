using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LiveHAPI.Infrastructure.Migrations
{
    public partial class ClientPretestStage001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientPretestStages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    Consent = table.Column<int>(nullable: true),
                    DisabilityIndicator = table.Column<int>(nullable: true),
                    EncounterDate = table.Column<string>(nullable: true),
                    EncounterType = table.Column<int>(nullable: false),
                    EverTested = table.Column<int>(nullable: true),
                    MonthsSinceLastTest = table.Column<int>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    SelfTest12Months = table.Column<int>(nullable: true),
                    ServicePoint = table.Column<int>(nullable: true),
                    StatusDate = table.Column<DateTime>(nullable: false),
                    Strategy = table.Column<int>(nullable: true),
                    SyncStatus = table.Column<int>(nullable: false),
                    SyncStatusInfo = table.Column<string>(nullable: true),
                    TbScreening = table.Column<int>(nullable: true),
                    TestedAs = table.Column<int>(nullable: true),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientPretestStages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientPretestDisabilityStages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClientPretestStageId = table.Column<Guid>(nullable: false),
                    Disabilities = table.Column<int>(nullable: false),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientPretestDisabilityStages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientPretestDisabilityStages_ClientPretestStages_ClientPretestStageId",
                        column: x => x.ClientPretestStageId,
                        principalTable: "ClientPretestStages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientPretestDisabilityStages_ClientPretestStageId",
                table: "ClientPretestDisabilityStages",
                column: "ClientPretestStageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientPretestDisabilityStages");

            migrationBuilder.DropTable(
                name: "ClientPretestStages");
        }
    }
}
