using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CC98.Medal.Migrations
{
    public partial class AddMedalOwnership : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MedalIssueRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MedalId = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedalIssueRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedalIssueRecords_Medals_MedalId",
                        column: x => x.MedalId,
                        principalTable: "Medals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMedalOwnerships",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MedalId = table.Column<int>(type: "int", nullable: false),
                    ExpireTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMedalOwnerships", x => new { x.UserId, x.MedalId });
                    table.ForeignKey(
                        name: "FK_UserMedalOwnerships_Medals_MedalId",
                        column: x => x.MedalId,
                        principalTable: "Medals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedalIssueRecords_MedalId",
                table: "MedalIssueRecords",
                column: "MedalId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMedalOwnerships_MedalId",
                table: "UserMedalOwnerships",
                column: "MedalId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMedalOwnerships_UserId",
                table: "UserMedalOwnerships",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedalIssueRecords");

            migrationBuilder.DropTable(
                name: "UserMedalOwnerships");
        }
    }
}
