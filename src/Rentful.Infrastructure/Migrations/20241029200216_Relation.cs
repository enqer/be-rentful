using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rentful.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "user_id",
                schema: "rentful",
                table: "announcements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_announcements_user_id",
                schema: "rentful",
                table: "announcements",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_announcements_users_user_id",
                schema: "rentful",
                table: "announcements",
                column: "user_id",
                principalSchema: "rentful",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_announcements_users_user_id",
                schema: "rentful",
                table: "announcements");

            migrationBuilder.DropIndex(
                name: "IX_announcements_user_id",
                schema: "rentful",
                table: "announcements");

            migrationBuilder.DropColumn(
                name: "user_id",
                schema: "rentful",
                table: "announcements");
        }
    }
}
