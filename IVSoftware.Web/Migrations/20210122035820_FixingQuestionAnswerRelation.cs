using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class FixingQuestionAnswerRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionAnswers");

            migrationBuilder.AddColumn<Guid>(
                name: "EvaluationQuestionBankId",
                table: "EvaluationQuestionAnswer",
                type: "uniqueidentifier",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationQuestionAnswer_EvaluationQuestionBankId",
                table: "EvaluationQuestionAnswer",
                column: "EvaluationQuestionBankId");

            migrationBuilder.AddForeignKey(
                name: "FK_EvaluationQuestionAnswer_EvaluationQuestionBank_EvaluationQuestionBankId",
                table: "EvaluationQuestionAnswer",
                column: "EvaluationQuestionBankId",
                principalTable: "EvaluationQuestionBank",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvaluationQuestionAnswer_EvaluationQuestionBank_EvaluationQuestionBankId",
                table: "EvaluationQuestionAnswer");

            migrationBuilder.DropIndex(
                name: "IX_EvaluationQuestionAnswer_EvaluationQuestionBankId",
                table: "EvaluationQuestionAnswer");

            migrationBuilder.DropColumn(
                name: "EvaluationQuestionBankId",
                table: "EvaluationQuestionAnswer");

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
    }
}
