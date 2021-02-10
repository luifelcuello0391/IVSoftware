using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class NewServiceDataIncluded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnalisysTypeId",
                table: "ServiceModel",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "AnalizeBefore",
                table: "ServiceModel",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<Guid>(
                name: "BackupAnalystId",
                table: "ServiceModel",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BackupPersonId",
                table: "ServiceModel",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaximumWeeklyAssignment",
                table: "ServiceModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PreservativesTheirConcentrationQuantity",
                table: "ServiceModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceptionDays",
                table: "ServiceModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "RegulatoryStorageTime",
                table: "ServiceModel",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "RelatedProtocol",
                table: "ServiceModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SamplingCaptureDescription",
                table: "ServiceModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StorageContainerId",
                table: "ServiceModel",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "StorageTemperature",
                table: "ServiceModel",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TestItemRetentionTimeInDays",
                table: "ServiceModel",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "TypeOfSamplingId",
                table: "ServiceModel",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "VolumeEstablishedByLaboratory",
                table: "ServiceModel",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "WashingCleaningConditionsForContainer",
                table: "ServiceModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AnalisysType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisterStatus = table.Column<int>(type: "int", nullable: false),
                    CreationDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDatetime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalisysType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SamplingType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisterStatus = table.Column<int>(type: "int", nullable: false),
                    CreationDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDatetime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SamplingType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StorageContainer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisterStatus = table.Column<int>(type: "int", nullable: false),
                    CreationDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDatetime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageContainer", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceModel_AnalisysTypeId",
                table: "ServiceModel",
                column: "AnalisysTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceModel_BackupAnalystId",
                table: "ServiceModel",
                column: "BackupAnalystId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceModel_StorageContainerId",
                table: "ServiceModel",
                column: "StorageContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceModel_TypeOfSamplingId",
                table: "ServiceModel",
                column: "TypeOfSamplingId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceModel_AnalisysType_AnalisysTypeId",
                table: "ServiceModel",
                column: "AnalisysTypeId",
                principalTable: "AnalisysType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceModel_Person_BackupAnalystId",
                table: "ServiceModel",
                column: "BackupAnalystId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceModel_SamplingType_TypeOfSamplingId",
                table: "ServiceModel",
                column: "TypeOfSamplingId",
                principalTable: "SamplingType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceModel_StorageContainer_StorageContainerId",
                table: "ServiceModel",
                column: "StorageContainerId",
                principalTable: "StorageContainer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceModel_AnalisysType_AnalisysTypeId",
                table: "ServiceModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceModel_Person_BackupAnalystId",
                table: "ServiceModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceModel_SamplingType_TypeOfSamplingId",
                table: "ServiceModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceModel_StorageContainer_StorageContainerId",
                table: "ServiceModel");

            migrationBuilder.DropTable(
                name: "AnalisysType");

            migrationBuilder.DropTable(
                name: "SamplingType");

            migrationBuilder.DropTable(
                name: "StorageContainer");

            migrationBuilder.DropIndex(
                name: "IX_ServiceModel_AnalisysTypeId",
                table: "ServiceModel");

            migrationBuilder.DropIndex(
                name: "IX_ServiceModel_BackupAnalystId",
                table: "ServiceModel");

            migrationBuilder.DropIndex(
                name: "IX_ServiceModel_StorageContainerId",
                table: "ServiceModel");

            migrationBuilder.DropIndex(
                name: "IX_ServiceModel_TypeOfSamplingId",
                table: "ServiceModel");

            migrationBuilder.DropColumn(
                name: "AnalisysTypeId",
                table: "ServiceModel");

            migrationBuilder.DropColumn(
                name: "AnalizeBefore",
                table: "ServiceModel");

            migrationBuilder.DropColumn(
                name: "BackupAnalystId",
                table: "ServiceModel");

            migrationBuilder.DropColumn(
                name: "BackupPersonId",
                table: "ServiceModel");

            migrationBuilder.DropColumn(
                name: "MaximumWeeklyAssignment",
                table: "ServiceModel");

            migrationBuilder.DropColumn(
                name: "PreservativesTheirConcentrationQuantity",
                table: "ServiceModel");

            migrationBuilder.DropColumn(
                name: "ReceptionDays",
                table: "ServiceModel");

            migrationBuilder.DropColumn(
                name: "RegulatoryStorageTime",
                table: "ServiceModel");

            migrationBuilder.DropColumn(
                name: "RelatedProtocol",
                table: "ServiceModel");

            migrationBuilder.DropColumn(
                name: "SamplingCaptureDescription",
                table: "ServiceModel");

            migrationBuilder.DropColumn(
                name: "StorageContainerId",
                table: "ServiceModel");

            migrationBuilder.DropColumn(
                name: "StorageTemperature",
                table: "ServiceModel");

            migrationBuilder.DropColumn(
                name: "TestItemRetentionTimeInDays",
                table: "ServiceModel");

            migrationBuilder.DropColumn(
                name: "TypeOfSamplingId",
                table: "ServiceModel");

            migrationBuilder.DropColumn(
                name: "VolumeEstablishedByLaboratory",
                table: "ServiceModel");

            migrationBuilder.DropColumn(
                name: "WashingCleaningConditionsForContainer",
                table: "ServiceModel");
        }
    }
}
