using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rentful.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TenantRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TenantId",
                schema: "rentful",
                table: "lease_agreements",
                newName: "tenant_id");

            migrationBuilder.RenameIndex(
                name: "IX_lease_agreements_TenantId",
                schema: "rentful",
                table: "lease_agreements",
                newName: "IX_lease_agreements_tenant_id");

            migrationBuilder.RenameColumn(
                name: "ApartmentId",
                schema: "rentful",
                table: "lease_agreements",
                newName: "apartment_id");

            migrationBuilder.RenameIndex(
                name: "IX_lease_agreements_ApartmentId",
                schema: "rentful",
                table: "lease_agreements",
                newName: "IX_lease_agreements_apartment_id");

            migrationBuilder.AddColumn<short>(
                name: "rating_tenant",
                schema: "rentful",
                table: "lease_agreements",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddForeignKey(
                name: "FK_lease_agreements_apartments_apartment_id",
                schema: "rentful",
                table: "lease_agreements",
                column: "apartment_id",
                principalSchema: "rentful",
                principalTable: "apartments",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_lease_agreements_users_tenant_id",
                schema: "rentful",
                table: "lease_agreements",
                column: "tenant_id",
                principalSchema: "rentful",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lease_agreements_apartments_apartment_id",
                schema: "rentful",
                table: "lease_agreements");

            migrationBuilder.DropForeignKey(
                name: "FK_lease_agreements_users_tenant_id",
                schema: "rentful",
                table: "lease_agreements");

            migrationBuilder.DropColumn(
                name: "rating_tenant",
                schema: "rentful",
                table: "lease_agreements");

            migrationBuilder.RenameColumn(
                name: "apartment_id",
                schema: "rentful",
                table: "lease_agreements",
                newName: "ApartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_lease_agreements_apartment_id",
                schema: "rentful",
                table: "lease_agreements",
                newName: "IX_lease_agreements_ApartmentId");

            migrationBuilder.RenameColumn(
                name: "tenant_id",
                schema: "rentful",
                table: "lease_agreements",
                newName: "TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_lease_agreements_tenant_id",
                schema: "rentful",
                table: "lease_agreements",
                newName: "IX_lease_agreements_TenantId");
        }
    }
}
