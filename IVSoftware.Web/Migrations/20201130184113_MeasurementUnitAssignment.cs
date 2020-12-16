using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class MeasurementUnitAssignment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReferenceMethodModel_MeasurementUnitModel_MeasurementUnitId",
                table: "ReferenceMethodModel");

            migrationBuilder.DropIndex(
                name: "IX_ReferenceMethodModel_MeasurementUnitId",
                table: "ReferenceMethodModel");

            migrationBuilder.DropColumn(
                name: "MeasurementUnitId",
                table: "ReferenceMethodModel");

            migrationBuilder.AddColumn<int>(
                name: "MeasurementUnitId",
                table: "VariableModel",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VariableModel_MeasurementUnitId",
                table: "VariableModel",
                column: "MeasurementUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_VariableModel_MeasurementUnitModel_MeasurementUnitId",
                table: "VariableModel",
                column: "MeasurementUnitId",
                principalTable: "MeasurementUnitModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VariableModel_MeasurementUnitModel_MeasurementUnitId",
                table: "VariableModel");

            migrationBuilder.DropIndex(
                name: "IX_VariableModel_MeasurementUnitId",
                table: "VariableModel");

            migrationBuilder.DropColumn(
                name: "MeasurementUnitId",
                table: "VariableModel");

            migrationBuilder.AddColumn<int>(
                name: "MeasurementUnitId",
                table: "ReferenceMethodModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReferenceMethodModel_MeasurementUnitId",
                table: "ReferenceMethodModel",
                column: "MeasurementUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReferenceMethodModel_MeasurementUnitModel_MeasurementUnitId",
                table: "ReferenceMethodModel",
                column: "MeasurementUnitId",
                principalTable: "MeasurementUnitModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
