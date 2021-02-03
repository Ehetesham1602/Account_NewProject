using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountErp.DataLayer.Migrations
{
    public partial class creditMemo03022021 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CreditMemo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<int>(nullable: false),
                    InvoiceNumber = table.Column<string>(maxLength: 50, nullable: false),
                    Tax = table.Column<decimal>(type: "NUMERIC(12,2)", nullable: true),
                    Discount = table.Column<decimal>(type: "NUMERIC(12,2)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "NUMERIC(12,2)", nullable: false),
                    Remark = table.Column<string>(maxLength: 1000, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 40, nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 40, nullable: true),
                    StrInvoiceDate = table.Column<string>(nullable: true),
                    InvoiceDate = table.Column<DateTime>(nullable: false),
                    StrDueDate = table.Column<string>(nullable: true),
                    DueDate = table.Column<DateTime>(nullable: false),
                    PoSoNumber = table.Column<decimal>(type: "NUMERIC(12,2)", nullable: true),
                    SubTotal = table.Column<decimal>(type: "NUMERIC(12,2)", nullable: true),
                    LineAmountSubTotal = table.Column<decimal>(type: "NUMERIC(12,2)", nullable: true),
                    InvoiceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditMemo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditMemo_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CreditMemo_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CreditMemoService",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreditMemoId = table.Column<int>(nullable: false),
                    ServiceId = table.Column<int>(nullable: true),
                    ProductId = table.Column<int>(nullable: true),
                    Rate = table.Column<decimal>(type: "NUMERIC(12,2)", nullable: false),
                    Price = table.Column<decimal>(type: "NUMERIC(12,2)", nullable: false),
                    TaxId = table.Column<int>(nullable: true),
                    TaxPrice = table.Column<decimal>(type: "NUMERIC(12,2)", nullable: false),
                    TaxPercentage = table.Column<decimal>(nullable: true),
                    LineAmount = table.Column<decimal>(type: "NUMERIC(12,2)", nullable: false),
                    OldQuantity = table.Column<int>(nullable: false),
                    NewQuantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditMemoService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditMemoService_CreditMemo_CreditMemoId",
                        column: x => x.CreditMemoId,
                        principalTable: "CreditMemo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CreditMemoService_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CreditMemoService_Items_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CreditMemoService_SalesTaxes_TaxId",
                        column: x => x.TaxId,
                        principalTable: "SalesTaxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreditMemo_CustomerId",
                table: "CreditMemo",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditMemo_InvoiceId",
                table: "CreditMemo",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditMemoService_CreditMemoId",
                table: "CreditMemoService",
                column: "CreditMemoId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditMemoService_ProductId",
                table: "CreditMemoService",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditMemoService_ServiceId",
                table: "CreditMemoService",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditMemoService_TaxId",
                table: "CreditMemoService",
                column: "TaxId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditMemoService");

            migrationBuilder.DropTable(
                name: "CreditMemo");
        }
    }
}
