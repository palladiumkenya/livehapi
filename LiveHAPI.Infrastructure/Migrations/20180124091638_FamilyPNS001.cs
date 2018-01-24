using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LiveHAPI.Infrastructure.Migrations
{
    public partial class FamilyPNS001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Consent",
                table: "ObsPartnerTraceResults",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BookingDate",
                table: "ObsPartnerScreenings",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<Guid>(
                name: "IPVOutcome",
                table: "ObsPartnerScreenings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LivingWithClient",
                table: "ObsPartnerScreenings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Occupation",
                table: "ObsPartnerScreenings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PNSApproach",
                table: "ObsPartnerScreenings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PNSRealtionship",
                table: "ObsPartnerScreenings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PnsAccepted",
                table: "ObsPartnerScreenings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Consent",
                table: "ObsFamilyTraceResults",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Reminder",
                table: "ObsFamilyTraceResults",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Consent",
                table: "ObsPartnerTraceResults");

            migrationBuilder.DropColumn(
                name: "IPVOutcome",
                table: "ObsPartnerScreenings");

            migrationBuilder.DropColumn(
                name: "LivingWithClient",
                table: "ObsPartnerScreenings");

            migrationBuilder.DropColumn(
                name: "Occupation",
                table: "ObsPartnerScreenings");

            migrationBuilder.DropColumn(
                name: "PNSApproach",
                table: "ObsPartnerScreenings");

            migrationBuilder.DropColumn(
                name: "PNSRealtionship",
                table: "ObsPartnerScreenings");

            migrationBuilder.DropColumn(
                name: "PnsAccepted",
                table: "ObsPartnerScreenings");

            migrationBuilder.DropColumn(
                name: "Consent",
                table: "ObsFamilyTraceResults");

            migrationBuilder.DropColumn(
                name: "Reminder",
                table: "ObsFamilyTraceResults");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BookingDate",
                table: "ObsPartnerScreenings",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
