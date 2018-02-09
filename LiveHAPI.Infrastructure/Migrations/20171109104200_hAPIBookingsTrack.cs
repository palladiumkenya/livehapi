using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LiveHAPI.Infrastructure.Migrations
{
    public partial class hAPIBookingsTrack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "BookingMet",
                table: "ObsPartnerScreenings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateBookingMet",
                table: "ObsPartnerScreenings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TraceId",
                table: "ObsPartnerScreenings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "BookingMet",
                table: "ObsMemberScreenings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateBookingMet",
                table: "ObsMemberScreenings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TraceId",
                table: "ObsMemberScreenings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ObsPartnerTraceResults_EncounterId",
                table: "ObsPartnerTraceResults",
                column: "EncounterId");

            migrationBuilder.CreateIndex(
                name: "IX_ObsPartnerScreenings_EncounterId",
                table: "ObsPartnerScreenings",
                column: "EncounterId");

            migrationBuilder.CreateIndex(
                name: "IX_ObsMemberScreenings_EncounterId",
                table: "ObsMemberScreenings",
                column: "EncounterId");

            migrationBuilder.CreateIndex(
                name: "IX_ObsFamilyTraceResults_EncounterId",
                table: "ObsFamilyTraceResults",
                column: "EncounterId");

            migrationBuilder.AddForeignKey(
                name: "FK_ObsFamilyTraceResults_Encounters_EncounterId",
                table: "ObsFamilyTraceResults",
                column: "EncounterId",
                principalTable: "Encounters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ObsMemberScreenings_Encounters_EncounterId",
                table: "ObsMemberScreenings",
                column: "EncounterId",
                principalTable: "Encounters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ObsPartnerScreenings_Encounters_EncounterId",
                table: "ObsPartnerScreenings",
                column: "EncounterId",
                principalTable: "Encounters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ObsPartnerTraceResults_Encounters_EncounterId",
                table: "ObsPartnerTraceResults",
                column: "EncounterId",
                principalTable: "Encounters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ObsFamilyTraceResults_Encounters_EncounterId",
                table: "ObsFamilyTraceResults");

            migrationBuilder.DropForeignKey(
                name: "FK_ObsMemberScreenings_Encounters_EncounterId",
                table: "ObsMemberScreenings");

            migrationBuilder.DropForeignKey(
                name: "FK_ObsPartnerScreenings_Encounters_EncounterId",
                table: "ObsPartnerScreenings");

            migrationBuilder.DropForeignKey(
                name: "FK_ObsPartnerTraceResults_Encounters_EncounterId",
                table: "ObsPartnerTraceResults");

            migrationBuilder.DropIndex(
                name: "IX_ObsPartnerTraceResults_EncounterId",
                table: "ObsPartnerTraceResults");

            migrationBuilder.DropIndex(
                name: "IX_ObsPartnerScreenings_EncounterId",
                table: "ObsPartnerScreenings");

            migrationBuilder.DropIndex(
                name: "IX_ObsMemberScreenings_EncounterId",
                table: "ObsMemberScreenings");

            migrationBuilder.DropIndex(
                name: "IX_ObsFamilyTraceResults_EncounterId",
                table: "ObsFamilyTraceResults");

            migrationBuilder.DropColumn(
                name: "BookingMet",
                table: "ObsPartnerScreenings");

            migrationBuilder.DropColumn(
                name: "DateBookingMet",
                table: "ObsPartnerScreenings");

            migrationBuilder.DropColumn(
                name: "TraceId",
                table: "ObsPartnerScreenings");

            migrationBuilder.DropColumn(
                name: "BookingMet",
                table: "ObsMemberScreenings");

            migrationBuilder.DropColumn(
                name: "DateBookingMet",
                table: "ObsMemberScreenings");

            migrationBuilder.DropColumn(
                name: "TraceId",
                table: "ObsMemberScreenings");
        }
    }
}
