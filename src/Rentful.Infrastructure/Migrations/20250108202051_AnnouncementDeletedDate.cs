using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rentful.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AnnouncementDeletedDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "date_deleted",
                schema: "rentful",
                table: "announcements",
                type: "timestamp without time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "date_deleted",
                schema: "rentful",
                table: "announcements");
        }
    }
}
