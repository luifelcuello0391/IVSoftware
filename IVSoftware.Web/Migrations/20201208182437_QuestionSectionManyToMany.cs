using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class QuestionSectionManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "CheckListQuestionCheckListSectionList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CheckListQuestionCheckListSection",
                table: "CheckListQuestionCheckListSection");

            migrationBuilder.AddColumn<int>(
                name: "CheckListQuestionCheckListSectionId",
                table: "CheckListQuestionCheckListSection",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "CheckListQuestionCheckListSection",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CheckListQuestionCheckListSection",
                table: "CheckListQuestionCheckListSection",
                column: "CheckListQuestionCheckListSectionId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckListQuestionCheckListSection_QuestionsId",
                table: "CheckListQuestionCheckListSection",
                column: "CheckListQuestionCheckListSectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_CheckListQuestionCheckListSection",
            //    table: "CheckListQuestionCheckListSection");

            //migrationBuilder.DropIndex(
            //    name: "IX_CheckListQuestionCheckListSection_QuestionsId",
            //    table: "CheckListQuestionCheckListSection");

            //migrationBuilder.DropColumn(
            //    name: "CheckListQuestionCheckListSectionId",
            //    table: "CheckListQuestionCheckListSection");

            //migrationBuilder.DropColumn(
            //    name: "Order",
            //    table: "CheckListQuestionCheckListSection");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_CheckListQuestionCheckListSection",
            //    table: "CheckListQuestionCheckListSection",
            //    columns: new[] { "QuestionsId", "SectionsId" });

            //migrationBuilder.CreateTable(
            //    name: "CheckListQuestionCheckListSectionList",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        QuestionsId = table.Column<int>(type: "int", nullable: false),
            //        SectionsId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_CheckListQuestionCheckListSectionList", x => x.Id);
            //    });
        }
    }
}
