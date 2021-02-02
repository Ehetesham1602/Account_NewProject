using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountErp.DataLayer.Migrations
{
    public partial class quotationTypechange02022021 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceType",
                table: "Quotations");

            migrationBuilder.AddColumn<int>(
                name: "QuotationType",
                table: "Quotations",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuotationType",
                table: "Quotations");

            migrationBuilder.AddColumn<int>(
                name: "InvoiceType",
                table: "Quotations",
                nullable: false,
                defaultValue: 0);
        }
    }
}
