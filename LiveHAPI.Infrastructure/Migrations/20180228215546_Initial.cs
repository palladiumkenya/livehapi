using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LiveHAPI.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actions",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConceptTypes",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConceptTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Conditions",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Counties",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EncounterTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncounterTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentifierTypes",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentifierTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    Display = table.Column<string>(maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KeyPops",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyPops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaritalStatuses",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaritalStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterFacilities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    AreaCode = table.Column<int>(nullable: false),
                    AreaInfo = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterFacilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(maxLength: 150, nullable: true),
                    Display = table.Column<string>(maxLength: 50, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Rank = table.Column<decimal>(nullable: false),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: true),
                    BirthDateEstimated = table.Column<bool>(nullable: true),
                    Gender = table.Column<string>(maxLength: 10, nullable: true),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PracticeTypes",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticeTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProviderTypes",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProviderTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RelationshipTypes",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelationshipTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriberSystems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDefault = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriberSystems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Validators",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Rank = table.Column<decimal>(nullable: false),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Validators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ValidatorTypes",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValidatorTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Concepts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: true),
                    ConceptTypeId = table.Column<string>(maxLength: 50, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(nullable: false)
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
                    Id = table.Column<Guid>(nullable: false),
                    CountyId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Voided = table.Column<bool>(nullable: false)
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
                    Id = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: false),
                    Display = table.Column<string>(maxLength: 100, nullable: true),
                    ItemId = table.Column<Guid>(nullable: false),
                    Rank = table.Column<decimal>(nullable: false),
                    Voided = table.Column<bool>(nullable: false)
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
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(maxLength: 150, nullable: true),
                    Display = table.Column<string>(maxLength: 50, nullable: true),
                    ModuleId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Rank = table.Column<decimal>(nullable: false),
                    Version = table.Column<string>(maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(nullable: false)
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
                    Id = table.Column<Guid>(nullable: false),
                    CountyId = table.Column<int>(nullable: true),
                    Landmark = table.Column<string>(maxLength: 200, nullable: true),
                    Lat = table.Column<decimal>(nullable: true),
                    Lng = table.Column<decimal>(nullable: true),
                    PersonId = table.Column<Guid>(nullable: false),
                    Preferred = table.Column<bool>(nullable: false),
                    Source = table.Column<string>(maxLength: 50, nullable: true),
                    SourceRef = table.Column<string>(maxLength: 50, nullable: true),
                    SourceSys = table.Column<string>(maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(nullable: false)
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
                    Id = table.Column<Guid>(nullable: false),
                    PersonId = table.Column<Guid>(nullable: false),
                    Phone = table.Column<int>(nullable: true),
                    Preferred = table.Column<bool>(nullable: false),
                    Source = table.Column<string>(maxLength: 50, nullable: true),
                    SourceRef = table.Column<string>(maxLength: 50, nullable: true),
                    SourceSys = table.Column<string>(maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(nullable: false)
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
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: true),
                    LastName = table.Column<string>(maxLength: 100, nullable: true),
                    MiddleName = table.Column<string>(maxLength: 100, nullable: true),
                    MothersName = table.Column<string>(maxLength: 100, nullable: true),
                    PersonId = table.Column<Guid>(nullable: false),
                    Preferred = table.Column<bool>(nullable: false),
                    Source = table.Column<string>(maxLength: 50, nullable: true),
                    SourceRef = table.Column<string>(maxLength: 50, nullable: true),
                    SourceSys = table.Column<string>(maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(nullable: false)
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
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    CountyId = table.Column<int>(nullable: true),
                    IsDefault = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    PracticeTypeId = table.Column<string>(maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(nullable: false)
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
                name: "SubscriberCohorts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Display = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Rank = table.Column<int>(nullable: false),
                    SubscriberSystemId = table.Column<Guid>(nullable: false),
                    View = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriberCohorts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriberCohorts_SubscriberSystems_SubscriberSystemId",
                        column: x => x.SubscriberSystemId,
                        principalTable: "SubscriberSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubscriberConfigs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Area = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    SubscriberSystemId = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriberConfigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriberConfigs_SubscriberSystems_SubscriberSystemId",
                        column: x => x.SubscriberSystemId,
                        principalTable: "SubscriberSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubscriberMaps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Field = table.Column<string>(nullable: true),
                    FormId = table.Column<string>(nullable: true),
                    Group = table.Column<int>(nullable: false),
                    Mode = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    SectionId = table.Column<string>(nullable: true),
                    SubField = table.Column<string>(nullable: true),
                    SubName = table.Column<string>(nullable: true),
                    SubType = table.Column<string>(nullable: true),
                    SubscriberSystemId = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriberMaps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriberMaps_SubscriberSystems_SubscriberSystemId",
                        column: x => x.SubscriberSystemId,
                        principalTable: "SubscriberSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubscriberMessagess",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    DateProcessed = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Processed = table.Column<bool>(nullable: false),
                    Rank = table.Column<decimal>(nullable: false),
                    Subject = table.Column<string>(nullable: true),
                    SubscriberSystemId = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriberMessagess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriberMessagess_SubscriberSystems_SubscriberSystemId",
                        column: x => x.SubscriberSystemId,
                        principalTable: "SubscriberSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubscriberSqlActions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Action = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Rank = table.Column<decimal>(nullable: false),
                    SubscriberSystemId = table.Column<Guid>(nullable: true),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriberSqlActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriberSqlActions_SubscriberSystems_SubscriberSystemId",
                        column: x => x.SubscriberSystemId,
                        principalTable: "SubscriberSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubscriberTranslations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    Display = table.Column<string>(nullable: true),
                    Group = table.Column<int>(nullable: false),
                    IsText = table.Column<bool>(nullable: false),
                    Ref = table.Column<string>(nullable: true),
                    SubCode = table.Column<string>(nullable: true),
                    SubDisplay = table.Column<string>(nullable: true),
                    SubRef = table.Column<string>(nullable: true),
                    SubscriberSystemId = table.Column<Guid>(nullable: false),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriberTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriberTranslations_SubscriberSystems_SubscriberSystemId",
                        column: x => x.SubscriberSystemId,
                        principalTable: "SubscriberSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Programs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(maxLength: 150, nullable: true),
                    Display = table.Column<string>(maxLength: 50, nullable: true),
                    EncounterTypeId = table.Column<Guid>(nullable: false),
                    FormId = table.Column<Guid>(nullable: false),
                    Rank = table.Column<decimal>(nullable: false),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Programs_EncounterTypes_EncounterTypeId",
                        column: x => x.EncounterTypeId,
                        principalTable: "EncounterTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Id = table.Column<Guid>(nullable: false),
                    ConceptId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(maxLength: 50, nullable: true),
                    Display = table.Column<string>(maxLength: 50, nullable: true),
                    Fact = table.Column<string>(nullable: true),
                    FormId = table.Column<Guid>(nullable: false),
                    Ordinal = table.Column<string>(maxLength: 50, nullable: true),
                    Rank = table.Column<decimal>(nullable: false),
                    Voided = table.Column<bool>(nullable: false)
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
                    Id = table.Column<Guid>(nullable: false),
                    AlreadyTestedPos = table.Column<bool>(nullable: true),
                    IsFamilyMember = table.Column<bool>(nullable: true),
                    IsPartner = table.Column<bool>(nullable: true),
                    KeyPop = table.Column<string>(maxLength: 50, nullable: true),
                    MaritalStatus = table.Column<string>(maxLength: 50, nullable: true),
                    OtherKeyPop = table.Column<string>(maxLength: 100, nullable: true),
                    PersonId = table.Column<Guid>(nullable: false),
                    PracticeId = table.Column<Guid>(nullable: false),
                    PreventEnroll = table.Column<bool>(nullable: true),
                    Voided = table.Column<bool>(nullable: false)
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
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(maxLength: 150, nullable: true),
                    Display = table.Column<string>(maxLength: 50, nullable: true),
                    FormId = table.Column<Guid>(nullable: false),
                    PracticeId = table.Column<Guid>(nullable: false),
                    Voided = table.Column<bool>(nullable: false)
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
                    Id = table.Column<Guid>(nullable: false),
                    ActivationCode = table.Column<string>(maxLength: 50, nullable: true),
                    ActivationDate = table.Column<DateTime>(nullable: true),
                    Device = table.Column<string>(maxLength: 150, nullable: true),
                    DeviceCode = table.Column<string>(maxLength: 150, nullable: true),
                    ExpiryDate = table.Column<DateTime>(nullable: true),
                    IPAddress = table.Column<string>(maxLength: 150, nullable: true),
                    Lat = table.Column<decimal>(nullable: true),
                    Lng = table.Column<decimal>(nullable: true),
                    Model = table.Column<string>(maxLength: 150, nullable: true),
                    Notes = table.Column<string>(maxLength: 150, nullable: true),
                    PracticeId = table.Column<Guid>(nullable: false),
                    RequestDate = table.Column<DateTime>(nullable: false),
                    Voided = table.Column<bool>(nullable: false)
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
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    Initials = table.Column<string>(maxLength: 50, nullable: true),
                    PersonId = table.Column<Guid>(nullable: false),
                    Phone = table.Column<int>(nullable: true),
                    PracticeId = table.Column<Guid>(nullable: true),
                    ProviderTypeId = table.Column<string>(maxLength: 50, nullable: true),
                    Source = table.Column<string>(maxLength: 50, nullable: true),
                    SourceRef = table.Column<string>(maxLength: 50, nullable: true),
                    SourceSys = table.Column<string>(maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(nullable: false)
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
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    Password = table.Column<string>(maxLength: 200, nullable: true),
                    PersonId = table.Column<Guid>(nullable: false),
                    Phone = table.Column<int>(nullable: true),
                    PracticeId = table.Column<Guid>(nullable: true),
                    Source = table.Column<string>(maxLength: 50, nullable: true),
                    SourceRef = table.Column<string>(maxLength: 50, nullable: true),
                    SourceSys = table.Column<string>(maxLength: 50, nullable: true),
                    UserName = table.Column<string>(maxLength: 100, nullable: true),
                    Voided = table.Column<bool>(nullable: false)
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
                    Id = table.Column<Guid>(nullable: false),
                    ActionId = table.Column<string>(maxLength: 50, nullable: true),
                    ConditionId = table.Column<string>(maxLength: 50, nullable: true),
                    GotoQuestionId = table.Column<Guid>(nullable: true),
                    Group = table.Column<decimal>(nullable: true),
                    QuestionId = table.Column<Guid>(nullable: false),
                    RefQuestionId = table.Column<Guid>(nullable: true),
                    Response = table.Column<string>(maxLength: 50, nullable: true),
                    ResponseComplex = table.Column<string>(maxLength: 50, nullable: true),
                    ResponseType = table.Column<string>(maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(nullable: false)
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
                    Id = table.Column<Guid>(nullable: false),
                    ActionId = table.Column<string>(maxLength: 50, nullable: true),
                    AltContent = table.Column<string>(maxLength: 50, nullable: true),
                    ClientAttributeId = table.Column<string>(maxLength: 50, nullable: true),
                    ConditionId = table.Column<string>(maxLength: 50, nullable: true),
                    Content = table.Column<string>(maxLength: 50, nullable: true),
                    Group = table.Column<decimal>(nullable: true),
                    QuestionId = table.Column<Guid>(nullable: false),
                    RemoteQuestionId = table.Column<Guid>(nullable: true),
                    Response = table.Column<string>(maxLength: 50, nullable: true),
                    ResponseComplex = table.Column<string>(maxLength: 50, nullable: true),
                    ResponseType = table.Column<string>(maxLength: 50, nullable: true),
                    SelfQuestionId = table.Column<Guid>(nullable: true),
                    Voided = table.Column<bool>(nullable: false)
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
                    Id = table.Column<Guid>(nullable: false),
                    ActionId = table.Column<string>(maxLength: 50, nullable: true),
                    ConditionId = table.Column<string>(maxLength: 50, nullable: true),
                    Group = table.Column<decimal>(nullable: true),
                    QuestionId = table.Column<Guid>(nullable: false),
                    QuestionValidationId = table.Column<Guid>(nullable: false),
                    RefQuestionId = table.Column<Guid>(nullable: true),
                    Response = table.Column<string>(maxLength: 50, nullable: true),
                    ResponseComplex = table.Column<string>(maxLength: 100, nullable: true),
                    ResponseType = table.Column<string>(maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(nullable: false)
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
                    Id = table.Column<Guid>(nullable: false),
                    ActionId = table.Column<string>(maxLength: 50, nullable: true),
                    ConditionId = table.Column<string>(maxLength: 50, nullable: true),
                    Content = table.Column<string>(maxLength: 50, nullable: true),
                    Group = table.Column<decimal>(nullable: true),
                    QuestionId = table.Column<Guid>(nullable: false),
                    Rank = table.Column<decimal>(nullable: true),
                    RefQuestionId = table.Column<Guid>(nullable: true),
                    Response = table.Column<string>(maxLength: 50, nullable: true),
                    ResponseComplex = table.Column<string>(maxLength: 100, nullable: true),
                    ResponseType = table.Column<string>(maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(nullable: false)
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
                    Id = table.Column<Guid>(nullable: false),
                    MaxLimit = table.Column<string>(maxLength: 50, nullable: true),
                    MinLimit = table.Column<string>(maxLength: 50, nullable: true),
                    QuestionId = table.Column<Guid>(nullable: false),
                    Revision = table.Column<int>(nullable: false),
                    ValidatorId = table.Column<string>(maxLength: 50, nullable: true),
                    ValidatorTypeId = table.Column<string>(maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(nullable: false)
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
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Voided = table.Column<bool>(nullable: false)
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
                    Id = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    Identifier = table.Column<string>(maxLength: 100, nullable: true),
                    IdentifierTypeId = table.Column<string>(maxLength: 50, nullable: true),
                    Preferred = table.Column<bool>(nullable: false),
                    RegistrationDate = table.Column<DateTime>(nullable: false),
                    Voided = table.Column<bool>(nullable: false)
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
                    Id = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    IsIndex = table.Column<bool>(nullable: true),
                    Preferred = table.Column<bool>(nullable: false),
                    RelatedClientId = table.Column<Guid>(nullable: false),
                    RelationshipTypeId = table.Column<string>(maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(nullable: false)
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
                name: "ClientStates",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    EncounterId = table.Column<Guid>(nullable: true),
                    IndexClientId = table.Column<Guid>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    StatusDate = table.Column<DateTime>(nullable: false),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientStates_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Encounters",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    DeviceId = table.Column<Guid>(nullable: false),
                    EncounterDate = table.Column<DateTime>(nullable: false),
                    EncounterTypeId = table.Column<Guid>(nullable: false),
                    FormId = table.Column<Guid>(nullable: false),
                    IndexClientId = table.Column<Guid>(nullable: true),
                    IsComplete = table.Column<bool>(nullable: false),
                    PracticeId = table.Column<Guid>(nullable: false),
                    ProviderId = table.Column<Guid>(nullable: false),
                    Started = table.Column<DateTime>(nullable: true),
                    Stopped = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    Voided = table.Column<bool>(nullable: false)
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
                        name: "FK_Encounters_EncounterTypes_EncounterTypeId",
                        column: x => x.EncounterTypeId,
                        principalTable: "EncounterTypes",
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
                    Id = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    EncounterId = table.Column<Guid>(nullable: false),
                    IsNull = table.Column<bool>(nullable: false),
                    ObsDate = table.Column<DateTime>(nullable: false),
                    QuestionId = table.Column<Guid>(nullable: false),
                    ValueCoded = table.Column<Guid>(nullable: true),
                    ValueDateTime = table.Column<DateTime>(nullable: true),
                    ValueMultiCoded = table.Column<string>(nullable: true),
                    ValueNumeric = table.Column<decimal>(nullable: true),
                    ValueText = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: false)
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
                name: "ObsFamilyTraceResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BookingDate = table.Column<DateTime>(nullable: true),
                    Consent = table.Column<Guid>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    EncounterId = table.Column<Guid>(nullable: false),
                    IndexClientId = table.Column<Guid>(nullable: false),
                    Mode = table.Column<Guid>(nullable: false),
                    ModeDisplay = table.Column<string>(nullable: true),
                    Outcome = table.Column<Guid>(nullable: false),
                    OutcomeDisplay = table.Column<string>(nullable: true),
                    Reminder = table.Column<DateTime>(nullable: true),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObsFamilyTraceResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObsFamilyTraceResults_Encounters_EncounterId",
                        column: x => x.EncounterId,
                        principalTable: "Encounters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObsFinalTestResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<Guid>(nullable: false),
                    CoupleDiscordant = table.Column<Guid>(nullable: true),
                    EncounterId = table.Column<Guid>(nullable: false),
                    FinalResult = table.Column<Guid>(nullable: true),
                    FinalResultCode = table.Column<string>(nullable: true),
                    FirstTestResult = table.Column<Guid>(nullable: true),
                    FirstTestResultCode = table.Column<string>(nullable: true),
                    PnsDeclined = table.Column<Guid>(nullable: true),
                    Remarks = table.Column<string>(maxLength: 100, nullable: true),
                    ResultGiven = table.Column<Guid>(nullable: true),
                    SecondTestResult = table.Column<Guid>(nullable: true),
                    SecondTestResultCode = table.Column<string>(nullable: true),
                    SelfTestOption = table.Column<Guid>(nullable: true),
                    Voided = table.Column<bool>(nullable: false)
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
                    Id = table.Column<Guid>(nullable: false),
                    DateEnrolled = table.Column<DateTime>(nullable: true),
                    DatePromised = table.Column<DateTime>(nullable: true),
                    EncounterId = table.Column<Guid>(nullable: false),
                    EnrollmentId = table.Column<string>(maxLength: 50, nullable: true),
                    FacilityHandedTo = table.Column<string>(maxLength: 50, nullable: true),
                    HandedTo = table.Column<string>(maxLength: 50, nullable: true),
                    ReferredTo = table.Column<string>(maxLength: 50, nullable: true),
                    Remarks = table.Column<string>(maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(nullable: false),
                    WorkerCarde = table.Column<string>(maxLength: 50, nullable: true)
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
                name: "ObsMemberScreenings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BookingDate = table.Column<DateTime>(nullable: false),
                    BookingMet = table.Column<bool>(nullable: false),
                    DateBookingMet = table.Column<DateTime>(nullable: true),
                    Eligibility = table.Column<Guid>(nullable: false),
                    EncounterId = table.Column<Guid>(nullable: false),
                    HivStatus = table.Column<Guid>(nullable: false),
                    IndexClientId = table.Column<Guid>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    ScreeningDate = table.Column<DateTime>(nullable: false),
                    TraceId = table.Column<Guid>(nullable: true),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObsMemberScreenings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObsMemberScreenings_Encounters_EncounterId",
                        column: x => x.EncounterId,
                        principalTable: "Encounters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObsPartnerScreenings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BookingDate = table.Column<DateTime>(nullable: true),
                    BookingMet = table.Column<bool>(nullable: false),
                    DateBookingMet = table.Column<DateTime>(nullable: true),
                    Eligibility = table.Column<Guid>(nullable: false),
                    EncounterId = table.Column<Guid>(nullable: false),
                    HivStatus = table.Column<Guid>(nullable: false),
                    IPVOutcome = table.Column<Guid>(nullable: true),
                    IPVScreening = table.Column<Guid>(nullable: false),
                    IndexClientId = table.Column<Guid>(nullable: false),
                    LivingWithClient = table.Column<Guid>(nullable: true),
                    Occupation = table.Column<string>(nullable: true),
                    PNSApproach = table.Column<Guid>(nullable: true),
                    PNSRealtionship = table.Column<Guid>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhysicalAssult = table.Column<Guid>(nullable: false),
                    PnsAccepted = table.Column<Guid>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    ScreeningDate = table.Column<DateTime>(nullable: false),
                    SexuallyUncomfortable = table.Column<Guid>(nullable: false),
                    Threatened = table.Column<Guid>(nullable: false),
                    TraceId = table.Column<Guid>(nullable: true),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObsPartnerScreenings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObsPartnerScreenings_Encounters_EncounterId",
                        column: x => x.EncounterId,
                        principalTable: "Encounters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObsPartnerTraceResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BookingDate = table.Column<DateTime>(nullable: true),
                    Consent = table.Column<Guid>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    EncounterId = table.Column<Guid>(nullable: false),
                    IndexClientId = table.Column<Guid>(nullable: false),
                    Mode = table.Column<Guid>(nullable: false),
                    ModeDisplay = table.Column<string>(nullable: true),
                    Outcome = table.Column<Guid>(nullable: false),
                    OutcomeDisplay = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObsPartnerTraceResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObsPartnerTraceResults_Encounters_EncounterId",
                        column: x => x.EncounterId,
                        principalTable: "Encounters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObsTestResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Attempt = table.Column<int>(nullable: false),
                    EncounterId = table.Column<Guid>(nullable: false),
                    Expiry = table.Column<DateTime>(nullable: false),
                    IsValid = table.Column<bool>(nullable: false),
                    Kit = table.Column<Guid>(nullable: false),
                    KitDisplay = table.Column<string>(maxLength: 50, nullable: true),
                    KitOther = table.Column<string>(maxLength: 50, nullable: true),
                    LotNumber = table.Column<string>(maxLength: 50, nullable: true),
                    Result = table.Column<Guid>(nullable: false),
                    ResultCode = table.Column<string>(maxLength: 50, nullable: true),
                    ResultDisplay = table.Column<string>(maxLength: 50, nullable: true),
                    TestName = table.Column<string>(maxLength: 50, nullable: true),
                    Voided = table.Column<bool>(nullable: false)
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
                    Id = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    EncounterId = table.Column<Guid>(nullable: false),
                    Mode = table.Column<Guid>(nullable: false),
                    Outcome = table.Column<Guid>(nullable: false),
                    Voided = table.Column<bool>(nullable: false)
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
                name: "IX_ClientStates_ClientId",
                table: "ClientStates",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Concepts_CategoryId",
                table: "Concepts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Encounters_ClientId",
                table: "Encounters",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Encounters_EncounterTypeId",
                table: "Encounters",
                column: "EncounterTypeId");

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
                name: "IX_ObsFamilyTraceResults_EncounterId",
                table: "ObsFamilyTraceResults",
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
                name: "IX_ObsMemberScreenings_EncounterId",
                table: "ObsMemberScreenings",
                column: "EncounterId");

            migrationBuilder.CreateIndex(
                name: "IX_ObsPartnerScreenings_EncounterId",
                table: "ObsPartnerScreenings",
                column: "EncounterId");

            migrationBuilder.CreateIndex(
                name: "IX_ObsPartnerTraceResults_EncounterId",
                table: "ObsPartnerTraceResults",
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
                name: "IX_Programs_EncounterTypeId",
                table: "Programs",
                column: "EncounterTypeId");

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
                name: "IX_SubscriberCohorts_SubscriberSystemId",
                table: "SubscriberCohorts",
                column: "SubscriberSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriberConfigs_SubscriberSystemId",
                table: "SubscriberConfigs",
                column: "SubscriberSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriberMaps_SubscriberSystemId",
                table: "SubscriberMaps",
                column: "SubscriberSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriberMessagess_SubscriberSystemId",
                table: "SubscriberMessagess",
                column: "SubscriberSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriberSqlActions_SubscriberSystemId",
                table: "SubscriberSqlActions",
                column: "SubscriberSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriberTranslations_SubscriberSystemId",
                table: "SubscriberTranslations",
                column: "SubscriberSystemId");

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
                name: "ClientStates");

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
                name: "ObsFamilyTraceResults");

            migrationBuilder.DropTable(
                name: "ObsFinalTestResults");

            migrationBuilder.DropTable(
                name: "ObsLinkages");

            migrationBuilder.DropTable(
                name: "ObsMemberScreenings");

            migrationBuilder.DropTable(
                name: "ObsPartnerScreenings");

            migrationBuilder.DropTable(
                name: "ObsPartnerTraceResults");

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
                name: "SubscriberCohorts");

            migrationBuilder.DropTable(
                name: "SubscriberConfigs");

            migrationBuilder.DropTable(
                name: "SubscriberMaps");

            migrationBuilder.DropTable(
                name: "SubscriberMessagess");

            migrationBuilder.DropTable(
                name: "SubscriberSqlActions");

            migrationBuilder.DropTable(
                name: "SubscriberTranslations");

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
                name: "SubscriberSystems");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "EncounterTypes");

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
