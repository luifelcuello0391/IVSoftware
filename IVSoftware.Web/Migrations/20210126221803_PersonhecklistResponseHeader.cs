using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class PersonhecklistResponseHeader : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistResponseHeader_Person_ValidatedById",
                table: "ChecklistResponseHeader");

            migrationBuilder.AddColumn<Guid>(
                name: "PersonId",
                table: "ChecklistResponseHeader",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistResponseHeader_PersonId",
                table: "ChecklistResponseHeader",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistResponseHeader_Person_PersonId",
                table: "ChecklistResponseHeader",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistResponseHeader_Person_ValidatedById",
                table: "ChecklistResponseHeader",
                column: "ValidatedById",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistResponseHeader_Person_PersonId",
                table: "ChecklistResponseHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_ChecklistResponseHeader_Person_ValidatedById",
                table: "ChecklistResponseHeader");

            migrationBuilder.DropIndex(
                name: "IX_ChecklistResponseHeader_PersonId",
                table: "ChecklistResponseHeader");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "ChecklistResponseHeader");

            migrationBuilder.AddForeignKey(
                name: "FK_ChecklistResponseHeader_Person_ValidatedById",
                table: "ChecklistResponseHeader",
                column: "ValidatedById",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
