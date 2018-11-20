using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LiveHAPI.Infrastructure.Migrations
{
    public partial class psmart001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PSmartStores",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Shr = table.Column<string>(nullable: true),
                    Status = table.Column<string>(maxLength: 100, nullable: true),
                    Status_Date = table.Column<DateTime>(nullable: true),
                    Uuid = table.Column<Guid>(nullable: false),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PSmartStores", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PSmartStores");
        }
    }
}
