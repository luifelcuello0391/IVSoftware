using System;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class TaxModelAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "QuotationTotalValueAfterTaxes",
                table: "QuotationRequest",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateTable(
                name: "Taxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Currentvalue = table.Column<float>(type: "real", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisterStatus = table.Column<int>(type: "int", nullable: false),
                    CreationDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDatetime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxesIntoServiceQuotationRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxId = table.Column<int>(type: "int", nullable: true),
                    QuotationRequestId = table.Column<int>(type: "int", nullable: true),
                    CurrentTaxValue = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxesIntoServiceQuotationRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxesIntoServiceQuotationRequest_QuotationRequest_QuotationRequestId",
                        column: x => x.QuotationRequestId,
                        principalTable: "QuotationRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaxesIntoServiceQuotationRequest_Taxes_TaxId",
                        column: x => x.TaxId,
                        principalTable: "Taxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaxesIntoServiceQuotationRequest_QuotationRequestId",
                table: "TaxesIntoServiceQuotationRequest",
                column: "QuotationRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxesIntoServiceQuotationRequest_TaxId",
                table: "TaxesIntoServiceQuotationRequest",
                column: "TaxId");

            var sqlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"InitialScripts\TaxTableInitialization.sql");
            migrationBuilder.Sql(File.ReadAllText(sqlFile, Encoding.GetEncoding(28591))); 
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaxesIntoServiceQuotationRequest");

            migrationBuilder.DropTable(
                name: "Taxes");

            migrationBuilder.DropColumn(
                name: "QuotationTotalValueAfterTaxes",
                table: "QuotationRequest");
        }
    }
}
