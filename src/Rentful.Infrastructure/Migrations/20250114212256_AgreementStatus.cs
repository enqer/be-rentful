using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rentful.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgreementStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_accepted",
                schema: "rentful",
                table: "lease_agreements");

            migrationBuilder.AddColumn<int>(
                name: "status",
                schema: "rentful",
                table: "lease_agreements",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                schema: "rentful",
                table: "lease_agreements");

            migrationBuilder.AddColumn<bool>(
                name: "is_accepted",
                schema: "rentful",
                table: "lease_agreements",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
