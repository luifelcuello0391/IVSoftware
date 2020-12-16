using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class QuestionSectionManyToMany4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckListQuestionCheckListSection_CheckListQuestion_QuestionsId",
                table: "CheckListQuestionCheckListSection");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckListQuestionCheckListSection_CheckListSection_SectionsId",
                table: "CheckListQuestionCheckListSection");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckListQuestionCheckListSection_CheckListQuestion_QuestionsId",
                table: "CheckListQuestionCheckListSection",
                column: "QuestionsId",
                principalTable: "CheckListQuestion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckListQuestionCheckListSection_CheckListSection_SectionsId",
                table: "CheckListQuestionCheckListSection",
                column: "SectionsId",
                principalTable: "CheckListSection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckListQuestionCheckListSection_CheckListQuestion_QuestionsId",
                table: "CheckListQuestionCheckListSection");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckListQuestionCheckListSection_CheckListSection_SectionsId",
                table: "CheckListQuestionCheckListSection");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckListQuestionCheckListSection_CheckListQuestion_QuestionsId",
                table: "CheckListQuestionCheckListSection",
                column: "QuestionsId",
                principalTable: "CheckListQuestion",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckListQuestionCheckListSection_CheckListSection_SectionsId",
                table: "CheckListQuestionCheckListSection",
                column: "SectionsId",
                principalTable: "CheckListSection",
                principalColumn: "Id");
        }
    }
}
