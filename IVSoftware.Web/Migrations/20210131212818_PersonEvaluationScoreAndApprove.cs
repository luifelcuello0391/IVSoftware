using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class PersonEvaluationScoreAndApprove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "PersonEvaluations",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "PersonEvaluations",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "PersonEvaluations");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "PersonEvaluations");
        }
    }
}
