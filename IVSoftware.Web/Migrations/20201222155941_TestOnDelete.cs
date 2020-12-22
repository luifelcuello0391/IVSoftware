using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class TestOnDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Gender_GenderId",
                table: "Person");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Gender_GenderId",
                table: "Person",
                column: "GenderId",
                principalTable: "Gender",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Gender_GenderId",
                table: "Person");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Gender_GenderId",
                table: "Person",
                column: "GenderId",
                principalTable: "Gender",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
