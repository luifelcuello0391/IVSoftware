using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.IO;
using System.Text;

namespace IVSoftware.Web.Migrations
{
    public partial class QuotationCancelationResponsefieldAndQuotationAcceptedStatusAddedByscript : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CancelationResponse",
                table: "QuotationRequest",
                type: "nvarchar(max)",
                nullable: true);

            var sqlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"InitialScripts\ConfirmedQuotationStatus.sql");
            migrationBuilder.Sql(File.ReadAllText(sqlFile, Encoding.GetEncoding(28591)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CancelationResponse",
                table: "QuotationRequest");
        }
    }
}
