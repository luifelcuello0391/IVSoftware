using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class ClientInformation1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CellPhone",
                table: "ClientModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "ClientModel",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "ClientModel",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "ClientModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonType",
                table: "ClientModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ClientContact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentTypeId = table.Column<int>(type: "int", nullable: true),
                    Identification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressDepartmentId = table.Column<int>(type: "int", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CellPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportDeliveryEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientModelId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisterStatus = table.Column<int>(type: "int", nullable: false),
                    CreationDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDatetime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientContact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientContact_ClientModel_ClientModelId",
                        column: x => x.ClientModelId,
                        principalTable: "ClientModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientContact_Departamento_AddressDepartmentId",
                        column: x => x.AddressDepartmentId,
                        principalTable: "Departamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientContact_Municipio_CityId",
                        column: x => x.CityId,
                        principalTable: "Municipio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientContact_TipoDocumento_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "TipoDocumento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientModel_CityId",
                table: "ClientModel",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientModel_DepartmentId",
                table: "ClientModel",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientContact_AddressDepartmentId",
                table: "ClientContact",
                column: "AddressDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientContact_CityId",
                table: "ClientContact",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientContact_ClientModelId",
                table: "ClientContact",
                column: "ClientModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientContact_DocumentTypeId",
                table: "ClientContact",
                column: "DocumentTypeId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientModel_Departamento_DepartmentId",
                table: "ClientModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientModel_Municipio_CityId",
                table: "ClientModel");

            migrationBuilder.DropTable(
                name: "ClientContact");

            migrationBuilder.DropIndex(
                name: "IX_ClientModel_CityId",
                table: "ClientModel");

            migrationBuilder.DropIndex(
                name: "IX_ClientModel_DepartmentId",
                table: "ClientModel");

            migrationBuilder.DropColumn(
                name: "CellPhone",
                table: "ClientModel");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "ClientModel");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "ClientModel");

            migrationBuilder.DropColumn(
                name: "Extension",
                table: "ClientModel");

            migrationBuilder.DropColumn(
                name: "PersonType",
                table: "ClientModel");
        }
    }
}
