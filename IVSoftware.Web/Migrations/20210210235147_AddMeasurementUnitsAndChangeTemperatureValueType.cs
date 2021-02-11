using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class AddMeasurementUnitsAndChangeTemperatureValueType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StorageTemperature",
                table: "ServiceModel",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<int>(
                name: "MeasurementUnitId",
                table: "ServiceModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceModel_MeasurementUnitId",
                table: "ServiceModel",
                column: "MeasurementUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceModel_MeasurementUnitModel_MeasurementUnitId",
                table: "ServiceModel",
                column: "MeasurementUnitId",
                principalTable: "MeasurementUnitModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceModel_MeasurementUnitModel_MeasurementUnitId",
                table: "ServiceModel");

            migrationBuilder.DropIndex(
                name: "IX_ServiceModel_MeasurementUnitId",
                table: "ServiceModel");

            migrationBuilder.DropColumn(
                name: "MeasurementUnitId",
                table: "ServiceModel");

            migrationBuilder.AlterColumn<double>(
                name: "StorageTemperature",
                table: "ServiceModel",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
