using Microsoft.EntityFrameworkCore.Migrations;

namespace ZnymkyHub.DAL.Migrations
{
    public partial class AddedPhotoResolutionForPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PhotoshootTypeId",
                table: "Photos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_PhotoshootTypeId",
                table: "Photos",
                column: "PhotoshootTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_PhotoshootTypes_PhotoshootTypeId",
                table: "Photos",
                column: "PhotoshootTypeId",
                principalTable: "PhotoshootTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_PhotoshootTypes_PhotoshootTypeId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_PhotoshootTypeId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "PhotoshootTypeId",
                table: "Photos");
        }
    }
}
