using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Rentful.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ApartmentTabel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_Addresses_addressId",
                schema: "public",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.EnsureSchema(
                name: "rentful");

            migrationBuilder.RenameTable(
                name: "users",
                schema: "public",
                newName: "users",
                newSchema: "rentful");

            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "address",
                newSchema: "rentful");

            migrationBuilder.RenameColumn(
                name: "addressId",
                schema: "rentful",
                table: "users",
                newName: "address_id");

            migrationBuilder.RenameIndex(
                name: "IX_users_addressId",
                schema: "rentful",
                table: "users",
                newName: "IX_users_address_id");

            migrationBuilder.AlterColumn<int>(
                name: "address_id",
                schema: "rentful",
                table: "users",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_address",
                schema: "rentful",
                table: "address",
                column: "id");

            migrationBuilder.CreateTable(
                name: "locations",
                schema: "rentful",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    latitude = table.Column<decimal>(type: "numeric", nullable: false),
                    longitude = table.Column<decimal>(type: "numeric", nullable: false),
                    place = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "apartments",
                schema: "rentful",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    area = table.Column<double>(type: "double precision", nullable: false),
                    number_of_rooms = table.Column<short>(type: "smallint", nullable: false),
                    is_furnished = table.Column<bool>(type: "boolean", nullable: false),
                    is_animal_friendly = table.Column<bool>(type: "boolean", nullable: false),
                    has_elevator = table.Column<bool>(type: "boolean", nullable: false),
                    has_balcony = table.Column<bool>(type: "boolean", nullable: false),
                    has_parking_space = table.Column<bool>(type: "boolean", nullable: false),
                    location_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_apartments", x => x.id);
                    table.ForeignKey(
                        name: "FK_apartments_locations_location_id",
                        column: x => x.location_id,
                        principalSchema: "rentful",
                        principalTable: "locations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "announcements",
                schema: "rentful",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    date_added = table.Column<string>(type: "text", nullable: false),
                    apartment_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_announcements", x => x.id);
                    table.ForeignKey(
                        name: "FK_announcements_apartments_apartment_id",
                        column: x => x.apartment_id,
                        principalSchema: "rentful",
                        principalTable: "apartments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_announcements_apartment_id",
                schema: "rentful",
                table: "announcements",
                column: "apartment_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_apartments_location_id",
                schema: "rentful",
                table: "apartments",
                column: "location_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_users_address_address_id",
                schema: "rentful",
                table: "users",
                column: "address_id",
                principalSchema: "rentful",
                principalTable: "address",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_address_address_id",
                schema: "rentful",
                table: "users");

            migrationBuilder.DropTable(
                name: "announcements",
                schema: "rentful");

            migrationBuilder.DropTable(
                name: "apartments",
                schema: "rentful");

            migrationBuilder.DropTable(
                name: "locations",
                schema: "rentful");

            migrationBuilder.DropPrimaryKey(
                name: "PK_address",
                schema: "rentful",
                table: "address");

            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.RenameTable(
                name: "users",
                schema: "rentful",
                newName: "users",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "address",
                schema: "rentful",
                newName: "Addresses");

            migrationBuilder.RenameColumn(
                name: "address_id",
                schema: "public",
                table: "users",
                newName: "addressId");

            migrationBuilder.RenameIndex(
                name: "IX_users_address_id",
                schema: "public",
                table: "users",
                newName: "IX_users_addressId");

            migrationBuilder.AlterColumn<int>(
                name: "addressId",
                schema: "public",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_users_Addresses_addressId",
                schema: "public",
                table: "users",
                column: "addressId",
                principalTable: "Addresses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
