using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LiveHAPI.Infrastructure.Migrations
{
    public partial class SyncInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientStages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    DateOfBirthPrecision = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    KeyPop = table.Column<int>(nullable: false),
                    Landmark = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    MaritalStatus = table.Column<int>(nullable: false),
                    MiddleName = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Serial = table.Column<string>(nullable: true),
                    Sex = table.Column<int>(nullable: false),
                    StatusDate = table.Column<DateTime>(nullable: false),
                    SyncStatus = table.Column<int>(nullable: false),
                    SyncStatusInfo = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientStages", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientStages");
        }
    }
}
