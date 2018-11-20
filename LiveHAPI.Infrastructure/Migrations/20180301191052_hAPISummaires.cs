using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LiveHAPI.Infrastructure.Migrations
{
    public partial class hAPISummaires : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientSummaries",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Area = table.Column<string>(nullable: true),
                    ClientId = table.Column<Guid>(nullable: false),
                    Rank = table.Column<decimal>(nullable: false),
                    Report = table.Column<string>(nullable: true),
                    ReportDate = table.Column<DateTime>(nullable: true),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientSummaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserSummaries",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Area = table.Column<string>(nullable: true),
                    Rank = table.Column<decimal>(nullable: false),
                    Report = table.Column<int>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSummaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSummaries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSummaries_UserId",
                table: "UserSummaries",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientSummaries");

            migrationBuilder.DropTable(
                name: "UserSummaries");
        }
    }
}
