using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class QuotationContactRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContactId",
                table: "QuotationRequest",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuotationRequest_ContactId",
                table: "QuotationRequest",
                column: "ContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationRequest_ClientContact_ContactId",
                table: "QuotationRequest",
                column: "ContactId",
                principalTable: "ClientContact",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuotationRequest_ClientContact_ContactId",
                table: "QuotationRequest");

            migrationBuilder.DropIndex(
                name: "IX_QuotationRequest_ContactId",
                table: "QuotationRequest");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "QuotationRequest");
        }
    }
}
