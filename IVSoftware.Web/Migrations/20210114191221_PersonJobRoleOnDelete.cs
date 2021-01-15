using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class PersonJobRoleOnDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonJobRoles_JobRole_JobRoleId",
                table: "PersonJobRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonJobRoles_Person_PersonId",
                table: "PersonJobRoles");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonJobRoles_JobRole_JobRoleId",
                table: "PersonJobRoles",
                column: "JobRoleId",
                principalTable: "JobRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonJobRoles_Person_PersonId",
                table: "PersonJobRoles",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonJobRoles_JobRole_JobRoleId",
                table: "PersonJobRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonJobRoles_Person_PersonId",
                table: "PersonJobRoles");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonJobRoles_JobRole_JobRoleId",
                table: "PersonJobRoles",
                column: "JobRoleId",
                principalTable: "JobRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonJobRoles_Person_PersonId",
                table: "PersonJobRoles",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
