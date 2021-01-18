using Microsoft.EntityFrameworkCore.Migrations;

namespace CC98.Medal.Migrations
{
    public partial class AddOwnerSortOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "UserMedalOwnerships",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "UserMedalOwnerships");
        }
    }
}
