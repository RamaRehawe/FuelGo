using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FuelGo.Migrations
{
    /// <inheritdoc />
    public partial class updatenaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Location_Description",
                table: "GasStations",
                newName: "LocationDescription");

            migrationBuilder.RenameColumn(
                name: "Location_Description",
                table: "CustomerApartments",
                newName: "LocationDescription");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LocationDescription",
                table: "GasStations",
                newName: "Location_Description");

            migrationBuilder.RenameColumn(
                name: "LocationDescription",
                table: "CustomerApartments",
                newName: "Location_Description");
        }
    }
}
