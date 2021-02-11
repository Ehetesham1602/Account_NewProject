using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountErp.DataLayer.Migrations
{
    public partial class dbmigration11022021 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DiffAmmount",
                table: "CreditMemoService",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "NewAmmount",
                table: "CreditMemoService",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OldAmmount",
                table: "CreditMemoService",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DiffAmmount",
                table: "CreditMemo",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "NewAmmount",
                table: "CreditMemo",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OldAmmount",
                table: "CreditMemo",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiffAmmount",
                table: "CreditMemoService");

            migrationBuilder.DropColumn(
                name: "NewAmmount",
                table: "CreditMemoService");

            migrationBuilder.DropColumn(
                name: "OldAmmount",
                table: "CreditMemoService");

            migrationBuilder.DropColumn(
                name: "DiffAmmount",
                table: "CreditMemo");

            migrationBuilder.DropColumn(
                name: "NewAmmount",
                table: "CreditMemo");

            migrationBuilder.DropColumn(
                name: "OldAmmount",
                table: "CreditMemo");
        }
    }
}
