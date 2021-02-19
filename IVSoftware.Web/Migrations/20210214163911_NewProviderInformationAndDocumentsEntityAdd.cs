using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class NewProviderInformationAndDocumentsEntityAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "AirResourceMonitoringDevideMaintenances");

            migrationBuilder.AddColumn<string>(
                name: "Rut",
                table: "Provider",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RutDocuentId",
                table: "Provider",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RutDocumentId",
                table: "Provider",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebPage",
                table: "Provider",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MaintenanceCertificateDocumentId",
                table: "Maintenances",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LaboratoryId",
                table: "EquipmentPurchaseRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MaintenanceCertificateDocumentId",
                table: "AirResourceMonitoringDevideMaintenances",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModiicationDatetime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Provider_RutDocumentId",
                table: "Provider",
                column: "RutDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenances_MaintenanceCertificateDocumentId",
                table: "Maintenances",
                column: "MaintenanceCertificateDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentPurchaseRequests_LaboratoryId",
                table: "EquipmentPurchaseRequests",
                column: "LaboratoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AirResourceMonitoringDevideMaintenances_MaintenanceCertificateDocumentId",
                table: "AirResourceMonitoringDevideMaintenances",
                column: "MaintenanceCertificateDocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AirResourceMonitoringDevideMaintenances_Document_MaintenanceCertificateDocumentId",
                table: "AirResourceMonitoringDevideMaintenances",
                column: "MaintenanceCertificateDocumentId",
                principalTable: "Document",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentPurchaseRequests_Laboratories_LaboratoryId",
                table: "EquipmentPurchaseRequests",
                column: "LaboratoryId",
                principalTable: "Laboratories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_Document_MaintenanceCertificateDocumentId",
                table: "Maintenances",
                column: "MaintenanceCertificateDocumentId",
                principalTable: "Document",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Provider_Document_RutDocumentId",
                table: "Provider",
                column: "RutDocumentId",
                principalTable: "Document",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AirResourceMonitoringDevideMaintenances_Document_MaintenanceCertificateDocumentId",
                table: "AirResourceMonitoringDevideMaintenances");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentPurchaseRequests_Laboratories_LaboratoryId",
                table: "EquipmentPurchaseRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_Document_MaintenanceCertificateDocumentId",
                table: "Maintenances");

            migrationBuilder.DropForeignKey(
                name: "FK_Provider_Document_RutDocumentId",
                table: "Provider");

            migrationBuilder.DropTable(
                name: "Document");

            migrationBuilder.DropIndex(
                name: "IX_Provider_RutDocumentId",
                table: "Provider");

            migrationBuilder.DropIndex(
                name: "IX_Maintenances_MaintenanceCertificateDocumentId",
                table: "Maintenances");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentPurchaseRequests_LaboratoryId",
                table: "EquipmentPurchaseRequests");

            migrationBuilder.DropIndex(
                name: "IX_AirResourceMonitoringDevideMaintenances_MaintenanceCertificateDocumentId",
                table: "AirResourceMonitoringDevideMaintenances");

            migrationBuilder.DropColumn(
                name: "Rut",
                table: "Provider");

            migrationBuilder.DropColumn(
                name: "RutDocuentId",
                table: "Provider");

            migrationBuilder.DropColumn(
                name: "RutDocumentId",
                table: "Provider");

            migrationBuilder.DropColumn(
                name: "WebPage",
                table: "Provider");

            migrationBuilder.DropColumn(
                name: "MaintenanceCertificateDocumentId",
                table: "Maintenances");

            migrationBuilder.DropColumn(
                name: "LaboratoryId",
                table: "EquipmentPurchaseRequests");

            migrationBuilder.DropColumn(
                name: "MaintenanceCertificateDocumentId",
                table: "AirResourceMonitoringDevideMaintenances");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "AirResourceMonitoringDevideMaintenances",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
