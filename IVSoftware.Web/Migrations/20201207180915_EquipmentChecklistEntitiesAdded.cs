using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class EquipmentChecklistEntitiesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CheckListQuestion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeOfQuestion = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisterStatus = table.Column<int>(type: "int", nullable: false),
                    CreationDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDatetime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckListQuestion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CheckListSection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisterStatus = table.Column<int>(type: "int", nullable: false),
                    CreationDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDatetime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckListSection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentCheckList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisterStatus = table.Column<int>(type: "int", nullable: false),
                    CreationDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDatetime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentCheckList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CheckListQuestionCheckListSection",
                columns: table => new
                {
                    QuestionsId = table.Column<int>(type: "int", nullable: false),
                    SectionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckListQuestionCheckListSection", x => new { x.QuestionsId, x.SectionsId });
                    table.ForeignKey(
                        name: "FK_CheckListQuestionCheckListSection_CheckListQuestion_QuestionsId",
                        column: x => x.QuestionsId,
                        principalTable: "CheckListQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CheckListQuestionCheckListSection_CheckListSection_SectionsId",
                        column: x => x.SectionsId,
                        principalTable: "CheckListSection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_CheckListQuestionCheckListSection_SectionsId",
                table: "CheckListQuestionCheckListSection",
                column: "SectionsId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckListQuestionCheckListSection");

            migrationBuilder.DropTable(
                name: "CheckListQuestionEquipmentCheckList");

            migrationBuilder.DropTable(
                name: "CheckListSectionEquipmentCheckList");

            migrationBuilder.DropTable(
                name: "EquipmentEquipmentCheckList");

            migrationBuilder.DropTable(
                name: "CheckListQuestion");

            migrationBuilder.DropTable(
                name: "CheckListSection");

            migrationBuilder.DropTable(
                name: "EquipmentCheckList");
        }
    }
}
