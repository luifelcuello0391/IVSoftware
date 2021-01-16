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
            try
            {
                var sqlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"InitialScripts\QuestionAndCheckListScript.sql");
                migrationBuilder.Sql(File.ReadAllText(sqlFile, Encoding.GetEncoding(28591)));
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error on Migration RuningCheckListScript >> " + ex.ToString());
            }
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}