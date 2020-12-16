using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class BaseModelAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDatetime",
                table: "VariableModel",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDatetime",
                table: "VariableModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "VariableModel",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegisterStatus",
                table: "VariableModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDatetime",
                table: "ReferenceMethodModel",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDatetime",
                table: "ReferenceMethodModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ReferenceMethodModel",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegisterStatus",
                table: "ReferenceMethodModel",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDatetime",
                table: "VariableModel");

            migrationBuilder.DropColumn(
                name: "ModificationDatetime",
                table: "VariableModel");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "VariableModel");

            migrationBuilder.DropColumn(
                name: "RegisterStatus",
                table: "VariableModel");

            migrationBuilder.DropColumn(
                name: "CreationDatetime",
                table: "ReferenceMethodModel");

            migrationBuilder.DropColumn(
                name: "ModificationDatetime",
                table: "ReferenceMethodModel");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ReferenceMethodModel");

            migrationBuilder.DropColumn(
                name: "RegisterStatus",
                table: "ReferenceMethodModel");
        }
    }
}
