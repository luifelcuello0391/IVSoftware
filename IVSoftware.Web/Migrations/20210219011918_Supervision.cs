using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class Supervision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Supervision",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupervisedPersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupervisedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Analyte = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supervision", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Supervision_Person_SupervisedById",
                        column: x => x.SupervisedById,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Supervision_Person_SupervisedPersonId",
                        column: x => x.SupervisedPersonId,
                        principalTable: "Person",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SupervisionDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PHVA = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Process = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SupervisedActivity = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    Result = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Observations = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    SupervisionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupervisionDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupervisionDetail_Supervision_SupervisionId",
                        column: x => x.SupervisionId,
                        principalTable: "Supervision",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Supervision_SupervisedById",
                table: "Supervision",
                column: "SupervisedById");

            migrationBuilder.CreateIndex(
                name: "IX_Supervision_SupervisedPersonId",
                table: "Supervision",
                column: "SupervisedPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_SupervisionDetail_SupervisionId",
                table: "SupervisionDetail",
                column: "SupervisionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SupervisionDetail");

            migrationBuilder.DropTable(
                name: "Supervision");
        }
    }
}
