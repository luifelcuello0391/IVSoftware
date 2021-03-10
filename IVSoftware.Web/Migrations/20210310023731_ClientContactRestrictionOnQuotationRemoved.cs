using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class ClientContactRestrictionOnQuotationRemoved : Migration
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
                onDelete: ReferentialAction.Restrict);
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
                onDelete: ReferentialAction.SetNull);
        }
    }
}
