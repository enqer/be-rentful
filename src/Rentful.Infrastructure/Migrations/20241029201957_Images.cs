using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Rentful.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Images : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "location_id",
                schema: "rentful",
                table: "apartments",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "deposit",
                schema: "rentful",
                table: "apartments",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "price",
                schema: "rentful",
                table: "apartments",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "rent",
                schema: "rentful",
                table: "apartments",
                type: "double precision",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "images",
                schema: "rentful",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    is_thumbnail = table.Column<bool>(type: "boolean", nullable: false),
                    source = table.Column<string>(type: "text", nullable: false),
                    apartment_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_images", x => x.id);
                    table.ForeignKey(
                        name: "FK_images_apartments_apartment_id",
                        column: x => x.apartment_id,
                        principalSchema: "rentful",
                        principalTable: "apartments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_images_apartment_id",
                schema: "rentful",
                table: "images",
                column: "apartment_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "images",
                schema: "rentful");

            migrationBuilder.DropColumn(
                name: "deposit",
                schema: "rentful",
                table: "apartments");

            migrationBuilder.DropColumn(
                name: "price",
                schema: "rentful",
                table: "apartments");

            migrationBuilder.DropColumn(
                name: "rent",
                schema: "rentful",
                table: "apartments");

            migrationBuilder.AlterColumn<int>(
                name: "location_id",
                schema: "rentful",
                table: "apartments",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
