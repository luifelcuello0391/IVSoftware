using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class EquipmentPurchaseRequestData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EquipmentPurchaseRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EquipmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Parameters = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Justification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnalystPersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WorkRange = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Voltage = table.Column<double>(type: "float", nullable: false),
                    Accuracy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinimumRead = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NetworkCompatibility = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Frequency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnvironmentConditions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Accessories = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequiresLearning = table.Column<bool>(type: "bit", nullable: false),
                    HourlyIntensity = table.Column<int>(type: "int", nullable: false),
                    Warranty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestStatus = table.Column<int>(type: "int", nullable: false),
                    RequestStatusDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ResponsePersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisterStatus = table.Column<int>(type: "int", nullable: false),
                    CreationDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDatetime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentPurchaseRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentPurchaseRequests_Person_AnalystPersonId",
                        column: x => x.AnalystPersonId,
                        principalTable: "Person",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EquipmentPurchaseRequests_Person_ResponsePersonId",
                        column: x => x.ResponsePersonId,
                        principalTable: "Person",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentPurchaseRequests_AnalystPersonId",
                table: "EquipmentPurchaseRequests",
                column: "AnalystPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentPurchaseRequests_ResponsePersonId",
                table: "EquipmentPurchaseRequests",
                column: "ResponsePersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipmentPurchaseRequests");            
        }
    }
}
