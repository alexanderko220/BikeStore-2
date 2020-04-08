using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BikeStore.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorys",
                columns: table => new
                {
                    CatId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MainCatId = table.Column<long>(nullable: true),
                    CatName = table.Column<string>(nullable: true),
                    IsCategoryActive = table.Column<bool>(nullable: false),
                    CatDescr = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorys", x => x.CatId);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    ColorId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ColorValue = table.Column<string>(nullable: true),
                    ColorName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.ColorId);
                });

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    SizeId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SizeValue = table.Column<string>(nullable: true),
                    SizeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.SizeId);
                });

            migrationBuilder.CreateTable(
                name: "SpecificationCategory",
                columns: table => new
                {
                    SpecCatId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SpecCatName = table.Column<string>(nullable: true),
                    IsSpecCatActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecificationCategory", x => x.SpecCatId);
                });

            migrationBuilder.CreateTable(
                name: "StoreImages",
                columns: table => new
                {
                    StoreImgId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreImages", x => x.StoreImgId);
                });

            migrationBuilder.CreateTable(
                name: "Specifications",
                columns: table => new
                {
                    SpecId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    SpecCatId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specifications", x => x.SpecId);
                    table.ForeignKey(
                        name: "FK_Specifications_SpecificationCategory_SpecCatId",
                        column: x => x.SpecCatId,
                        principalTable: "SpecificationCategory",
                        principalColumn: "SpecCatId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bikes",
                columns: table => new
                {
                    BikeId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Brand = table.Column<string>(maxLength: 100, nullable: true),
                    Model = table.Column<string>(maxLength: 255, nullable: true),
                    IsInStock = table.Column<bool>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ThumbBase64 = table.Column<string>(nullable: true),
                    CategoryId = table.Column<long>(nullable: false),
                    ImgId = table.Column<long>(nullable: false),
                    ImagesStoreImgId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bikes", x => x.BikeId);
                    table.ForeignKey(
                        name: "FK_Bikes_Categorys_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categorys",
                        principalColumn: "CatId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bikes_StoreImages_ImagesStoreImgId",
                        column: x => x.ImagesStoreImgId,
                        principalTable: "StoreImages",
                        principalColumn: "StoreImgId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImgContent",
                columns: table => new
                {
                    ImgContentId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImgContentName = table.Column<string>(nullable: true),
                    ImgContentMimeType = table.Column<string>(nullable: true),
                    ImgCreateDt = table.Column<DateTime>(nullable: true),
                    Content = table.Column<byte[]>(nullable: true),
                    StoreImgId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImgContent", x => x.ImgContentId);
                    table.ForeignKey(
                        name: "FK_ImgContent_StoreImages_StoreImgId",
                        column: x => x.StoreImgId,
                        principalTable: "StoreImages",
                        principalColumn: "StoreImgId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BikesColors",
                columns: table => new
                {
                    BikeId = table.Column<long>(nullable: false),
                    ColorId = table.Column<long>(nullable: false),
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SizeId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BikesColors", x => new { x.BikeId, x.ColorId });
                    table.UniqueConstraint("AK_BikesColors_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BikesColors_Bikes_BikeId",
                        column: x => x.BikeId,
                        principalTable: "Bikes",
                        principalColumn: "BikeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BikesColors_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "ColorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BikesColors_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "SizeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BikesSizes",
                columns: table => new
                {
                    BikeId = table.Column<long>(nullable: false),
                    SizeId = table.Column<long>(nullable: false),
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BikesSizes", x => new { x.BikeId, x.SizeId });
                    table.UniqueConstraint("AK_BikesSizes_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BikesSizes_Bikes_BikeId",
                        column: x => x.BikeId,
                        principalTable: "Bikes",
                        principalColumn: "BikeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BikesSizes_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "SizeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BikesSpecifications",
                columns: table => new
                {
                    BikeId = table.Column<long>(nullable: false),
                    SpecificationId = table.Column<long>(nullable: false),
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BikesSpecifications", x => new { x.BikeId, x.SpecificationId });
                    table.UniqueConstraint("AK_BikesSpecifications_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BikesSpecifications_Bikes_BikeId",
                        column: x => x.BikeId,
                        principalTable: "Bikes",
                        principalColumn: "BikeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BikesSpecifications_Specifications_SpecificationId",
                        column: x => x.SpecificationId,
                        principalTable: "Specifications",
                        principalColumn: "SpecId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bikes_CategoryId",
                table: "Bikes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Bikes_ImagesStoreImgId",
                table: "Bikes",
                column: "ImagesStoreImgId");

            migrationBuilder.CreateIndex(
                name: "IX_BikesColors_ColorId",
                table: "BikesColors",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_BikesColors_SizeId",
                table: "BikesColors",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_BikesSizes_SizeId",
                table: "BikesSizes",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_BikesSpecifications_SpecificationId",
                table: "BikesSpecifications",
                column: "SpecificationId");

            migrationBuilder.CreateIndex(
                name: "IX_ImgContent_StoreImgId",
                table: "ImgContent",
                column: "StoreImgId");

            migrationBuilder.CreateIndex(
                name: "IX_Specifications_SpecCatId",
                table: "Specifications",
                column: "SpecCatId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BikesColors");

            migrationBuilder.DropTable(
                name: "BikesSizes");

            migrationBuilder.DropTable(
                name: "BikesSpecifications");

            migrationBuilder.DropTable(
                name: "ImgContent");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropTable(
                name: "Bikes");

            migrationBuilder.DropTable(
                name: "Specifications");

            migrationBuilder.DropTable(
                name: "Categorys");

            migrationBuilder.DropTable(
                name: "StoreImages");

            migrationBuilder.DropTable(
                name: "SpecificationCategory");
        }
    }
}
