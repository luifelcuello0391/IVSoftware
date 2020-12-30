using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class ServiceGroupRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceGroupModel_ServiceModel_ServiceModelId",
                table: "ServiceGroupModel");

            migrationBuilder.DropIndex(
                name: "IX_ServiceGroupModel_ServiceModelId",
                table: "ServiceGroupModel");

            migrationBuilder.DropColumn(
                name: "ServiceModelId",
                table: "ServiceGroupModel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceModelId",
                table: "ServiceGroupModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceGroupModel_ServiceModelId",
                table: "ServiceGroupModel",
                column: "ServiceModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceGroupModel_ServiceModel_ServiceModelId",
                table: "ServiceGroupModel",
                column: "ServiceModelId",
                principalTable: "ServiceModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
