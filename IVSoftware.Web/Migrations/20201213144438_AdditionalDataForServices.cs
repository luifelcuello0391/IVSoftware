using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class AdditionalDataForServices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AcredditedByIdeam",
                table: "ServiceModel",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AuthorizedByINS",
                table: "ServiceModel",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "BillingCode",
                table: "ServiceModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingName",
                table: "ServiceModel",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReportDeliveryTime",
                table: "ServiceModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Valid",
                table: "ServiceModel",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CheckListResponseDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeaderId = table.Column<int>(type: "int", nullable: true),
                    QuestionId = table.Column<int>(type: "int", nullable: true),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisterStatus = table.Column<int>(type: "int", nullable: false),
                    CreationDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDatetime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckListResponseDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckListResponseDetail_CheckListQuestion_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "CheckListQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CheckListResponseDetail_ChecklistResponseHeader_HeaderId",
                        column: x => x.HeaderId,
                        principalTable: "ChecklistResponseHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckListResponseDetail_HeaderId",
                table: "CheckListResponseDetail",
                column: "HeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckListResponseDetail_QuestionId",
                table: "CheckListResponseDetail",
                column: "QuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckListResponseDetail");

            migrationBuilder.DropColumn(
                name: "AcredditedByIdeam",
                table: "ServiceModel");

            migrationBuilder.DropColumn(
                name: "AuthorizedByINS",
                table: "ServiceModel");

            migrationBuilder.DropColumn(
                name: "BillingCode",
                table: "ServiceModel");

            migrationBuilder.DropColumn(
                name: "BillingName",
                table: "ServiceModel");

            migrationBuilder.DropColumn(
                name: "ReportDeliveryTime",
                table: "ServiceModel");

            migrationBuilder.DropColumn(
                name: "Valid",
                table: "ServiceModel");
        }
    }
}
