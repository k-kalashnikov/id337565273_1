﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SP.Contract.Persistence;

namespace SP.Contract.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200811132246_AddFullNameAccount")]
    partial class AddFullNameAccount
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("SP.Contract.Domains.AggregatesModel.Contract.Entities.Contract", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ContractID")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("FinishDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.Property<Guid?>("Parent")
                        .HasColumnType("uuid");

                    b.Property<bool?>("SignedByContractor")
                        .HasColumnType("boolean");

                    b.Property<bool?>("SignedByCustomer")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("_contractStatusId")
                        .HasColumnName("ContractStatusID")
                        .HasColumnType("integer");

                    b.Property<int>("_contractTypeId")
                        .HasColumnName("ContractTypeID")
                        .HasColumnType("integer");

                    b.Property<long>("_contractorOrganizationId")
                        .HasColumnName("ContractorOrganizationID")
                        .HasColumnType("bigint");

                    b.Property<long>("_createdById")
                        .HasColumnName("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<long>("_customerOrganizationId")
                        .HasColumnName("CustomerOrganizationID")
                        .HasColumnType("bigint");

                    b.Property<long?>("_modifiedById")
                        .HasColumnName("ModifiedBy")
                        .HasColumnType("bigint");

                    b.HasKey("Id")
                        .HasName("PK_ContractID");

                    b.HasIndex("_contractStatusId")
                        .HasName("IX_Contract_ContractStatusID");

                    b.HasIndex("_contractTypeId")
                        .HasName("IX_Contract_ContractTypeID");

                    b.HasIndex("_contractorOrganizationId")
                        .HasName("IX_Contract_ContractorOrganizationID");

                    b.HasIndex("_createdById")
                        .HasName("IX_Contract_CreatedBy");

                    b.HasIndex("_customerOrganizationId")
                        .HasName("IX_Contract_CustomerOrganizationID");

                    b.HasIndex("_modifiedById")
                        .HasName("IX_Contract_ModifiedBy");

                    b.ToTable("Contract");
                });

            modelBuilder.Entity("SP.Contract.Domains.AggregatesModel.Contract.Entities.ContractDocument", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ContractDocumentID")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("Deleted")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("Modified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.Property<Guid>("_contractId")
                        .HasColumnName("ContractID")
                        .HasColumnType("uuid");

                    b.Property<long>("_createdById")
                        .HasColumnName("CreatedById")
                        .HasColumnType("bigint");

                    b.Property<long?>("_modifiedById")
                        .HasColumnName("ModifiedById")
                        .HasColumnType("bigint");

                    b.HasKey("Id")
                        .HasName("PK_ContractDocumentID");

                    b.HasIndex("_contractId")
                        .HasName("IX_ContractDocument_ContractID");

                    b.HasIndex("_createdById")
                        .HasName("IX_ContractDocument_CreatedById");

                    b.HasIndex("_modifiedById")
                        .HasName("IX_ContractDocument_ModifiedById");

                    b.ToTable("ContractDocument");
                });

            modelBuilder.Entity("SP.Contract.Domains.AggregatesModel.Contract.Entities.ContractStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("ContractStatusID")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id")
                        .HasName("PK_ContractStatusID");

                    b.ToTable("ContractStatus");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Черновик"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Подписан"
                        });
                });

            modelBuilder.Entity("SP.Contract.Domains.AggregatesModel.Contract.Entities.ContractType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("ContractTypeID")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id")
                        .HasName("PK_ContractTypeID");

                    b.ToTable("ContractType");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Рамочный"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Прейскурантный"
                        });
                });

            modelBuilder.Entity("SP.Contract.Domains.AggregatesModel.Misc.Entities.Account", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnName("AccountID")
                        .HasColumnType("bigint");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.Property<string>("FullName")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("text")
                        .HasComputedColumnSql("\"LastName\" || ' ' || \"FirstName\" || ' ' || COALESCE(\"MiddleName\" || ' ', '')");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.Property<string>("MiddleName")
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.Property<long?>("_organizationId")
                        .HasColumnName("OrganizationID")
                        .HasColumnType("bigint");

                    b.HasKey("Id")
                        .HasName("PK_AccountID");

                    b.HasIndex("_organizationId")
                        .HasName("IX_Account_OrganizationID");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("SP.Contract.Domains.AggregatesModel.Misc.Entities.Organization", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnName("OrganizationID")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id")
                        .HasName("PK_OrganizationID");

                    b.ToTable("Organization");
                });

            modelBuilder.Entity("SP.Contract.Domains.AggregatesModel.Contract.Entities.Contract", b =>
                {
                    b.HasOne("SP.Contract.Domains.AggregatesModel.Contract.Entities.ContractStatus", "ContractStatus")
                        .WithMany()
                        .HasForeignKey("_contractStatusId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SP.Contract.Domains.AggregatesModel.Contract.Entities.ContractType", "ContractType")
                        .WithMany()
                        .HasForeignKey("_contractTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SP.Contract.Domains.AggregatesModel.Misc.Entities.Organization", "ContractorOrganization")
                        .WithMany()
                        .HasForeignKey("_contractorOrganizationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SP.Contract.Domains.AggregatesModel.Misc.Entities.Account", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("_createdById")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SP.Contract.Domains.AggregatesModel.Misc.Entities.Organization", "CustomerOrganization")
                        .WithMany()
                        .HasForeignKey("_customerOrganizationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SP.Contract.Domains.AggregatesModel.Misc.Entities.Account", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("_modifiedById")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("SP.Contract.Domains.AggregatesModel.Contract.Entities.ContractDocument", b =>
                {
                    b.HasOne("SP.Contract.Domains.AggregatesModel.Contract.Entities.Contract", "Contract")
                        .WithMany()
                        .HasForeignKey("_contractId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SP.Contract.Domains.AggregatesModel.Misc.Entities.Account", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("_createdById")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SP.Contract.Domains.AggregatesModel.Misc.Entities.Account", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("_modifiedById")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("SP.Contract.Domains.AggregatesModel.Misc.Entities.Account", b =>
                {
                    b.HasOne("SP.Contract.Domains.AggregatesModel.Misc.Entities.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("_organizationId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
