using System;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class JobRoleNameFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonJobRol");

            migrationBuilder.DropTable(
                name: "JobRol");

            migrationBuilder.CreateTable(
                name: "JobRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonJobRoles",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobRoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonJobRoles", x => new { x.PersonId, x.JobRoleId });
                    table.ForeignKey(
                        name: "FK_PersonJobRoles_JobRole_JobRoleId",
                        column: x => x.JobRoleId,
                        principalTable: "JobRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonJobRoles_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonJobRoles_JobRoleId",
                table: "PersonJobRoles",
                column: "JobRoleId");

            var sqlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"InitialScripts\JobRolesData.sql");
            migrationBuilder.Sql(File.ReadAllText(sqlFile, Encoding.GetEncoding(28591)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonJobRoles");

            migrationBuilder.DropTable(
                name: "JobRole");

            migrationBuilder.CreateTable(
                name: "JobRol",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobRol", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonJobRol",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobRolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonJobRol", x => new { x.PersonId, x.JobRolId });
                    table.ForeignKey(
                        name: "FK_PersonJobRol_JobRol_JobRolId",
                        column: x => x.JobRolId,
                        principalTable: "JobRol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonJobRol_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonJobRol_JobRolId",
                table: "PersonJobRol",
                column: "JobRolId");
        }
    }
}
