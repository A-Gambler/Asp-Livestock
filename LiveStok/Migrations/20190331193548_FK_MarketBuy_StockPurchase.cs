using Microsoft.EntityFrameworkCore.Migrations;

namespace LiveStok.Migrations
{
    public partial class FK_MarketBuy_StockPurchase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarketBuys_StockPurchase_StockPurchaseID",
                table: "MarketBuys");

            migrationBuilder.AddForeignKey(
                name: "FK_MarketBuy_StockPurchase",
                table: "MarketBuys",
                column: "StockPurchaseID",
                principalTable: "StockPurchase",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarketBuy_StockPurchase",
                table: "MarketBuys");

            migrationBuilder.AddForeignKey(
                name: "FK_MarketBuys_StockPurchase_StockPurchaseID",
                table: "MarketBuys",
                column: "StockPurchaseID",
                principalTable: "StockPurchase",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
