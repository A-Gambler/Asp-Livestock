using Microsoft.EntityFrameworkCore.Migrations;

namespace LiveStok.Migrations
{
    public partial class EstimatedWeightToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EstimatedWeight",
                table: "StockPurchase",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "EstimatedWeight",
                table: "StockPurchase",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);
        }
    }
}
