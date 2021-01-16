using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.IO;
using System.Text;

namespace IVSoftware.Web.Migrations
{
    public partial class RuningCheckListScript : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"InitialScripts\QuestionAndCheckListScript.sql");
            migrationBuilder.Sql(File.ReadAllText(sqlFile, Encoding.GetEncoding(28591)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}