using Microsoft.EntityFrameworkCore.Migrations;

namespace SP.Contract.Persistence.Migrations
{
    public partial class RemoveCurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Currency_CurrencyID",
                table: "Contract");

            migrationBuilder.DropIndex(
                name: "IX_Contract_CurrencyID",
                table: "Contract");

            migrationBuilder.DropColumn(
                name: "CurrencyID",
                table: "Contract");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CurrencyID",
                table: "Contract",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Contract_CurrencyID",
                table: "Contract",
                column: "CurrencyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Currency_CurrencyID",
                table: "Contract",
                column: "CurrencyID",
                principalTable: "Currency",
                principalColumn: "CurrencyID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
