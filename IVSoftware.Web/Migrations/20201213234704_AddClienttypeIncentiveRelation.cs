using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class AddClienttypeIncentiveRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IncentiveModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<float>(type: "real", nullable: false),
                    IsPercentage = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisterStatus = table.Column<int>(type: "int", nullable: false),
                    CreationDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDatetime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncentiveModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientTypeIncentiveRelation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientTypeId = table.Column<int>(type: "int", nullable: true),
                    IncentiveId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientTypeIncentiveRelation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientTypeIncentiveRelation_ClientTypeModel_ClientTypeId",
                        column: x => x.ClientTypeId,
                        principalTable: "ClientTypeModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientTypeIncentiveRelation_IncentiveModel_IncentiveId",
                        column: x => x.IncentiveId,
                        principalTable: "IncentiveModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientTypeIncentiveRelation_ClientTypeId",
                table: "ClientTypeIncentiveRelation",
                column: "ClientTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientTypeIncentiveRelation_IncentiveId",
                table: "ClientTypeIncentiveRelation",
                column: "IncentiveId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientTypeIncentiveRelation");

            migrationBuilder.DropTable(
                name: "IncentiveModel");
        }
    }
}
