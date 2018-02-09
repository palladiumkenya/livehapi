using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LiveHAPI.Infrastructure.Migrations
{
    public partial class PNSFamilTracing001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BookingDate",
                table: "ObsPartnerTraceResults",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "ObsPartnerScreenings",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BookingDate",
                table: "ObsFamilyTraceResults",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingDate",
                table: "ObsPartnerTraceResults");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "ObsPartnerScreenings");

            migrationBuilder.DropColumn(
                name: "BookingDate",
                table: "ObsFamilyTraceResults");
        }
    }
}
