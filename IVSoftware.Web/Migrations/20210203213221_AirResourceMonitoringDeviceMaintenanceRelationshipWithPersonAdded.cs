using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class AirResourceMonitoringDeviceMaintenanceRelationshipWithPersonAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PersonId",
                table: "AirResourceMonitoringDevideMaintenances",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AirResourceMonitoringDevideMaintenances_PersonId",
                table: "AirResourceMonitoringDevideMaintenances",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_AirResourceMonitoringDevideMaintenances_Person_PersonId",
                table: "AirResourceMonitoringDevideMaintenances",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AirResourceMonitoringDevideMaintenances_Person_PersonId",
                table: "AirResourceMonitoringDevideMaintenances");

            migrationBuilder.DropIndex(
                name: "IX_AirResourceMonitoringDevideMaintenances_PersonId",
                table: "AirResourceMonitoringDevideMaintenances");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "AirResourceMonitoringDevideMaintenances");
        }
    }
}
