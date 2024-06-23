using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThermoTsev.Backend.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DBupdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Users_UserId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_ShipmentConditions_Shipments_ShipmentId",
                table: "ShipmentConditions");

            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_Locations_DestinationDeliveryLocationId",
                table: "Shipments");

            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_Locations_OriginatingDeliveryLocationId",
                table: "Shipments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShipmentConditions",
                table: "ShipmentConditions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locations",
                table: "Locations");

            migrationBuilder.RenameTable(
                name: "ShipmentConditions",
                newName: "ShipmentInfos");

            migrationBuilder.RenameTable(
                name: "Notifications",
                newName: "EmergencyNotifications");

            migrationBuilder.RenameTable(
                name: "Locations",
                newName: "DeliveryLocations");

            migrationBuilder.RenameIndex(
                name: "IX_ShipmentConditions_ShipmentId",
                table: "ShipmentInfos",
                newName: "IX_ShipmentInfos_ShipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Notifications_UserId",
                table: "EmergencyNotifications",
                newName: "IX_EmergencyNotifications_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShipmentInfos",
                table: "ShipmentInfos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmergencyNotifications",
                table: "EmergencyNotifications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeliveryLocations",
                table: "DeliveryLocations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmergencyNotifications_Users_UserId",
                table: "EmergencyNotifications",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShipmentInfos_Shipments_ShipmentId",
                table: "ShipmentInfos",
                column: "ShipmentId",
                principalTable: "Shipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_DeliveryLocations_DestinationDeliveryLocationId",
                table: "Shipments",
                column: "DestinationDeliveryLocationId",
                principalTable: "DeliveryLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_DeliveryLocations_OriginatingDeliveryLocationId",
                table: "Shipments",
                column: "OriginatingDeliveryLocationId",
                principalTable: "DeliveryLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmergencyNotifications_Users_UserId",
                table: "EmergencyNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_ShipmentInfos_Shipments_ShipmentId",
                table: "ShipmentInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_DeliveryLocations_DestinationDeliveryLocationId",
                table: "Shipments");

            migrationBuilder.DropForeignKey(
                name: "FK_Shipments_DeliveryLocations_OriginatingDeliveryLocationId",
                table: "Shipments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShipmentInfos",
                table: "ShipmentInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmergencyNotifications",
                table: "EmergencyNotifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeliveryLocations",
                table: "DeliveryLocations");

            migrationBuilder.RenameTable(
                name: "ShipmentInfos",
                newName: "ShipmentConditions");

            migrationBuilder.RenameTable(
                name: "EmergencyNotifications",
                newName: "Notifications");

            migrationBuilder.RenameTable(
                name: "DeliveryLocations",
                newName: "Locations");

            migrationBuilder.RenameIndex(
                name: "IX_ShipmentInfos_ShipmentId",
                table: "ShipmentConditions",
                newName: "IX_ShipmentConditions_ShipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_EmergencyNotifications_UserId",
                table: "Notifications",
                newName: "IX_Notifications_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShipmentConditions",
                table: "ShipmentConditions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notifications",
                table: "Notifications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locations",
                table: "Locations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_UserId",
                table: "Notifications",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShipmentConditions_Shipments_ShipmentId",
                table: "ShipmentConditions",
                column: "ShipmentId",
                principalTable: "Shipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_Locations_DestinationDeliveryLocationId",
                table: "Shipments",
                column: "DestinationDeliveryLocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shipments_Locations_OriginatingDeliveryLocationId",
                table: "Shipments",
                column: "OriginatingDeliveryLocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
