using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class PersonEvaluationID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonEvaluations",
                table: "PersonEvaluations");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "PersonEvaluations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonEvaluations",
                table: "PersonEvaluations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PersonEvaluations_PersonId",
                table: "PersonEvaluations",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonEvaluations",
                table: "PersonEvaluations");

            migrationBuilder.DropIndex(
                name: "IX_PersonEvaluations_PersonId",
                table: "PersonEvaluations");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PersonEvaluations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonEvaluations",
                table: "PersonEvaluations",
                columns: new[] { "PersonId", "EvaluationId" });
        }
    }
}
