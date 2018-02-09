using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LiveHAPI.Infrastructure.Migrations
{
    public partial class hAPIOContacts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFamilyMember",
                table: "Clients",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPartner",
                table: "Clients",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ObsFamilyTraceResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EncounterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModeDisplay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Outcome = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OutcomeDisplay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObsFamilyTraceResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ObsMemberScreenings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Eligibility = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EncounterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HivStatus = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScreeningDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObsMemberScreenings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ObsPartnerScreenings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Eligibility = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EncounterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HivStatus = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IPVScreening = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhysicalAssult = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScreeningDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SexuallyUncomfortable = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Threatened = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObsPartnerScreenings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ObsPartnerTraceResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EncounterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModeDisplay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Outcome = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OutcomeDisplay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObsPartnerTraceResults", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ObsFamilyTraceResults");

            migrationBuilder.DropTable(
                name: "ObsMemberScreenings");

            migrationBuilder.DropTable(
                name: "ObsPartnerScreenings");

            migrationBuilder.DropTable(
                name: "ObsPartnerTraceResults");

            migrationBuilder.DropColumn(
                name: "IsFamilyMember",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "IsPartner",
                table: "Clients");
        }
    }
}
