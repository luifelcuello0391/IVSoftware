using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class QuotationStatusAddedQuotationRequestFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientName",
                table: "QuotationRequest");

            migrationBuilder.DropColumn(
                name: "ClientRegisterId",
                table: "QuotationRequest");

            migrationBuilder.DropColumn(
                name: "UserIdThatGenerated",
                table: "QuotationRequest");

            migrationBuilder.DropColumn(
                name: "UserNameThatGenerated",
                table: "QuotationRequest");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "QuotationRequest",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GenerationUsedId",
                table: "QuotationRequest",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RequestDateTime",
                table: "QuotationRequest",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "QuotationRequest",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "QuotationStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisterStatus = table.Column<int>(type: "int", nullable: false),
                    CreationDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDatetime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuotationRequest_ClientId",
                table: "QuotationRequest",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationRequest_GenerationUsedId",
                table: "QuotationRequest",
                column: "GenerationUsedId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationRequest_StatusId",
                table: "QuotationRequest",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationRequest_AspNetUsers_GenerationUsedId",
                table: "QuotationRequest",
                column: "GenerationUsedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationRequest_ClientModel_ClientId",
                table: "QuotationRequest",
                column: "ClientId",
                principalTable: "ClientModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationRequest_QuotationStatus_StatusId",
                table: "QuotationRequest",
                column: "StatusId",
                principalTable: "QuotationStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuotationRequest_AspNetUsers_GenerationUsedId",
                table: "QuotationRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationRequest_ClientModel_ClientId",
                table: "QuotationRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationRequest_QuotationStatus_StatusId",
                table: "QuotationRequest");

            migrationBuilder.DropTable(
                name: "QuotationStatus");

            migrationBuilder.DropIndex(
                name: "IX_QuotationRequest_ClientId",
                table: "QuotationRequest");

            migrationBuilder.DropIndex(
                name: "IX_QuotationRequest_GenerationUsedId",
                table: "QuotationRequest");

            migrationBuilder.DropIndex(
                name: "IX_QuotationRequest_StatusId",
                table: "QuotationRequest");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "QuotationRequest");

            migrationBuilder.DropColumn(
                name: "GenerationUsedId",
                table: "QuotationRequest");

            migrationBuilder.DropColumn(
                name: "RequestDateTime",
                table: "QuotationRequest");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "QuotationRequest");

            migrationBuilder.AddColumn<string>(
                name: "ClientName",
                table: "QuotationRequest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClientRegisterId",
                table: "QuotationRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserIdThatGenerated",
                table: "QuotationRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserNameThatGenerated",
                table: "QuotationRequest",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
