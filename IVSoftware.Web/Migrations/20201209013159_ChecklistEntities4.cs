using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class ChecklistEntities4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_EquipmentCheckList_CheckListId",
                table: "Equipment");

            migrationBuilder.DropIndex(
                name: "IX_Equipment_CheckListId",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "CheckListId",
                table: "Equipment");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CheckListId",
                table: "Equipment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_CheckListId",
                table: "Equipment",
                column: "CheckListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_EquipmentCheckList_CheckListId",
                table: "Equipment",
                column: "CheckListId",
                principalTable: "EquipmentCheckList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
