using Microsoft.EntityFrameworkCore.Migrations;

namespace CC98.Medal.Migrations
{
    public partial class AddMedalInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanApply",
                table: "Medals",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanBuy",
                table: "Medals",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HideOwners",
                table: "Medals",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Medals",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanApply",
                table: "Medals");

            migrationBuilder.DropColumn(
                name: "CanBuy",
                table: "Medals");

            migrationBuilder.DropColumn(
                name: "HideOwners",
                table: "Medals");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Medals");
        }
    }
}
