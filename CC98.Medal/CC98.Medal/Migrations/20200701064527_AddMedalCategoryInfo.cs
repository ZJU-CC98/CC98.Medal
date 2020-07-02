using Microsoft.EntityFrameworkCore.Migrations;

namespace CC98.Medal.Migrations
{
    public partial class AddMedalCategoryInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "MedalCategories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IconUri",
                table: "MedalCategories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "MedalCategories",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "MedalCategories");

            migrationBuilder.DropColumn(
                name: "IconUri",
                table: "MedalCategories");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "MedalCategories");
        }
    }
}
