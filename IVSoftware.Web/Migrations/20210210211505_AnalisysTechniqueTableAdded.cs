using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class AnalisysTechniqueTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnalisysTechniqueId",
                table: "ServiceModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AnalisysTechnique",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisterStatus = table.Column<int>(type: "int", nullable: false),
                    CreationDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDatetime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalisysTechnique", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceModel_AnalisysTechniqueId",
                table: "ServiceModel",
                column: "AnalisysTechniqueId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceModel_AnalisysTechnique_AnalisysTechniqueId",
                table: "ServiceModel",
                column: "AnalisysTechniqueId",
                principalTable: "AnalisysTechnique",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceModel_AnalisysTechnique_AnalisysTechniqueId",
                table: "ServiceModel");

            migrationBuilder.DropTable(
                name: "AnalisysTechnique");

            migrationBuilder.DropIndex(
                name: "IX_ServiceModel_AnalisysTechniqueId",
                table: "ServiceModel");

            migrationBuilder.DropColumn(
                name: "AnalisysTechniqueId",
                table: "ServiceModel");
        }
    }
}
