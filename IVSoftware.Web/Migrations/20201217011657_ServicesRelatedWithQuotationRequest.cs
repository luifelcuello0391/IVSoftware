using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class ServicesRelatedWithQuotationRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServicesIntoQuotation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(type: "int", nullable: true),
                    QuotationRequestId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicesIntoQuotation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicesIntoQuotation_QuotationRequest_QuotationRequestId",
                        column: x => x.QuotationRequestId,
                        principalTable: "QuotationRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServicesIntoQuotation_ServiceModel_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "ServiceModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServicesIntoQuotation_QuotationRequestId",
                table: "ServicesIntoQuotation",
                column: "QuotationRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicesIntoQuotation_ServiceId",
                table: "ServicesIntoQuotation",
                column: "ServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServicesIntoQuotation");
        }
    }
}
