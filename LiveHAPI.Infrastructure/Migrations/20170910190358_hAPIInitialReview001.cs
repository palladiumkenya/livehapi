using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LiveHAPI.Infrastructure.Migrations
{
    public partial class hAPIInitialReview001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConceptTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConceptTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Conditions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Counties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentifierTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentifierTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Display = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KeyPops",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyPops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaritalStatuses",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaritalStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterFacilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    AreaCode = table.Column<int>(type: "int", nullable: false),
                    AreaInfo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterFacilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Display = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Rank = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BirthDateEstimated = table.Column<bool>(type: "bit", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PracticeTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticeTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProviderTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProviderTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RelationshipTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelationshipTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Validators",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Rank = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Validators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ValidatorTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValidatorTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Concepts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ConceptTypeId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Concepts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Concepts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubCounties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountyId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCounties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCounties_Counties_CountyId",
                        column: x => x.CountyId,
                        principalTable: "Counties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Display = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rank = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryItems_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Forms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Display = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Rank = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Forms_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonAddresss",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountyId = table.Column<int>(type: "int", nullable: true),
                    Landmark = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Lat = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    Lng = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Preferred = table.Column<bool>(type: "bit", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SourceRef = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SourceSys = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonAddresss", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonAddresss_Counties_CountyId",
                        column: x => x.CountyId,
                        principalTable: "Counties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonAddresss_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonContacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Phone = table.Column<int>(type: "int", nullable: false),
                    Preferred = table.Column<bool>(type: "bit", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SourceRef = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SourceSys = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonContacts_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonNames",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MothersName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Preferred = table.Column<bool>(type: "bit", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SourceRef = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SourceSys = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonNames_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Practices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CountyId = table.Column<int>(type: "int", nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PracticeTypeId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Practices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Practices_Counties_CountyId",
                        column: x => x.CountyId,
                        principalTable: "Counties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Practices_PracticeTypes_PracticeTypeId",
                        column: x => x.PracticeTypeId,
                        principalTable: "PracticeTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Programs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Display = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EncounterTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rank = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Programs_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConceptId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Display = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FormId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ordinal = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Rank = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Concepts_ConceptId",
                        column: x => x.ConceptId,
                        principalTable: "Concepts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Questions_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KeyPop = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MaritalStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OtherKeyPop = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PracticeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clients_Practices_PracticeId",
                        column: x => x.PracticeId,
                        principalTable: "Practices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormImplementations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Display = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FormId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PracticeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormImplementations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormImplementations_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormImplementations_Practices_PracticeId",
                        column: x => x.PracticeId,
                        principalTable: "Practices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PracticeActivations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActivationCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ActivationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Device = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    DeviceCode = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IPAddress = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Lat = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    Lng = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    PracticeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticeActivations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PracticeActivations_Practices_PracticeId",
                        column: x => x.PracticeId,
                        principalTable: "Practices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Initials = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Phone = table.Column<int>(type: "int", nullable: true),
                    PracticeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProviderTypeId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Source = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SourceRef = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SourceSys = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Providers_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Providers_Practices_PracticeId",
                        column: x => x.PracticeId,
                        principalTable: "Practices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Providers_ProviderTypes_ProviderTypeId",
                        column: x => x.ProviderTypeId,
                        principalTable: "ProviderTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Phone = table.Column<int>(type: "int", nullable: true),
                    PracticeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SourceRef = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SourceSys = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Practices_PracticeId",
                        column: x => x.PracticeId,
                        principalTable: "Practices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuestionBranches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActionId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ConditionId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GotoQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Group = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RefQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Response = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ResponseComplex = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ResponseType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionBranches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionBranches_Actions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionBranches_Conditions_ConditionId",
                        column: x => x.ConditionId,
                        principalTable: "Conditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionBranches_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionRemoteTransformations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActionId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AltContent = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ClientAttributeId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ConditionId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Group = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RemoteQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Response = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ResponseComplex = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ResponseType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SelfQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionRemoteTransformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionRemoteTransformations_Actions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionRemoteTransformations_Conditions_ConditionId",
                        column: x => x.ConditionId,
                        principalTable: "Conditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionRemoteTransformations_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionReValidations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActionId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ConditionId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Group = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionValidationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RefQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Response = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ResponseComplex = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ResponseType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionReValidations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionReValidations_Actions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionReValidations_Conditions_ConditionId",
                        column: x => x.ConditionId,
                        principalTable: "Conditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionReValidations_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionTransformation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActionId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ConditionId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Group = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rank = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    RefQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Response = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ResponseComplex = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ResponseType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTransformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionTransformation_Actions_ActionId",
                        column: x => x.ActionId,
                        principalTable: "Actions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionTransformation_Conditions_ConditionId",
                        column: x => x.ConditionId,
                        principalTable: "Conditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionTransformation_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionValidations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaxLimit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MinLimit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Revision = table.Column<int>(type: "int", nullable: false),
                    ValidatorId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ValidatorTypeId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionValidations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionValidations_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionValidations_Validators_ValidatorId",
                        column: x => x.ValidatorId,
                        principalTable: "Validators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionValidations_ValidatorTypes_ValidatorTypeId",
                        column: x => x.ValidatorTypeId,
                        principalTable: "ValidatorTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClientAttributes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientAttributes_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientIdentifiers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Identifier = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IdentifierTypeId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Preferred = table.Column<bool>(type: "bit", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientIdentifiers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientIdentifiers_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientIdentifiers_IdentifierTypes_IdentifierTypeId",
                        column: x => x.IdentifierTypeId,
                        principalTable: "IdentifierTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClientRelationships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Preferred = table.Column<bool>(type: "bit", nullable: false),
                    RelatedClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RelationshipTypeId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientRelationships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientRelationships_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientRelationships_RelationshipTypes_RelationshipTypeId",
                        column: x => x.RelationshipTypeId,
                        principalTable: "RelationshipTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Encounters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EncounterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EncounterTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FormId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false),
                    PracticeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProviderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Started = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Stopped = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encounters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Encounters_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Encounters_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Obses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EncounterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsNull = table.Column<bool>(type: "bit", nullable: false),
                    ObsDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValueCoded = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ValueDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValueMultiCoded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValueNumeric = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    ValueText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Obses_Encounters_EncounterId",
                        column: x => x.EncounterId,
                        principalTable: "Encounters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObsFinalTestResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CoupleDiscordant = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EncounterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FinalResult = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FinalResultCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstTestResult = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FirstTestResultCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResultGiven = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SecondTestResult = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SecondTestResultCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SelfTestOption = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObsFinalTestResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObsFinalTestResults_Encounters_EncounterId",
                        column: x => x.EncounterId,
                        principalTable: "Encounters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObsLinkages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateEnrolled = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DatePromised = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EncounterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnrollmentId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FacilityHandedTo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    HandedTo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ReferredTo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false),
                    WorkerCarde = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObsLinkages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObsLinkages_Encounters_EncounterId",
                        column: x => x.EncounterId,
                        principalTable: "Encounters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObsTestResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Attempt = table.Column<int>(type: "int", nullable: false),
                    EncounterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Expiry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsValid = table.Column<bool>(type: "bit", nullable: false),
                    Kit = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KitDisplay = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    KitOther = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LotNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Result = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResultCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ResultDisplay = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TestName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObsTestResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObsTestResults_Encounters_EncounterId",
                        column: x => x.EncounterId,
                        principalTable: "Encounters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObsTraceResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EncounterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Outcome = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Voided = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObsTraceResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObsTraceResults_Encounters_EncounterId",
                        column: x => x.EncounterId,
                        principalTable: "Encounters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryItems_CategoryId",
                table: "CategoryItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryItems_ItemId",
                table: "CategoryItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientAttributes_ClientId",
                table: "ClientAttributes",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientIdentifiers_ClientId",
                table: "ClientIdentifiers",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientIdentifiers_IdentifierTypeId",
                table: "ClientIdentifiers",
                column: "IdentifierTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientRelationships_ClientId",
                table: "ClientRelationships",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientRelationships_RelationshipTypeId",
                table: "ClientRelationships",
                column: "RelationshipTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_PersonId",
                table: "Clients",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_PracticeId",
                table: "Clients",
                column: "PracticeId");

            migrationBuilder.CreateIndex(
                name: "IX_Concepts_CategoryId",
                table: "Concepts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Encounters_ClientId",
                table: "Encounters",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Encounters_FormId",
                table: "Encounters",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_FormImplementations_FormId",
                table: "FormImplementations",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_FormImplementations_PracticeId",
                table: "FormImplementations",
                column: "PracticeId");

            migrationBuilder.CreateIndex(
                name: "IX_Forms_ModuleId",
                table: "Forms",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Obses_EncounterId",
                table: "Obses",
                column: "EncounterId");

            migrationBuilder.CreateIndex(
                name: "IX_ObsFinalTestResults_EncounterId",
                table: "ObsFinalTestResults",
                column: "EncounterId");

            migrationBuilder.CreateIndex(
                name: "IX_ObsLinkages_EncounterId",
                table: "ObsLinkages",
                column: "EncounterId");

            migrationBuilder.CreateIndex(
                name: "IX_ObsTestResults_EncounterId",
                table: "ObsTestResults",
                column: "EncounterId");

            migrationBuilder.CreateIndex(
                name: "IX_ObsTraceResults_EncounterId",
                table: "ObsTraceResults",
                column: "EncounterId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonAddresss_CountyId",
                table: "PersonAddresss",
                column: "CountyId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonAddresss_PersonId",
                table: "PersonAddresss",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonContacts_PersonId",
                table: "PersonContacts",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonNames_PersonId",
                table: "PersonNames",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PracticeActivations_PracticeId",
                table: "PracticeActivations",
                column: "PracticeId");

            migrationBuilder.CreateIndex(
                name: "IX_Practices_CountyId",
                table: "Practices",
                column: "CountyId");

            migrationBuilder.CreateIndex(
                name: "IX_Practices_PracticeTypeId",
                table: "Practices",
                column: "PracticeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_FormId",
                table: "Programs",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_Providers_PersonId",
                table: "Providers",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Providers_PracticeId",
                table: "Providers",
                column: "PracticeId");

            migrationBuilder.CreateIndex(
                name: "IX_Providers_ProviderTypeId",
                table: "Providers",
                column: "ProviderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionBranches_ActionId",
                table: "QuestionBranches",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionBranches_ConditionId",
                table: "QuestionBranches",
                column: "ConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionBranches_QuestionId",
                table: "QuestionBranches",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionRemoteTransformations_ActionId",
                table: "QuestionRemoteTransformations",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionRemoteTransformations_ConditionId",
                table: "QuestionRemoteTransformations",
                column: "ConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionRemoteTransformations_QuestionId",
                table: "QuestionRemoteTransformations",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionReValidations_ActionId",
                table: "QuestionReValidations",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionReValidations_ConditionId",
                table: "QuestionReValidations",
                column: "ConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionReValidations_QuestionId",
                table: "QuestionReValidations",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ConceptId",
                table: "Questions",
                column: "ConceptId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_FormId",
                table: "Questions",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTransformation_ActionId",
                table: "QuestionTransformation",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTransformation_ConditionId",
                table: "QuestionTransformation",
                column: "ConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTransformation_QuestionId",
                table: "QuestionTransformation",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionValidations_QuestionId",
                table: "QuestionValidations",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionValidations_ValidatorId",
                table: "QuestionValidations",
                column: "ValidatorId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionValidations_ValidatorTypeId",
                table: "QuestionValidations",
                column: "ValidatorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCounties_CountyId",
                table: "SubCounties",
                column: "CountyId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PersonId",
                table: "Users",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PracticeId",
                table: "Users",
                column: "PracticeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryItems");

            migrationBuilder.DropTable(
                name: "ClientAttributes");

            migrationBuilder.DropTable(
                name: "ClientIdentifiers");

            migrationBuilder.DropTable(
                name: "ClientRelationships");

            migrationBuilder.DropTable(
                name: "ConceptTypes");

            migrationBuilder.DropTable(
                name: "FormImplementations");

            migrationBuilder.DropTable(
                name: "KeyPops");

            migrationBuilder.DropTable(
                name: "MaritalStatuses");

            migrationBuilder.DropTable(
                name: "MasterFacilities");

            migrationBuilder.DropTable(
                name: "Obses");

            migrationBuilder.DropTable(
                name: "ObsFinalTestResults");

            migrationBuilder.DropTable(
                name: "ObsLinkages");

            migrationBuilder.DropTable(
                name: "ObsTestResults");

            migrationBuilder.DropTable(
                name: "ObsTraceResults");

            migrationBuilder.DropTable(
                name: "PersonAddresss");

            migrationBuilder.DropTable(
                name: "PersonContacts");

            migrationBuilder.DropTable(
                name: "PersonNames");

            migrationBuilder.DropTable(
                name: "PracticeActivations");

            migrationBuilder.DropTable(
                name: "Programs");

            migrationBuilder.DropTable(
                name: "Providers");

            migrationBuilder.DropTable(
                name: "QuestionBranches");

            migrationBuilder.DropTable(
                name: "QuestionRemoteTransformations");

            migrationBuilder.DropTable(
                name: "QuestionReValidations");

            migrationBuilder.DropTable(
                name: "QuestionTransformation");

            migrationBuilder.DropTable(
                name: "QuestionValidations");

            migrationBuilder.DropTable(
                name: "SubCounties");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "IdentifierTypes");

            migrationBuilder.DropTable(
                name: "RelationshipTypes");

            migrationBuilder.DropTable(
                name: "Encounters");

            migrationBuilder.DropTable(
                name: "ProviderTypes");

            migrationBuilder.DropTable(
                name: "Actions");

            migrationBuilder.DropTable(
                name: "Conditions");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Validators");

            migrationBuilder.DropTable(
                name: "ValidatorTypes");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Concepts");

            migrationBuilder.DropTable(
                name: "Forms");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Practices");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "Counties");

            migrationBuilder.DropTable(
                name: "PracticeTypes");
        }
    }
}
