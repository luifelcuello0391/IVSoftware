using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class AddServiceMatrixRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MatrixGroupId",
                table: "ServiceModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceModel_MatrixGroupId",
                table: "ServiceModel",
                column: "MatrixGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceModel_MatrixModel_MatrixGroupId",
                table: "ServiceModel",
                column: "MatrixGroupId",
                principalTable: "MatrixModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceModel_MatrixModel_MatrixGroupId",
                table: "ServiceModel");

            migrationBuilder.DropIndex(
                name: "IX_ServiceModel_MatrixGroupId",
                table: "ServiceModel");

            migrationBuilder.DropColumn(
                name: "MatrixGroupId",
                table: "ServiceModel");
        }
    }
}
