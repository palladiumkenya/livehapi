﻿// <auto-generated />
using LiveHAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace LiveHAPI.Infrastructure.Migrations
{
    [DbContext(typeof(LiveHAPIContext))]
    [Migration("20170907121021_hAPIInitial")]
    partial class hAPIInitial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LiveHAPI.Core.Model.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("KeyPop")
                        .HasMaxLength(50);

                    b.Property<string>("MaritalStatus")
                        .HasMaxLength(50);

                    b.Property<string>("OtherKeyPop")
                        .HasMaxLength(100);

                    b.Property<Guid>("PersonId");

                    b.Property<Guid>("PracticeId");

                    b.Property<bool>("Voided");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.HasIndex("PracticeId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("LiveHAPI.Core.Model.ClientAttribute", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ClientId");

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<bool>("Voided");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("ClientAttributes");
                });

            modelBuilder.Entity("LiveHAPI.Core.Model.ClientIdentifier", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ClientId");

                    b.Property<string>("Identifier")
                        .HasMaxLength(100);

                    b.Property<string>("IdentifierTypeId")
                        .HasMaxLength(50);

                    b.Property<bool>("Preferred");

                    b.Property<DateTime>("RegistrationDate");

                    b.Property<bool>("Voided");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("IdentifierTypeId");

                    b.ToTable("ClientIdentifiers");
                });

            modelBuilder.Entity("LiveHAPI.Core.Model.ClientRelationship", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ClientId");

                    b.Property<bool>("Preferred");

                    b.Property<Guid>("RelatedClientId");

                    b.Property<string>("RelationshipTypeId")
                        .HasMaxLength(50);

                    b.Property<bool>("Voided");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("RelationshipTypeId");

                    b.ToTable("ClientRelationships");
                });

            modelBuilder.Entity("LiveHAPI.Core.Model.County", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("Voided");

                    b.HasKey("Id");

                    b.ToTable("Counties");
                });

            modelBuilder.Entity("LiveHAPI.Core.Model.IdentifierType", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<bool>("Voided");

                    b.HasKey("Id");

                    b.ToTable("IdentifierTypes");
                });

            modelBuilder.Entity("LiveHAPI.Core.Model.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BirthDate");

                    b.Property<bool?>("BirthDateEstimated");

                    b.Property<string>("Email")
                        .HasMaxLength(100);

                    b.Property<string>("FirstName")
                        .HasMaxLength(100);

                    b.Property<string>("Gender")
                        .HasMaxLength(10);

                    b.Property<string>("LastName")
                        .HasMaxLength(100);

                    b.Property<string>("MiddleName")
                        .HasMaxLength(100);

                    b.Property<bool>("Voided");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("LiveHAPI.Core.Model.PersonAddress", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CountyId");

                    b.Property<string>("Landmark")
                        .HasMaxLength(200);

                    b.Property<decimal?>("Lat");

                    b.Property<decimal?>("Lng");

                    b.Property<Guid>("PersonId");

                    b.Property<bool>("Preferred");

                    b.Property<bool>("Voided");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("PersonAddresss");
                });

            modelBuilder.Entity("LiveHAPI.Core.Model.PersonContact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("PersonId");

                    b.Property<int?>("Phone");

                    b.Property<bool>("Preferred");

                    b.Property<bool>("Voided");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("PersonContacts");
                });

            modelBuilder.Entity("LiveHAPI.Core.Model.Practice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .HasMaxLength(20);

                    b.Property<int?>("CountyId");

                    b.Property<bool>("IsDefault");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("PracticeTypeId")
                        .HasMaxLength(50);

                    b.Property<bool>("Voided");

                    b.HasKey("Id");

                    b.HasIndex("CountyId");

                    b.HasIndex("PracticeTypeId");

                    b.ToTable("Practices");
                });

            modelBuilder.Entity("LiveHAPI.Core.Model.PracticeType", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<bool>("Voided");

                    b.HasKey("Id");

                    b.ToTable("PracticeTypes");
                });

            modelBuilder.Entity("LiveHAPI.Core.Model.Provider", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .HasMaxLength(50);

                    b.Property<Guid>("PersonId");

                    b.Property<Guid>("PracticeId");

                    b.Property<string>("ProviderTypeId")
                        .HasMaxLength(50);

                    b.Property<bool>("Voided");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.HasIndex("PracticeId");

                    b.HasIndex("ProviderTypeId");

                    b.ToTable("Providers");
                });

            modelBuilder.Entity("LiveHAPI.Core.Model.ProviderType", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<bool>("Voided");

                    b.HasKey("Id");

                    b.ToTable("ProviderTypes");
                });

            modelBuilder.Entity("LiveHAPI.Core.Model.RelationshipType", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50);

                    b.Property<string>("Description")
                        .HasMaxLength(200);

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<bool>("Voided");

                    b.HasKey("Id");

                    b.ToTable("RelationshipTypes");
                });

            modelBuilder.Entity("LiveHAPI.Core.Model.SubCounty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CountyId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("Voided");

                    b.HasKey("Id");

                    b.HasIndex("CountyId");

                    b.ToTable("SubCounties");
                });

            modelBuilder.Entity("LiveHAPI.Core.Model.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password")
                        .HasMaxLength(200);

                    b.Property<Guid>("PersonId");

                    b.Property<Guid?>("PracticeId");

                    b.Property<string>("UserName")
                        .HasMaxLength(100);

                    b.Property<bool>("Voided");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.HasIndex("PracticeId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LiveHAPI.Core.Model.Client", b =>
                {
                    b.HasOne("LiveHAPI.Core.Model.Person")
                        .WithMany("Clients")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LiveHAPI.Core.Model.Practice")
                        .WithMany("Clients")
                        .HasForeignKey("PracticeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LiveHAPI.Core.Model.ClientAttribute", b =>
                {
                    b.HasOne("LiveHAPI.Core.Model.Client")
                        .WithMany("Attributes")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LiveHAPI.Core.Model.ClientIdentifier", b =>
                {
                    b.HasOne("LiveHAPI.Core.Model.Client")
                        .WithMany("Identifiers")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LiveHAPI.Core.Model.IdentifierType")
                        .WithMany("ClientIdentifiers")
                        .HasForeignKey("IdentifierTypeId");
                });

            modelBuilder.Entity("LiveHAPI.Core.Model.ClientRelationship", b =>
                {
                    b.HasOne("LiveHAPI.Core.Model.Client")
                        .WithMany("Relationships")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LiveHAPI.Core.Model.RelationshipType")
                        .WithMany("ClientRelationships")
                        .HasForeignKey("RelationshipTypeId");
                });

            modelBuilder.Entity("LiveHAPI.Core.Model.PersonAddress", b =>
                {
                    b.HasOne("LiveHAPI.Core.Model.Person")
                        .WithMany("Addresses")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LiveHAPI.Core.Model.PersonContact", b =>
                {
                    b.HasOne("LiveHAPI.Core.Model.Person")
                        .WithMany("Contacts")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LiveHAPI.Core.Model.Practice", b =>
                {
                    b.HasOne("LiveHAPI.Core.Model.County")
                        .WithMany("Practices")
                        .HasForeignKey("CountyId");

                    b.HasOne("LiveHAPI.Core.Model.PracticeType")
                        .WithMany("Practices")
                        .HasForeignKey("PracticeTypeId");
                });

            modelBuilder.Entity("LiveHAPI.Core.Model.Provider", b =>
                {
                    b.HasOne("LiveHAPI.Core.Model.Person")
                        .WithMany("Providers")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LiveHAPI.Core.Model.Practice")
                        .WithMany("Providers")
                        .HasForeignKey("PracticeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LiveHAPI.Core.Model.ProviderType")
                        .WithMany("Providers")
                        .HasForeignKey("ProviderTypeId");
                });

            modelBuilder.Entity("LiveHAPI.Core.Model.SubCounty", b =>
                {
                    b.HasOne("LiveHAPI.Core.Model.County")
                        .WithMany("SubCounties")
                        .HasForeignKey("CountyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LiveHAPI.Core.Model.User", b =>
                {
                    b.HasOne("LiveHAPI.Core.Model.Person")
                        .WithMany("Users")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LiveHAPI.Core.Model.Practice")
                        .WithMany("Users")
                        .HasForeignKey("PracticeId");
                });
#pragma warning restore 612, 618
        }
    }
}