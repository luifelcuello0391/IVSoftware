using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class CheckListHeader_Detail_relationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckListResponseDetail_ChecklistResponseHeader_HeaderId",
                table: "CheckListResponseDetail");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckListResponseDetail_ChecklistResponseHeader_HeaderId",
                table: "CheckListResponseDetail",
                column: "HeaderId",
                principalTable: "ChecklistResponseHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckListResponseDetail_ChecklistResponseHeader_HeaderId",
                table: "CheckListResponseDetail");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckListResponseDetail_ChecklistResponseHeader_HeaderId",
                table: "CheckListResponseDetail",
                column: "HeaderId",
                principalTable: "ChecklistResponseHeader",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
