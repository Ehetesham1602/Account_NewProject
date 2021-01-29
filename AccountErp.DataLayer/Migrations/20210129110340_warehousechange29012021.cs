using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountErp.DataLayer.Migrations
{
    public partial class warehousechange29012021 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WareHouseId",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_WareHouseId",
                table: "Products",
                column: "WareHouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_WareHouse_WareHouseId",
                table: "Products",
                column: "WareHouseId",
                principalTable: "WareHouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_WareHouse_WareHouseId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_WareHouseId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "WareHouseId",
                table: "Products");
        }
    }
}
