using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class VariableReferenceMethodWorkingRangeToServiceModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReferenceMethodId",
                table: "ServiceModel",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VariableId",
                table: "ServiceModel",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkingRangeId",
                table: "ServiceModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceModel_ReferenceMethodId",
                table: "ServiceModel",
                column: "ReferenceMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceModel_VariableId",
                table: "ServiceModel",
                column: "VariableId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceModel_WorkingRangeId",
                table: "ServiceModel",
                column: "WorkingRangeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceModel_ReferenceMethodModel_ReferenceMethodId",
                table: "ServiceModel",
                column: "ReferenceMethodId",
                principalTable: "ReferenceMethodModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceModel_VariableModel_VariableId",
                table: "ServiceModel",
                column: "VariableId",
                principalTable: "VariableModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceModel_WorkingRangeModel_WorkingRangeId",
                table: "ServiceModel",
                column: "WorkingRangeId",
                principalTable: "WorkingRangeModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceModel_ReferenceMethodModel_ReferenceMethodId",
                table: "ServiceModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceModel_VariableModel_VariableId",
                table: "ServiceModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceModel_WorkingRangeModel_WorkingRangeId",
                table: "ServiceModel");

            migrationBuilder.DropIndex(
                name: "IX_ServiceModel_ReferenceMethodId",
                table: "ServiceModel");

            migrationBuilder.DropIndex(
                name: "IX_ServiceModel_VariableId",
                table: "ServiceModel");

            migrationBuilder.DropIndex(
                name: "IX_ServiceModel_WorkingRangeId",
                table: "ServiceModel");

            migrationBuilder.DropColumn(
                name: "ReferenceMethodId",
                table: "ServiceModel");

            migrationBuilder.DropColumn(
                name: "VariableId",
                table: "ServiceModel");

            migrationBuilder.DropColumn(
                name: "WorkingRangeId",
                table: "ServiceModel");
        }
    }
}
