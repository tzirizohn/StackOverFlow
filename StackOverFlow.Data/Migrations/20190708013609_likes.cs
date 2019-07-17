using Microsoft.EntityFrameworkCore.Migrations;

namespace StackOverFlow.Data.Migrations
{
    public partial class likes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Like_QuestionId",
                table: "Like");

            migrationBuilder.CreateIndex(
                name: "IX_Like_QuestionId",
                table: "Like",
                column: "QuestionId");
        }
    }
}
