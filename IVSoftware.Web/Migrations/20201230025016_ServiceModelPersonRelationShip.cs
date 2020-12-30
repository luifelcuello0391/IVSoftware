using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class ServiceModelPersonRelationShip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistResponseHeader_Persona_ValidatedById",
                table: "ChecklistResponseHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientContact_Departamento_AddressDepartmentId",
                table: "ClientContact");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientContact_Municipio_CityId",
                table: "ClientContact");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientContact_TipoDocumento_DocumentTypeId",
                table: "ClientContact");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientModel_Departamento_DepartmentId",
                table: "ClientModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientModel_Municipio_CityId",
                table: "ClientModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientModel_TipoDocumento_DocumentTypeId",
                table: "ClientModel");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationRequest_Persona_GenerationUsedId",
                table: "QuotationRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceModel_WorkingRangeModel_WorkingRangeId",
                table: "ServiceModel");

            migrationBuilder.DropIndex(
                name: "IX_ServiceModel_WorkingRangeId",
                table: "ServiceModel");

            migrationBuilder.DropColumn(
                name: "WorkingRangeId",
                table: "ServiceModel");

            migrationBuilder.AddColumn<bool>(
                name: "AvailableForClients",
                table: "ServiceModel",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<float>(
                name: "MaximumValue",
                table: "ServiceModel",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "MinimumValue",
                table: "ServiceModel",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<Guid>(
                name: "PersonId",
                table: "ServiceModel",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Precondition",
                table: "ServiceModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WeeklyAssignmentQuantity",
                table: "ServiceModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "AvailableForClients",
                table: "ServiceGroupModel",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<Guid>(
                name: "GenerationUsedId",
                table: "QuotationRequest",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ValidatedById",
                table: "ChecklistResponseHeader",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceModel_PersonId",
                table: "ServiceModel",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistResponseHeader_Person_ValidatedById",
                table: "ChecklistResponseHeader",
                column: "ValidatedById",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientContact_Department_AddressDepartmentId",
                table: "ClientContact",
                column: "AddressDepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientContact_IdentificationType_DocumentTypeId",
                table: "ClientContact",
                column: "DocumentTypeId",
                principalTable: "IdentificationType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientContact_Municipality_CityId",
                table: "ClientContact",
                column: "CityId",
                principalTable: "Municipality",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientModel_Department_DepartmentId",
                table: "ClientModel",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientModel_IdentificationType_DocumentTypeId",
                table: "ClientModel",
                column: "DocumentTypeId",
                principalTable: "IdentificationType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientModel_Municipality_CityId",
                table: "ClientModel",
                column: "CityId",
                principalTable: "Municipality",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationRequest_Person_GenerationUsedId",
                table: "QuotationRequest",
                column: "GenerationUsedId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceModel_Person_PersonId",
                table: "ServiceModel",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistResponseHeader_Person_ValidatedById",
                table: "ChecklistResponseHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientContact_Department_AddressDepartmentId",
                table: "ClientContact");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientContact_IdentificationType_DocumentTypeId",
                table: "ClientContact");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientContact_Municipality_CityId",
                table: "ClientContact");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientModel_Department_DepartmentId",
                table: "ClientModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientModel_IdentificationType_DocumentTypeId",
                table: "ClientModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientModel_Municipality_CityId",
                table: "ClientModel");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationRequest_Person_GenerationUsedId",
                table: "QuotationRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceModel_Person_PersonId",
                table: "ServiceModel");

            migrationBuilder.DropIndex(
                name: "IX_ServiceModel_PersonId",
                table: "ServiceModel");

            migrationBuilder.DropColumn(
                name: "AvailableForClients",
                table: "ServiceModel");

            migrationBuilder.DropColumn(
                name: "MaximumValue",
                table: "ServiceModel");

            migrationBuilder.DropColumn(
                name: "MinimumValue",
                table: "ServiceModel");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "ServiceModel");

            migrationBuilder.DropColumn(
                name: "Precondition",
                table: "ServiceModel");

            migrationBuilder.DropColumn(
                name: "WeeklyAssignmentQuantity",
                table: "ServiceModel");

            migrationBuilder.DropColumn(
                name: "AvailableForClients",
                table: "ServiceGroupModel");

            migrationBuilder.AddColumn<int>(
                name: "WorkingRangeId",
                table: "ServiceModel",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GenerationUsedId",
                table: "QuotationRequest",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ValidatedById",
                table: "ChecklistResponseHeader",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceModel_WorkingRangeId",
                table: "ServiceModel",
                column: "WorkingRangeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistResponseHeader_Persona_ValidatedById",
                table: "ChecklistResponseHeader",
                column: "ValidatedById",
                principalTable: "Persona",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientContact_Departamento_AddressDepartmentId",
                table: "ClientContact",
                column: "AddressDepartmentId",
                principalTable: "Departamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientContact_Municipio_CityId",
                table: "ClientContact",
                column: "CityId",
                principalTable: "Municipio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientContact_TipoDocumento_DocumentTypeId",
                table: "ClientContact",
                column: "DocumentTypeId",
                principalTable: "TipoDocumento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientModel_Departamento_DepartmentId",
                table: "ClientModel",
                column: "DepartmentId",
                principalTable: "Departamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientModel_Municipio_CityId",
                table: "ClientModel",
                column: "CityId",
                principalTable: "Municipio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientModel_TipoDocumento_DocumentTypeId",
                table: "ClientModel",
                column: "DocumentTypeId",
                principalTable: "TipoDocumento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationRequest_Persona_GenerationUsedId",
                table: "QuotationRequest",
                column: "GenerationUsedId",
                principalTable: "Persona",
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
    }
}
