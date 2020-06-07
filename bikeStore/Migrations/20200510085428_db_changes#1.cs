using Microsoft.EntityFrameworkCore.Migrations;

namespace BikeStore.Migrations
{
    public partial class db_changes1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsThumbnail",
                table: "ImgContent",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsThumbnail",
                table: "ImgContent");
        }
    }
}
