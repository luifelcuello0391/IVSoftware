using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class MaintenancePersonRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PersonId",
                table: "Maintenances",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Maintenances_PersonId",
                table: "Maintenances",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_Person_PersonId",
                table: "Maintenances",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_Person_PersonId",
                table: "Maintenances");

            migrationBuilder.DropIndex(
                name: "IX_Maintenances_PersonId",
                table: "Maintenances");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Maintenances");
        }
    }
}
