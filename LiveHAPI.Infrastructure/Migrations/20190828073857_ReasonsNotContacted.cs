using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LiveHAPI.Infrastructure.Migrations
{
    public partial class ReasonsNotContacted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ReasonNotContacted",
                table: "ObsTraceResults",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReasonNotContactedOther",
                table: "ObsTraceResults",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReasonNotContacted",
                table: "ObsPartnerTraceResults",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReasonNotContactedOther",
                table: "ObsPartnerTraceResults",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasArtStartDate",
                table: "ObsLinkages",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReasonNotContacted",
                table: "ObsFamilyTraceResults",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReasonNotContactedOther",
                table: "ObsFamilyTraceResults",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReasonNotContacted",
                table: "ObsTraceResults");

            migrationBuilder.DropColumn(
                name: "ReasonNotContactedOther",
                table: "ObsTraceResults");

            migrationBuilder.DropColumn(
                name: "ReasonNotContacted",
                table: "ObsPartnerTraceResults");

            migrationBuilder.DropColumn(
                name: "ReasonNotContactedOther",
                table: "ObsPartnerTraceResults");

            migrationBuilder.DropColumn(
                name: "HasArtStartDate",
                table: "ObsLinkages");

            migrationBuilder.DropColumn(
                name: "ReasonNotContacted",
                table: "ObsFamilyTraceResults");

            migrationBuilder.DropColumn(
                name: "ReasonNotContactedOther",
                table: "ObsFamilyTraceResults");
        }
    }
}
