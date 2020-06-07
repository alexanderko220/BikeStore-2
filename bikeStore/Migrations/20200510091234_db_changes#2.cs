using Microsoft.EntityFrameworkCore.Migrations;

namespace BikeStore.Migrations
{
    public partial class db_changes2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bikes_Categorys_CategoryId",
                table: "Bikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categorys",
                table: "Categorys");

            migrationBuilder.RenameTable(
                name: "Categorys",
                newName: "Categories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "CatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bikes_Categories_CategoryId",
                table: "Bikes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CatId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bikes_Categories_CategoryId",
                table: "Bikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Categorys");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categorys",
                table: "Categorys",
                column: "CatId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bikes_Categorys_CategoryId",
                table: "Bikes",
                column: "CategoryId",
                principalTable: "Categorys",
                principalColumn: "CatId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
