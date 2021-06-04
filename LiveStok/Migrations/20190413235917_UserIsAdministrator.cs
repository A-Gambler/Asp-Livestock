using Microsoft.EntityFrameworkCore.Migrations;

namespace LiveStok.Migrations
{
    public partial class UserIsAdministrator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdministrator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdministrator",
                table: "AspNetUsers");
        }
    }
}
