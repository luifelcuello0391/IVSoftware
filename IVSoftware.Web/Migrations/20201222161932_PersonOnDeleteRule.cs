using Microsoft.EntityFrameworkCore.Migrations;

namespace IVSoftware.Web.Migrations
{
    public partial class PersonOnDeleteRule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Arl_ArlId",
                table: "Person");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_BloodType_BloodTypeId",
                table: "Person");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_Eps_EpsId",
                table: "Person");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Arl_ArlId",
                table: "Person",
                column: "ArlId",
                principalTable: "Arl",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Person_BloodType_BloodTypeId",
                table: "Person",
                column: "BloodTypeId",
                principalTable: "BloodType",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Eps_EpsId",
                table: "Person",
                column: "EpsId",
                principalTable: "Eps",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Arl_ArlId",
                table: "Person");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_BloodType_BloodTypeId",
                table: "Person");

            migrationBuilder.DropForeignKey(
                name: "FK_Person_Eps_EpsId",
                table: "Person");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Arl_ArlId",
                table: "Person",
                column: "ArlId",
                principalTable: "Arl",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Person_BloodType_BloodTypeId",
                table: "Person",
                column: "BloodTypeId",
                principalTable: "BloodType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Eps_EpsId",
                table: "Person",
                column: "EpsId",
                principalTable: "Eps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
