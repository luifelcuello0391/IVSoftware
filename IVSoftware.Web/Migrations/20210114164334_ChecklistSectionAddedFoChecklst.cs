using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class ChecklistSectionAddedFoChecklst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SectionId",
                table: "CheckListResponseDetail",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CheckListResponseDetail_SectionId",
                table: "CheckListResponseDetail",
                column: "SectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckListResponseDetail_CheckListSection_SectionId",
                table: "CheckListResponseDetail",
                column: "SectionId",
                principalTable: "CheckListSection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckListResponseDetail_CheckListSection_SectionId",
                table: "CheckListResponseDetail");

            migrationBuilder.DropIndex(
                name: "IX_CheckListResponseDetail_SectionId",
                table: "CheckListResponseDetail");

            migrationBuilder.DropColumn(
                name: "SectionId",
                table: "CheckListResponseDetail");
        }
    }
}
