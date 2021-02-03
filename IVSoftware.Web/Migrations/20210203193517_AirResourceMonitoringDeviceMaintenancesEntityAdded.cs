using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class AirResourceMonitoringDeviceMaintenancesEntityAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PurchaseValue",
                table: "Equipment",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "AirResourceMonitoringDevideMaintenances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeOfMaintenanceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EquipId = table.Column<int>(type: "int", nullable: false),
                    MaintenanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProviderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderIdentificaton = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderContactName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SparePartsChanged = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextMaintenanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StockNumber = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisterStatus = table.Column<int>(type: "int", nullable: false),
                    CreationDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDatetime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirResourceMonitoringDevideMaintenances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AirResourceMonitoringDevideMaintenances_Equipment_EquipId",
                        column: x => x.EquipId,
                        principalTable: "Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AirResourceMonitoringDevideMaintenances_EquipId",
                table: "AirResourceMonitoringDevideMaintenances",
                column: "EquipId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirResourceMonitoringDevideMaintenances");

            migrationBuilder.DropColumn(
                name: "PurchaseValue",
                table: "Equipment");
        }
    }
}
