using Microsoft.EntityFrameworkCore.Migrations;

namespace CC98.Medal.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MedalCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedalCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedalCategories_MedalCategories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "MedalCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Medals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ImageUri = table.Column<string>(nullable: false),
                    LinkUri = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medals_MedalCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "MedalCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedalCategories_ParentId",
                table: "MedalCategories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Medals_CategoryId",
                table: "Medals",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Medals");

            migrationBuilder.DropTable(
                name: "MedalCategories");
        }
    }
}
