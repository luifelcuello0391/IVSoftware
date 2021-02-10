using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class NewServiceDataIncludedWihRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceModel_Person_BackupAnalystId",
                table: "ServiceModel");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceModel_Person_BackupAnalystId",
                table: "ServiceModel",
                column: "BackupAnalystId",
                principalTable: "Person",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceModel_Person_BackupAnalystId",
                table: "ServiceModel");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceModel_Person_BackupAnalystId",
                table: "ServiceModel",
                column: "BackupAnalystId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
