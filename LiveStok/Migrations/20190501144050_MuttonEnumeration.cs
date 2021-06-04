using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LiveStok.Migrations
{
    public partial class MuttonEnumeration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TypeOfAnimals",
                keyColumn: "ID",
                keyValue: new Guid("1550b1b3-43aa-4df8-9a21-a604a160807f"),
                column: "Name",
                value: "MUTTON 1");

            migrationBuilder.InsertData(
                table: "TypeOfAnimals",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { new Guid("b58b1bab-b7c0-424c-b1bd-6b28f7dc5173"), "MUTTON 2" },
                    { new Guid("c1147554-fd45-4381-9f66-eab06a34eea7"), "MUTTON 5" },
                    { new Guid("29106f56-4edb-4358-a4d5-cd234fe5212c"), "MUTTON 6" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TypeOfAnimals",
                keyColumn: "ID",
                keyValue: new Guid("29106f56-4edb-4358-a4d5-cd234fe5212c"));

            migrationBuilder.DeleteData(
                table: "TypeOfAnimals",
                keyColumn: "ID",
                keyValue: new Guid("b58b1bab-b7c0-424c-b1bd-6b28f7dc5173"));

            migrationBuilder.DeleteData(
                table: "TypeOfAnimals",
                keyColumn: "ID",
                keyValue: new Guid("c1147554-fd45-4381-9f66-eab06a34eea7"));

            migrationBuilder.UpdateData(
                table: "TypeOfAnimals",
                keyColumn: "ID",
                keyValue: new Guid("1550b1b3-43aa-4df8-9a21-a604a160807f"),
                column: "Name",
                value: "MUTTON");
        }
    }
}
