using System.IO;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZnymkyHub.DAL.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string script = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).ToString(), "ZnymkyHub.DAL", "InitialPhotos", "SeedData.sql");
            migrationBuilder.Sql(File.ReadAllText(script));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
