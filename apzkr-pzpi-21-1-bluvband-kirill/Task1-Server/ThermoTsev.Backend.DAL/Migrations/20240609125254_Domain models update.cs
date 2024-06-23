using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThermoTsev.Backend.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Domainmodelsupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxTemperature",
                table: "ShipmentConditions");

            migrationBuilder.RenameColumn(
                name: "MinTemperature",
                table: "ShipmentConditions",
                newName: "MinAllowedTemperature");

            migrationBuilder.RenameColumn(
                name: "MinHumidity",
                table: "ShipmentConditions",
                newName: "MaxAllowedTemperature");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MinAllowedTemperature",
                table: "ShipmentConditions",
                newName: "MinTemperature");

            migrationBuilder.RenameColumn(
                name: "MaxAllowedTemperature",
                table: "ShipmentConditions",
                newName: "MinHumidity");

            migrationBuilder.AddColumn<float>(
                name: "MaxTemperature",
                table: "ShipmentConditions",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
