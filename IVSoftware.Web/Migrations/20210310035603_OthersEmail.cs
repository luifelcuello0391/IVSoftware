using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class OthersEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OthersEmail",
                table: "Person",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OthersEmail",
                table: "Person");
        }
    }
}
