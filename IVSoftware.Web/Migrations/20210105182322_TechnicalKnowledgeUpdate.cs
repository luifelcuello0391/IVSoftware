using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class TechnicalKnowledgeUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Matrix",
                table: "TechnicalKnowledge");

            migrationBuilder.DropColumn(
                name: "Service",
                table: "TechnicalKnowledge");

            migrationBuilder.AddColumn<int>(
                name: "MatrixId",
                table: "TechnicalKnowledge",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "TechnicalKnowledge",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalKnowledge_MatrixId",
                table: "TechnicalKnowledge",
                column: "MatrixId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalKnowledge_ServiceId",
                table: "TechnicalKnowledge",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicalKnowledge_MatrixModel_MatrixId",
                table: "TechnicalKnowledge",
                column: "MatrixId",
                principalTable: "MatrixModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicalKnowledge_ServiceModel_ServiceId",
                table: "TechnicalKnowledge",
                column: "ServiceId",
                principalTable: "ServiceModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_Provider_EquipProviderId",
                table: "Equipment");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationRequest_ClientModel_ClientId",
                table: "QuotationRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationRequest_Person_GenerationUsedId",
                table: "QuotationRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationRequest_QuotationStatus_StatusId",
                table: "QuotationRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnicalKnowledge_MatrixModel_MatrixId",
                table: "TechnicalKnowledge");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnicalKnowledge_ServiceModel_ServiceId",
                table: "TechnicalKnowledge");

            migrationBuilder.DropIndex(
                name: "IX_TechnicalKnowledge_MatrixId",
                table: "TechnicalKnowledge");

            migrationBuilder.DropIndex(
                name: "IX_TechnicalKnowledge_ServiceId",
                table: "TechnicalKnowledge");

            migrationBuilder.DropColumn(
                name: "MatrixId",
                table: "TechnicalKnowledge");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "TechnicalKnowledge");

            migrationBuilder.AddColumn<string>(
                name: "Matrix",
                table: "TechnicalKnowledge",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Service",
                table: "TechnicalKnowledge",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Provider",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Provider",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GenerationUsedId",
                table: "Provider",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Provider",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TempId",
                table: "Provider",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Provider_TempId",
                table: "Provider",
                column: "TempId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_Provider_EquipProviderId",
                table: "Equipment",
                column: "EquipProviderId",
                principalTable: "Provider",
                principalColumn: "TempId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Provider_ClientModel_ClientId",
                table: "Provider",
                column: "ClientId",
                principalTable: "ClientModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Provider_Person_GenerationUsedId",
                table: "Provider",
                column: "GenerationUsedId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Provider_QuotationStatus_StatusId",
                table: "Provider",
                column: "StatusId",
                principalTable: "QuotationStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
