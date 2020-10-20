using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SP.Contract.Persistence.Migrations
{
    public partial class AddContractDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractDocument",
                columns: table => new
                {
                    ContractDocumentID = table.Column<Guid>(nullable: false),
                    ContractID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    FileName = table.Column<string>(maxLength: 255, nullable: false),
                    Link = table.Column<string>(maxLength: 255, nullable: false),
                    CreatedById = table.Column<long>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    ModifiedById = table.Column<long>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractDocumentID", x => x.ContractDocumentID);
                    table.ForeignKey(
                        name: "FK_ContractDocument_Account_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractDocument_Account_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "Account",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractDocument_Contract_ContractID",
                        column: x => x.ContractID,
                        principalTable: "Contract",
                        principalColumn: "ContractID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractDocument_CreatedById",
                table: "ContractDocument",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ContractDocument_ModifiedById",
                table: "ContractDocument",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ContractDocument_ContractID",
                table: "ContractDocument",
                column: "ContractID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractDocument");
        }
    }
}
