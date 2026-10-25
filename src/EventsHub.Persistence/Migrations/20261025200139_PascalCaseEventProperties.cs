using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsHub.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PascalCaseEventProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "longitude",
                table: "Events",
                newName: "Longitude");

            migrationBuilder.RenameColumn(
                name: "latitude",
                table: "Events",
                newName: "Latitude");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Events",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "Events",
                newName: "longitude");

            migrationBuilder.RenameColumn(
                name: "Latitude",
                table: "Events",
                newName: "latitude");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Events",
                newName: "id");
        }
    }
}
