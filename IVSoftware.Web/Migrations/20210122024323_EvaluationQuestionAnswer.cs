using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class EvaluationQuestionAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EvaluationQuestion",
                table: "EvaluationQuestion");

            migrationBuilder.RenameTable(
                name: "EvaluationQuestion",
                newName: "EvaluationQuestionBank");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EvaluationQuestionBank",
                table: "EvaluationQuestionBank",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "EvaluationQuestionAnswer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsRight = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationQuestionAnswer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionAnswers",
                columns: table => new
                {
                    EvaluationQuestionBankId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EvaluationQuestionAnswerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAnswers", x => new { x.EvaluationQuestionBankId, x.EvaluationQuestionAnswerId });
                    table.ForeignKey(
                        name: "FK_QuestionAnswers_EvaluationQuestionAnswer_EvaluationQuestionAnswerId",
                        column: x => x.EvaluationQuestionAnswerId,
                        principalTable: "EvaluationQuestionAnswer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionAnswers_EvaluationQuestionBank_EvaluationQuestionBankId",
                        column: x => x.EvaluationQuestionBankId,
                        principalTable: "EvaluationQuestionBank",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswers_EvaluationQuestionAnswerId",
                table: "QuestionAnswers",
                column: "EvaluationQuestionAnswerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionAnswers");

            migrationBuilder.DropTable(
                name: "EvaluationQuestionAnswer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EvaluationQuestionBank",
                table: "EvaluationQuestionBank");

            migrationBuilder.RenameTable(
                name: "EvaluationQuestionBank",
                newName: "EvaluationQuestion");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EvaluationQuestion",
                table: "EvaluationQuestion",
                column: "Id");
        }
    }
}
