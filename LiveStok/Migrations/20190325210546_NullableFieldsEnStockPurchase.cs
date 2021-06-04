using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LiveStok.Migrations
{
    public partial class NullableFieldsEnStockPurchase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockPurchase_TypeOfAnimal",
                table: "StockPurchase");

            migrationBuilder.DropColumn(
                name: "PurchaseType",
                table: "StockPurchase");

            migrationBuilder.AlterColumn<Guid>(
                name: "TypeOfAnimalID",
                table: "StockPurchase",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "StockPurchase",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<int>(
                name: "Number",
                table: "StockPurchase",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<decimal>(
                name: "Freight",
                table: "StockPurchase",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "EstimatedWeight",
                table: "StockPurchase",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.InsertData(
                table: "TypeOfAnimals",
                columns: new[] { "ID", "Name" },
                values: new object[] { new Guid("8c9f5370-d15e-4876-9c34-2223734d9ab5"), "GOATS" });

            migrationBuilder.CreateIndex(
                name: "IX_MarketBuySummaries_TypeOfAnimalID",
                table: "MarketBuySummaries",
                column: "TypeOfAnimalID");

            migrationBuilder.AddForeignKey(
                name: "FK_MarketBuySummaries_TypeOfAnimals_TypeOfAnimalID",
                table: "MarketBuySummaries",
                column: "TypeOfAnimalID",
                principalTable: "TypeOfAnimals",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StockPurchase_TypeOfAnimal",
                table: "StockPurchase",
                column: "TypeOfAnimalID",
                principalTable: "TypeOfAnimals",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarketBuySummaries_TypeOfAnimals_TypeOfAnimalID",
                table: "MarketBuySummaries");

            migrationBuilder.DropForeignKey(
                name: "FK_StockPurchase_TypeOfAnimal",
                table: "StockPurchase");

            migrationBuilder.DropIndex(
                name: "IX_MarketBuySummaries_TypeOfAnimalID",
                table: "MarketBuySummaries");

            migrationBuilder.DeleteData(
                table: "TypeOfAnimals",
                keyColumn: "ID",
                keyValue: new Guid("8c9f5370-d15e-4876-9c34-2223734d9ab5"));

            migrationBuilder.AlterColumn<Guid>(
                name: "TypeOfAnimalID",
                table: "StockPurchase",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "StockPurchase",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Number",
                table: "StockPurchase",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Freight",
                table: "StockPurchase",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "EstimatedWeight",
                table: "StockPurchase",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PurchaseType",
                table: "StockPurchase",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StockPurchase_TypeOfAnimal",
                table: "StockPurchase",
                column: "TypeOfAnimalID",
                principalTable: "TypeOfAnimals",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
