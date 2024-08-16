using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace flashlightapi.Migrations
{
    /// <inheritdoc />
    public partial class AssignmentAddAppUserFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Assignment",
                type: "text",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_CreatedById",
                table: "Assignment",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignment_AspNetUsers_CreatedById",
                table: "Assignment",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignment_AspNetUsers_CreatedById",
                table: "Assignment");

            migrationBuilder.DropIndex(
                name: "IX_Assignment_CreatedById",
                table: "Assignment");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Assignment");
        }
    }
}
