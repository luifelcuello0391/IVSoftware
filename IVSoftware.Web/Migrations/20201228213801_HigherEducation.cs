using System;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class HigherEducation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcademicLevel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicLevel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HigherEducation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InstitutionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DegreeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudiesName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfessionalCardNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcademicLevelId = table.Column<int>(type: "int", nullable: false),
                    PersonId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PersonId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HigherEducation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HigherEducation_AcademicLevel_AcademicLevelId",
                        column: x => x.AcademicLevelId,
                        principalTable: "AcademicLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HigherEducation_Person_PersonId1",
                        column: x => x.PersonId1,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HigherEducation_AcademicLevelId",
                table: "HigherEducation",
                column: "AcademicLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_HigherEducation_PersonId1",
                table: "HigherEducation",
                column: "PersonId1");

            var sqlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"InitialScripts\AcademyLevelData.sql");
            migrationBuilder.Sql(File.ReadAllText(sqlFile, Encoding.GetEncoding(28591)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HigherEducation");

            migrationBuilder.DropTable(
                name: "AcademicLevel");
        }
    }
}
