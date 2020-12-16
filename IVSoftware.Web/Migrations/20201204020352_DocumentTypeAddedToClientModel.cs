using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class DocumentTypeAddedToClientModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentType",
                table: "ClientModel");

            migrationBuilder.AddColumn<int>(
                name: "DocumentTypeId",
                table: "ClientModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientModel_DocumentTypeId",
                table: "ClientModel",
                column: "DocumentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientModel_TipoDocumento_DocumentTypeId",
                table: "ClientModel",
                column: "DocumentTypeId",
                principalTable: "TipoDocumento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientModel_TipoDocumento_DocumentTypeId",
                table: "ClientModel");

            migrationBuilder.DropIndex(
                name: "IX_ClientModel_DocumentTypeId",
                table: "ClientModel");

            migrationBuilder.DropColumn(
                name: "DocumentTypeId",
                table: "ClientModel");

            migrationBuilder.AddColumn<int>(
                name: "DocumentType",
                table: "ClientModel",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
