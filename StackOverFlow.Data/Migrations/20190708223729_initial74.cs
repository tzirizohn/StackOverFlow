using Microsoft.EntityFrameworkCore.Migrations;

namespace StackOverFlow.Data.Migrations
{
    public partial class initial74 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Like_QuestionId",
                table: "Like");

            migrationBuilder.CreateIndex(
                name: "IX_Like_QuestionId",
                table: "Like",
                column: "QuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Like_QuestionId",
                table: "Like");

            migrationBuilder.CreateIndex(
                name: "IX_Like_QuestionId",
                table: "Like",
                column: "QuestionId",
                unique: true);
        }
    }
}
