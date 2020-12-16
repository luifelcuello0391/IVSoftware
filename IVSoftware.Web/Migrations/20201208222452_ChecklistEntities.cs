using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class ChecklistEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckListQuestionEquipmentCheckList");

            migrationBuilder.DropTable(
                name: "CheckListSectionEquipmentCheckList");

            migrationBuilder.DropTable(
                name: "EquipmentEquipmentCheckList");

            migrationBuilder.AddColumn<int>(
                name: "CheckListId",
                table: "Equipment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EquipmentCheckListQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<int>(type: "int", nullable: true),
                    CheckListId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentCheckListQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentCheckListQuestions_CheckListQuestion_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "CheckListQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EquipmentCheckListQuestions_EquipmentCheckList_CheckListId",
                        column: x => x.CheckListId,
                        principalTable: "EquipmentCheckList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentCheckListSections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectionId = table.Column<int>(type: "int", nullable: true),
                    ChecklistId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentCheckListSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentCheckListSections_CheckListSection_SectionId",
                        column: x => x.SectionId,
                        principalTable: "CheckListSection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EquipmentCheckListSections_EquipmentCheckList_ChecklistId",
                        column: x => x.ChecklistId,
                        principalTable: "EquipmentCheckList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipment_CheckListId",
                table: "Equipment",
                column: "CheckListId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentCheckListQuestions_CheckListId",
                table: "EquipmentCheckListQuestions",
                column: "CheckListId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentCheckListQuestions_QuestionId",
                table: "EquipmentCheckListQuestions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentCheckListSections_ChecklistId",
                table: "EquipmentCheckListSections",
                column: "ChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentCheckListSections_SectionId",
                table: "EquipmentCheckListSections",
                column: "SectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_EquipmentCheckList_CheckListId",
                table: "Equipment",
                column: "CheckListId",
                principalTable: "EquipmentCheckList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_EquipmentCheckList_CheckListId",
                table: "Equipment");

            migrationBuilder.DropTable(
                name: "EquipmentCheckListQuestions");

            migrationBuilder.DropTable(
                name: "EquipmentCheckListSections");

            migrationBuilder.DropIndex(
                name: "IX_Equipment_CheckListId",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "CheckListId",
                table: "Equipment");

            migrationBuilder.CreateTable(
                name: "CheckListQuestionEquipmentCheckList",
                columns: table => new
                {
                    CheckListId = table.Column<int>(type: "int", nullable: false),
                    QuestionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckListQuestionEquipmentCheckList", x => new { x.CheckListId, x.QuestionsId });
                    table.ForeignKey(
                        name: "FK_CheckListQuestionEquipmentCheckList_CheckListQuestion_QuestionsId",
                        column: x => x.QuestionsId,
                        principalTable: "CheckListQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CheckListQuestionEquipmentCheckList_EquipmentCheckList_CheckListId",
                        column: x => x.CheckListId,
                        principalTable: "EquipmentCheckList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CheckListSectionEquipmentCheckList",
                columns: table => new
                {
                    CheckListsId = table.Column<int>(type: "int", nullable: false),
                    SectionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckListSectionEquipmentCheckList", x => new { x.CheckListsId, x.SectionsId });
                    table.ForeignKey(
                        name: "FK_CheckListSectionEquipmentCheckList_CheckListSection_SectionsId",
                        column: x => x.SectionsId,
                        principalTable: "CheckListSection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CheckListSectionEquipmentCheckList_EquipmentCheckList_CheckListsId",
                        column: x => x.CheckListsId,
                        principalTable: "EquipmentCheckList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentEquipmentCheckList",
                columns: table => new
                {
                    CheckListsId = table.Column<int>(type: "int", nullable: false),
                    EquipmentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentEquipmentCheckList", x => new { x.CheckListsId, x.EquipmentsId });
                    table.ForeignKey(
                        name: "FK_EquipmentEquipmentCheckList_Equipment_EquipmentsId",
                        column: x => x.EquipmentsId,
                        principalTable: "Equipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EquipmentEquipmentCheckList_EquipmentCheckList_CheckListsId",
                        column: x => x.CheckListsId,
                        principalTable: "EquipmentCheckList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckListQuestionEquipmentCheckList_QuestionsId",
                table: "CheckListQuestionEquipmentCheckList",
                column: "QuestionsId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckListSectionEquipmentCheckList_SectionsId",
                table: "CheckListSectionEquipmentCheckList",
                column: "SectionsId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentEquipmentCheckList_EquipmentsId",
                table: "EquipmentEquipmentCheckList",
                column: "EquipmentsId");
        }
    }
}
