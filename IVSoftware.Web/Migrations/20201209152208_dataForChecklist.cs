using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class dataForChecklist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Observation",
                table: "ChecklistResponseHeader",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ValidatedById",
                table: "ChecklistResponseHeader",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ValidationResult",
                table: "ChecklistResponseHeader",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistResponseHeader_ValidatedById",
                table: "ChecklistResponseHeader",
                column: "ValidatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistResponseHeader_AspNetUsers_ValidatedById",
                table: "ChecklistResponseHeader",
                column: "ValidatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistResponseHeader_AspNetUsers_ValidatedById",
                table: "ChecklistResponseHeader");

            migrationBuilder.DropIndex(
                name: "IX_ChecklistResponseHeader_ValidatedById",
                table: "ChecklistResponseHeader");

            migrationBuilder.DropColumn(
                name: "Observation",
                table: "ChecklistResponseHeader");

            migrationBuilder.DropColumn(
                name: "ValidatedById",
                table: "ChecklistResponseHeader");

            migrationBuilder.DropColumn(
                name: "ValidationResult",
                table: "ChecklistResponseHeader");
        }
    }
}
