using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class IncludeBaseModelInfoIntoMatrixModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDatetime",
                table: "MatrixModel",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDatetime",
                table: "MatrixModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "MatrixModel",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegisterStatus",
                table: "MatrixModel",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDatetime",
                table: "MatrixModel");

            migrationBuilder.DropColumn(
                name: "ModificationDatetime",
                table: "MatrixModel");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "MatrixModel");

            migrationBuilder.DropColumn(
                name: "RegisterStatus",
                table: "MatrixModel");
        }
    }
}
