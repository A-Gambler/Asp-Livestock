using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LiveStok.Migrations
{
    public partial class ListsHide : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<bool>(
                name: "Hide",
                table: "Transport",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Hide",
                table: "Locationts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Hide",
                table: "Buyer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Hide",
                table: "Agent",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hide",
                table: "Transport");

            migrationBuilder.DropColumn(
                name: "Hide",
                table: "Locationts");

            migrationBuilder.DropColumn(
                name: "Hide",
                table: "Buyer");

            migrationBuilder.DropColumn(
                name: "Hide",
                table: "Agent");

           
        }
    }
}
