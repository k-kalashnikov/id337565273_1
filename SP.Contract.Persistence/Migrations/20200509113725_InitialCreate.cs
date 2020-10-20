using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SP.Contract.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(maxLength: 255, nullable: false),
                    LastName = table.Column<string>(maxLength: 255, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountID", x => x.AccountID);
                });

            migrationBuilder.CreateTable(
                name: "ContractStatus",
                columns: table => new
                {
                    ContractStatusID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractStatusID", x => x.ContractStatusID);
                });

            migrationBuilder.CreateTable(
                name: "ContractType",
                columns: table => new
                {
                    ContractTypeID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractTypeID", x => x.ContractTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    CurrencyID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Code = table.Column<string>(maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyID", x => x.CurrencyID);
                });

            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    OrganizationID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationID", x => x.OrganizationID);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    ContractID = table.Column<Guid>(nullable: false),
                    ContractStatusID = table.Column<int>(nullable: false),
                    ContractTypeID = table.Column<int>(nullable: false),
                    CurrencyID = table.Column<long>(nullable: false),
                    CustomerOrganizationID = table.Column<long>(nullable: false),
                    ContractorOrganizationID = table.Column<long>(nullable: false),
                    Number = table.Column<string>(maxLength: 255, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    FinishDate = table.Column<DateTime>(nullable: false),
                    SignedByCustomer = table.Column<bool>(nullable: true),
                    SignedByContractor = table.Column<bool>(nullable: true),
                    Parent = table.Column<Guid>(nullable: true),
                    CreatedBy = table.Column<long>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractID", x => x.ContractID);
                    table.ForeignKey(
                        name: "FK_Contract_ContractStatus_ContractStatusID",
                        column: x => x.ContractStatusID,
                        principalTable: "ContractStatus",
                        principalColumn: "ContractStatusID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_ContractType_ContractTypeID",
                        column: x => x.ContractTypeID,
                        principalTable: "ContractType",
                        principalColumn: "ContractTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_Organization_ContractorOrganizationID",
                        column: x => x.ContractorOrganizationID,
                        principalTable: "Organization",
                        principalColumn: "OrganizationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_Account_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_Currency_CurrencyID",
                        column: x => x.CurrencyID,
                        principalTable: "Currency",
                        principalColumn: "CurrencyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_Organization_CustomerOrganizationID",
                        column: x => x.CustomerOrganizationID,
                        principalTable: "Organization",
                        principalColumn: "OrganizationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_Account_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ContractStatus",
                columns: new[] { "ContractStatusID", "Name" },
                values: new object[,]
                {
                    { 1, "Черновик" },
                    { 2, "Подписан" }
                });

            migrationBuilder.InsertData(
                table: "ContractType",
                columns: new[] { "ContractTypeID", "Name" },
                values: new object[,]
                {
                    { 1, "Рамочный" },
                    { 2, "Прейскурантный" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ContractStatusID",
                table: "Contract",
                column: "ContractStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ContractTypeID",
                table: "Contract",
                column: "ContractTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ContractorOrganizationID",
                table: "Contract",
                column: "ContractorOrganizationID");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_CreatedBy",
                table: "Contract",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_CurrencyID",
                table: "Contract",
                column: "CurrencyID");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_CustomerOrganizationID",
                table: "Contract",
                column: "CustomerOrganizationID");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_ModifiedBy",
                table: "Contract",
                column: "ModifiedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropTable(
                name: "ContractStatus");

            migrationBuilder.DropTable(
                name: "ContractType");

            migrationBuilder.DropTable(
                name: "Organization");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Currency");
        }
    }
}
