using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace flashlightapi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIdentityRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c1dda0b-b9a2-4a44-af1a-e8babeef5031");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9d1cf010-a07e-4836-b239-8551df769f07");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Assignment");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b122ef39-3a39-4962-bfbe-7e776b007a12", null, "Admin", "ADMIN" },
                    { "e1635ea3-6e38-40dc-b51e-a79ed1945857", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b122ef39-3a39-4962-bfbe-7e776b007a12");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e1635ea3-6e38-40dc-b51e-a79ed1945857");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "Assignment",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4c1dda0b-b9a2-4a44-af1a-e8babeef5031", null, "User", "USER" },
                    { "9d1cf010-a07e-4836-b239-8551df769f07", null, "Admin", "ADMIN" }
                });
        }
    }
}
