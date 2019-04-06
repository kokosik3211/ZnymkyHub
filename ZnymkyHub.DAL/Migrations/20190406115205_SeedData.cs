using System.IO;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZnymkyHub.DAL.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).ToString(), "ZnymkyHub.DAL", "SQLScripts", "Inserting_data_script.sql");
            migrationBuilder.Sql(File.ReadAllText(path));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
