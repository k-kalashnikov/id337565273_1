using Microsoft.EntityFrameworkCore.Migrations;

namespace SP.Contract.Persistence.Migrations
{
    public partial class AddFullNameAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Account",
                nullable: true,
                computedColumnSql: "\"LastName\" || ' ' || \"FirstName\" || ' ' || COALESCE(\"MiddleName\" || ' ', '')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Account");
        }
    }
}
