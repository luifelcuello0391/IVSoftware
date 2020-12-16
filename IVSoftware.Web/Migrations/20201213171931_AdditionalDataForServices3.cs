using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class AdditionalDataForServices3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceModel_ServiceGroupModel_ServiceGroupModelId",
                table: "ServiceModel");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceModel_VariableModel_VariableId",
                table: "ServiceModel");

            migrationBuilder.DropIndex(
                name: "IX_ServiceModel_ServiceGroupModelId",
                table: "ServiceModel");

            migrationBuilder.DropIndex(
                name: "IX_ServiceModel_VariableId",
                table: "ServiceModel");

            migrationBuilder.DropColumn(
                name: "ServiceGroupModelId",
                table: "ServiceModel");

            migrationBuilder.DropColumn(
                name: "VariableId",
                table: "ServiceModel");

            migrationBuilder.AddColumn<int>(
                name: "ServiceModelId",
                table: "ServiceGroupModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ServiceGroupServicesRelation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceGroupId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceGroupServicesRelation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceGroupServicesRelation_ServiceGroupModel_ServiceGroupId",
                        column: x => x.ServiceGroupId,
                        principalTable: "ServiceGroupModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceGroupServicesRelation_ServiceModel_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "ServiceModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceGroupModel_ServiceModelId",
                table: "ServiceGroupModel",
                column: "ServiceModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceGroupServicesRelation_ServiceGroupId",
                table: "ServiceGroupServicesRelation",
                column: "ServiceGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceGroupServicesRelation_ServiceId",
                table: "ServiceGroupServicesRelation",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceGroupModel_ServiceModel_ServiceModelId",
                table: "ServiceGroupModel",
                column: "ServiceModelId",
                principalTable: "ServiceModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceGroupModel_ServiceModel_ServiceModelId",
                table: "ServiceGroupModel");

            migrationBuilder.DropTable(
                name: "ServiceGroupServicesRelation");

            migrationBuilder.DropIndex(
                name: "IX_ServiceGroupModel_ServiceModelId",
                table: "ServiceGroupModel");

            migrationBuilder.DropColumn(
                name: "ServiceModelId",
                table: "ServiceGroupModel");

            migrationBuilder.AddColumn<int>(
                name: "ServiceGroupModelId",
                table: "ServiceModel",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VariableId",
                table: "ServiceModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceModel_ServiceGroupModelId",
                table: "ServiceModel",
                column: "ServiceGroupModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceModel_VariableId",
                table: "ServiceModel",
                column: "VariableId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceModel_ServiceGroupModel_ServiceGroupModelId",
                table: "ServiceModel",
                column: "ServiceGroupModelId",
                principalTable: "ServiceGroupModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceModel_VariableModel_VariableId",
                table: "ServiceModel",
                column: "VariableId",
                principalTable: "VariableModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
