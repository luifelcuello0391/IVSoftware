using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.IO;
using System.Text;

namespace IVSoftware.Web.Migrations
{
    public partial class ClientContactRestrictionOnQuotationAddedwithscript : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuotationRequest_ClientContact_ContactId",
                table: "QuotationRequest");

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationRequest_ClientContact_ContactId",
                table: "QuotationRequest",
                column: "ContactId",
                principalTable: "ClientContact",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            var sqlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"InitialScripts\quotation_clientcontact_relationship_deleted.sql");
            migrationBuilder.Sql(File.ReadAllText(sqlFile, Encoding.GetEncoding(28591)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuotationRequest_ClientContact_ContactId",
                table: "QuotationRequest");

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationRequest_ClientContact_ContactId",
                table: "QuotationRequest",
                column: "ContactId",
                principalTable: "ClientContact",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
