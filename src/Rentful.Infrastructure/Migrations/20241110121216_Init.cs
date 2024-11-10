using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Rentful.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "rentful");

            migrationBuilder.CreateTable(
                name: "address",
                schema: "rentful",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    postal_code = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    building_number = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    street = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    city = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    country = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_address", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "locations",
                schema: "rentful",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    latitude = table.Column<decimal>(type: "numeric", nullable: false),
                    longitude = table.Column<decimal>(type: "numeric", nullable: false),
                    province = table.Column<string>(type: "text", nullable: false),
                    city = table.Column<string>(type: "text", nullable: false),
                    is_precise = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "rentful",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    telephone_number = table.Column<string>(type: "text", nullable: true),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    address_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                    table.ForeignKey(
                        name: "FK_users_address_address_id",
                        column: x => x.address_id,
                        principalSchema: "rentful",
                        principalTable: "address",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "apartments",
                schema: "rentful",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    rent = table.Column<double>(type: "double precision", nullable: true),
                    deposit = table.Column<double>(type: "double precision", nullable: true),
                    area = table.Column<double>(type: "double precision", nullable: false),
                    number_of_rooms = table.Column<short>(type: "smallint", nullable: false),
                    is_furnished = table.Column<bool>(type: "boolean", nullable: false),
                    is_animal_friendly = table.Column<bool>(type: "boolean", nullable: false),
                    has_elevator = table.Column<bool>(type: "boolean", nullable: false),
                    has_balcony = table.Column<bool>(type: "boolean", nullable: false),
                    has_parking_space = table.Column<bool>(type: "boolean", nullable: false),
                    location_id = table.Column<int>(type: "integer", nullable: false)
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
                    date_added = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    apartment_id = table.Column<int>(type: "integer", nullable: true),
                    user_id = table.Column<int>(type: "integer", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_announcements_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "rentful",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_announcements_apartment_id",
                schema: "rentful",
                table: "announcements",
                column: "apartment_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_announcements_user_id",
                schema: "rentful",
                table: "announcements",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_apartments_location_id",
                schema: "rentful",
                table: "apartments",
                column: "location_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_images_apartment_id",
                schema: "rentful",
                table: "images",
                column: "apartment_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_address_id",
                schema: "rentful",
                table: "users",
                column: "address_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "announcements",
                schema: "rentful");

            migrationBuilder.DropTable(
                name: "images",
                schema: "rentful");

            migrationBuilder.DropTable(
                name: "users",
                schema: "rentful");

            migrationBuilder.DropTable(
                name: "apartments",
                schema: "rentful");

            migrationBuilder.DropTable(
                name: "address",
                schema: "rentful");

            migrationBuilder.DropTable(
                name: "locations",
                schema: "rentful");
        }
    }
}
