﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class QuestionSectionManyToMany3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SectionsId",
                table: "CheckListQuestionCheckListSection",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionsId",
                table: "CheckListQuestionCheckListSection",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SectionsId",
                table: "CheckListQuestionCheckListSection",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "QuestionsId",
                table: "CheckListQuestionCheckListSection",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
