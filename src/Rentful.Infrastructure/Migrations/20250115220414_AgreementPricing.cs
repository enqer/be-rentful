using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rentful.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgreementPricing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "deposit",
                schema: "rentful",
                table: "lease_agreements",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "price",
                schema: "rentful",
                table: "lease_agreements",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "rent",
                schema: "rentful",
                table: "lease_agreements",
                type: "double precision",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deposit",
                schema: "rentful",
                table: "lease_agreements");

            migrationBuilder.DropColumn(
                name: "price",
                schema: "rentful",
                table: "lease_agreements");

            migrationBuilder.DropColumn(
                name: "rent",
                schema: "rentful",
                table: "lease_agreements");
        }
    }
}
