using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountErp.DataLayer.Migrations
{
    public partial class quotationchange02022021 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuotationServices_Items_ServiceId",
                table: "QuotationServices");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceId",
                table: "QuotationServices",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "QuotationServices",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InvoiceType",
                table: "Quotations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_QuotationServices_ProductId",
                table: "QuotationServices",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationServices_Products_ProductId",
                table: "QuotationServices",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationServices_Items_ServiceId",
                table: "QuotationServices",
                column: "ServiceId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuotationServices_Products_ProductId",
                table: "QuotationServices");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationServices_Items_ServiceId",
                table: "QuotationServices");

            migrationBuilder.DropIndex(
                name: "IX_QuotationServices_ProductId",
                table: "QuotationServices");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "QuotationServices");

            migrationBuilder.DropColumn(
                name: "InvoiceType",
                table: "Quotations");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceId",
                table: "QuotationServices",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationServices_Items_ServiceId",
                table: "QuotationServices",
                column: "ServiceId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
