using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rentful.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "time",
                schema: "rentful",
                table: "reservations");

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                schema: "rentful",
                table: "reservations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                schema: "rentful",
                table: "reservations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "time",
                schema: "rentful",
                table: "reservations",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
