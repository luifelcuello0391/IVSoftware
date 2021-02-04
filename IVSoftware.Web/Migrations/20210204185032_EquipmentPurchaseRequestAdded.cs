using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class EquipmentPurchaseRequestAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonEvaluations",
                table: "PersonEvaluations");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "PersonEvaluations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "PersonEvaluations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "PersonEvaluations",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "PersonEvaluations",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonEvaluations",
                table: "PersonEvaluations",
                column: "Id");

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

            migrationBuilder.CreateTable(
                name: "NotificationSetting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationSetting", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonEvaluations_PersonId",
                table: "PersonEvaluations",
                column: "PersonId");

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

            migrationBuilder.DropTable(
                name: "NotificationSetting");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonEvaluations",
                table: "PersonEvaluations");

            migrationBuilder.DropIndex(
                name: "IX_PersonEvaluations_PersonId",
                table: "PersonEvaluations");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PersonEvaluations");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "PersonEvaluations");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "PersonEvaluations");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "PersonEvaluations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonEvaluations",
                table: "PersonEvaluations",
                columns: new[] { "PersonId", "EvaluationId" });
        }
    }
}
