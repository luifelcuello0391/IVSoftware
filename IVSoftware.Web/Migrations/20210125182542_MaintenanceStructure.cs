using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class MaintenanceStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Maintenances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeOfMaintenanceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EquipId = table.Column<int>(type: "int", nullable: false),
                    MaintenanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Diagnostic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkExecuted = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextCalibrationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderId = table.Column<int>(type: "int", nullable: true),
                    RegisterStatus = table.Column<int>(type: "int", nullable: false),
                    CreationDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDatetime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maintenances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Maintenances_Equipment_EquipId",
                        column: x => x.EquipId,
                        principalTable: "Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Maintenances_Provider_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Provider",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Maintenances_EquipId",
                table: "Maintenances",
                column: "EquipId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenances_ProviderId",
                table: "Maintenances",
                column: "ProviderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Maintenances");
        }
    }
}
