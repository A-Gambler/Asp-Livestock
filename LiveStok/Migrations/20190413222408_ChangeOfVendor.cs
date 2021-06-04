using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LiveStok.Migrations
{
    public partial class ChangeOfVendor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockPurchase_Vendor",
                table: "StockPurchase");

            migrationBuilder.AlterColumn<Guid>(
                name: "VendorID",
                table: "StockPurchase",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<string>(
                name: "VendorName",
                table: "StockPurchase",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StockPurchase_Vendor_VendorID",
                table: "StockPurchase",
                column: "VendorID",
                principalTable: "Vendor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockPurchase_Vendor_VendorID",
                table: "StockPurchase");

            migrationBuilder.DropColumn(
                name: "VendorName",
                table: "StockPurchase");

            migrationBuilder.AlterColumn<Guid>(
                name: "VendorID",
                table: "StockPurchase",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StockPurchase_Vendor",
                table: "StockPurchase",
                column: "VendorID",
                principalTable: "Vendor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
