using Microsoft.EntityFrameworkCore.Migrations;

namespace Accoon.Api.DataServices.Entities.Migrations
{
    public partial class migrations2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Customers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Customers",
                nullable: false,
                defaultValue: 0);
        }
    }
}
