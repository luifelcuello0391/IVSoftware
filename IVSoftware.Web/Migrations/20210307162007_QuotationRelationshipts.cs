using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class QuotationRelationshipts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "QuotationTotalValue",
                table: "QuotationRequest",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "ServicesReportTime",
                table: "QuotationRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "ServicesTotal",
                table: "QuotationRequest",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateTable(
                name: "IncentivesIntoServiceQuotationRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuotationRequestId = table.Column<int>(type: "int", nullable: true),
                    IncentiveId = table.Column<int>(type: "int", nullable: true),
                    IncentiveCurrentValue = table.Column<float>(type: "real", nullable: false),
                    IsPercentage = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncentivesIntoServiceQuotationRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IncentivesIntoServiceQuotationRequest_IncentiveModel_IncentiveId",
                        column: x => x.IncentiveId,
                        principalTable: "IncentiveModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IncentivesIntoServiceQuotationRequest_QuotationRequest_QuotationRequestId",
                        column: x => x.QuotationRequestId,
                        principalTable: "QuotationRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IncentivesIntoServiceQuotationRequest_IncentiveId",
                table: "IncentivesIntoServiceQuotationRequest",
                column: "IncentiveId");

            migrationBuilder.CreateIndex(
                name: "IX_IncentivesIntoServiceQuotationRequest_QuotationRequestId",
                table: "IncentivesIntoServiceQuotationRequest",
                column: "QuotationRequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IncentivesIntoServiceQuotationRequest");

            migrationBuilder.DropColumn(
                name: "QuotationTotalValue",
                table: "QuotationRequest");

            migrationBuilder.DropColumn(
                name: "ServicesReportTime",
                table: "QuotationRequest");

            migrationBuilder.DropColumn(
                name: "ServicesTotal",
                table: "QuotationRequest");
        }
    }
}
