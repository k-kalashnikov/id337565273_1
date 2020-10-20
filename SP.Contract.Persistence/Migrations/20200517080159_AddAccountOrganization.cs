using Microsoft.EntityFrameworkCore.Migrations;

namespace SP.Contract.Persistence.Migrations
{
    public partial class AddAccountOrganization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "CreatedById",
                table: "ContractDocument",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationID",
                table: "Account",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Account_OrganizationID",
                table: "Account",
                column: "OrganizationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Organization_OrganizationID",
                table: "Account",
                column: "OrganizationID",
                principalTable: "Organization",
                principalColumn: "OrganizationID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_Organization_OrganizationID",
                table: "Account");

            migrationBuilder.DropIndex(
                name: "IX_Account_OrganizationID",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "OrganizationID",
                table: "Account");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedById",
                table: "ContractDocument",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));
        }
    }
}
