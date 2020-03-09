using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BikeStore.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
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
                    table.PrimaryKey("PK_Categories", x => x.CatId);
                });

            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    ColorId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ColorValue = table.Column<string>(nullable: true),
                    ColorName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.ColorId);
                });

            migrationBuilder.CreateTable(
                name: "Size",
                columns: table => new
                {
                    SizeId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SizeValue = table.Column<string>(nullable: true),
                    SizeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Size", x => x.SizeId);
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
                    Brand = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    IsInStock = table.Column<bool>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ThumbImgContent = table.Column<byte[]>(nullable: true),
                    CategoryId = table.Column<long>(nullable: false),
                    ImgId = table.Column<long>(nullable: false),
                    ImagesStoreImgId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bikes", x => x.BikeId);
                    table.ForeignKey(
                        name: "FK_Bikes_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
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
                name: "BikesColorSize",
                columns: table => new
                {
                    BikeId = table.Column<long>(nullable: false),
                    ColorId = table.Column<long>(nullable: false),
                    SizeId = table.Column<long>(nullable: false),
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BikesColorSize", x => new { x.BikeId, x.ColorId, x.SizeId });
                    table.UniqueConstraint("AK_BikesColorSize_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BikesColorSize_Bikes_BikeId",
                        column: x => x.BikeId,
                        principalTable: "Bikes",
                        principalColumn: "BikeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BikesColorSize_Color_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Color",
                        principalColumn: "ColorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BikesColorSize_Size_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Size",
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

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CatId", "CatDescr", "CatName", "IsCategoryActive", "MainCatId" },
                values: new object[,]
                {
                    { 1L, null, "Mountain", true, null },
                    { 18L, null, "Active", true, 4L },
                    { 17L, null, "Mountain", true, 4L },
                    { 16L, null, "Road", true, 4L },
                    { 15L, null, "Comfort", true, 3L },
                    { 14L, null, "Transport", true, 3L },
                    { 12L, null, "Triathlon", true, 2L },
                    { 11L, null, "Cyclocross", true, 2L },
                    { 10L, null, "Adventure & Gravel", true, 2L },
                    { 13L, null, "Fitness", true, 3L },
                    { 8L, null, "Dirt Jump", true, 1L },
                    { 7L, null, "Downhill", true, 1L },
                    { 6L, null, "Trail", true, 1L },
                    { 5L, null, "Cross Country", true, 1L },
                    { 4L, null, "Electric", true, null },
                    { 3L, null, "Active", true, null },
                    { 2L, null, "Road", true, null },
                    { 9L, null, "Performance", true, 2L }
                });

            migrationBuilder.InsertData(
                table: "SpecificationCategory",
                columns: new[] { "SpecCatId", "IsSpecCatActive", "SpecCatName" },
                values: new object[,]
                {
                    { 6L, true, "Accessories" },
                    { 1L, true, "Drivetrain" },
                    { 2L, true, "Suspension" },
                    { 3L, true, "Wheels" },
                    { 4L, true, "Cockpit" },
                    { 5L, true, "Brakes" },
                    { 7L, true, "Frameset" }
                });

            migrationBuilder.InsertData(
                table: "Bikes",
                columns: new[] { "BikeId", "Brand", "CategoryId", "ImagesStoreImgId", "ImgId", "IsInStock", "Model", "Price", "ThumbImgContent" },
                values: new object[,]
                {
                    { 1L, "Specialized", 5L, null, 0L, true, "S-Works Epic AXS", 1299.6m, null },
                    { 2L, "Specialized", 5L, null, 0L, true, "Epic Pro", 899.5m, null }
                });

            migrationBuilder.InsertData(
                table: "Specifications",
                columns: new[] { "SpecId", "Brand", "Description", "Model", "SpecCatId", "Type" },
                values: new object[,]
                {
                    { 30L, "Specialized", "sealed cartridge bearings, 15x110mm spacing, 28h", "", 3L, "FRONT HUB" },
                    { 31L, "DT Swiss", "Star Ratchet, 36t engagement, SRAM XD driver body, 12mm thru-axle, 148mm spacing, 28h", "350", 3L, "REAR HUB" },
                    { 32L, "DT Swiss", "", "Industry", 3L, "SPOKES" },
                    { 33L, "Roval", "25mm internal width, tubeless-ready", "Control Carbon", 3L, "RIMS" },
                    { 17L, "Specialized", "carbon fiber rails, carbon fiber base, 143mm", "Body Geometry S-Works Power", 4L, "SADDLE" },
                    { 18L, "Specialized", "10mm setback, 30.9mm", "S-Works FACT carbon", 4L, "SEATPOST" },
                    { 19L, "Specialized", "alloy, titanium bolts, 6-degree rise", "S-Works SL", 4L, "STEM" },
                    { 20L, "Specialized", "6-degree upsweep, 8-degree backsweep, 10mm rise, 760mm, 31.8mm", "S-Works Carbon XC Mini Rise", 4L, "HANDLEBARS" },
                    { 21L, "Specialized", "", "Trail Grips", 4L, "GRIPS" },
                    { 34L, "Specialized", "Hollow Titanium rails, 143mm", "Body Geometry Power", 4L, "SADDLE" },
                    { 35L, "Specialized", " single-bolt, 30.9mm", "Carbon", 4L, "SEATPOST" },
                    { 36L, "Specialized", "3D-forged alloy, 4-bolt, 6-degree rise", "XC", 4L, "STEM" },
                    { 22L, "SRAM", "hydraulic disc", "Level Ultimate", 5L, "FRONT BRAKE" },
                    { 23L, "SRAM", "hydraulic disc", "Level Ultimate", 5L, "REAR BRAKE" },
                    { 37L, "SRAM", "hydraulic disc", "Level TLM", 5L, "FRONT BRAKE" },
                    { 24L, "Specialized", "", "Dirt", 6L, "PEDALS" },
                    { 16L, "Specialized", "Control casing, GRIPTON® compound, 60 TPI, 2Bliss Ready, 29x2.3", "Fast Trak", 3L, "REAR TIRE" },
                    { 15L, "Specialized", "Control casing, GRIPTON® compound, 60 TPI, 2Bliss Ready, 29x2.3", "Fast Trak", 3L, "FRONT TIRE" },
                    { 14L, "Roval", "hookless carbon, 25mm internal width, tubeless-ready, hand-built", "Control SL", 3L, "RIMS" },
                    { 13L, "DT Swiss", "Competition Race", "", 3L, "SPOKES" },
                    { 1L, "SRAM", "(Rainbow)", "XX1", 1L, "CHAIN" },
                    { 2L, "SRAM", "threaded BB", "DUB", 1L, "BOTTOM BRACKET" },
                    { 3L, "QUARQ", "Boost™ 148, DUB, 170/175mm", "XX1 Eagle Power Meter", 1L, "CRANKSET" },
                    { 4L, "SRAM", "trigger, 12-speed", "XX1 Eagle AXS", 1L, "SHIFT LEVERS" },
                    { 5L, "SRAM", "10-50t", "XG-1299 Eagle", 1L, "CASSETTE" },
                    { 6L, "Alloy", "32T", "", 1L, "CHAINRINGS" },
                    { 7L, "SRAM", "", "XX1 Eagle AXS", 1L, "REAR DERAILLEUR" },
                    { 25L, "Specialized", "XC Geometry, Rider-First Engineered™, threaded BB, 12x148mm rear spacing, internal cable routing, 100mm of travel", "S-Works FACT 12m", 7L, "SEAT BINDER" },
                    { 26L, "SRAM", "12-speed", "GX Eagle", 1L, "CHAIN" },
                    { 28L, "SRAM", "12-speed, 10-50t", "XG-1295 Eagle", 1L, "CASSETTE" },
                    { 8L, "RockShox", "top-adjust Brain Fade, tapered carbon crown and steerer, 15x110mm Maxle® Stealth thru-axle, 42mm offset, 100mm of travel", "SID Brain Ultimate", 2L, "FORK" },
                    { 9L, "RockShox", "AUTOSAG, 51x257mm", "Custom Micro Brain shock w/ Spike Valve", 2L, "REAR SHOCK" },
                    { 29L, "RockShox", "Position Sensitive, top-adjust Brain Fade, 15x110mm Maxle® Stealth thru-axle, 42mm offset, 100mm of travel", "SID Brain 29", 2L, "FORK" },
                    { 10L, "Roval", "sealed cartridge bearings, 15mm thru-axle, 110mm spacing, 24h", "Control SL", 3L, "FRONT HUB" },
                    { 11L, "Roval", "DT Swiss Star Ratchet, 54t engagement, SRAM XD driver body, 12mm thru-axle, 148mm spacing, 28h", "Control SL", 3L, "REAR HUB" },
                    { 12L, "Presta", "60mm valve", "", 3L, "INNER TUBES" },
                    { 27L, "SRAM", "trigger, 12-speed", "X01 Eagle", 1L, "SHIFT LEVERS" },
                    { 38L, "Specialized", "Alloy, 34.9mm", "", 7L, "SEAT BINDER" }
                });

            migrationBuilder.InsertData(
                table: "BikesSpecifications",
                columns: new[] { "BikeId", "SpecificationId", "Id" },
                values: new object[,]
                {
                    { 1L, 1L, 1L },
                    { 2L, 16L, 41L },
                    { 2L, 30L, 35L },
                    { 2L, 31L, 36L },
                    { 2L, 32L, 37L },
                    { 2L, 33L, 38L },
                    { 1L, 17L, 17L },
                    { 1L, 18L, 18L },
                    { 1L, 19L, 19L },
                    { 1L, 20L, 20L },
                    { 2L, 20L, 45L },
                    { 1L, 21L, 21L },
                    { 2L, 21L, 46L },
                    { 2L, 34L, 42L },
                    { 2L, 35L, 43L },
                    { 2L, 36L, 44L },
                    { 1L, 22L, 22L },
                    { 1L, 23L, 23L },
                    { 2L, 23L, 48L },
                    { 2L, 37L, 47L },
                    { 1L, 24L, 24L },
                    { 2L, 24L, 49L },
                    { 1L, 16L, 16L },
                    { 2L, 15L, 40L },
                    { 1L, 15L, 15L },
                    { 2L, 14L, 39L },
                    { 1L, 2L, 2L },
                    { 2L, 2L, 27L },
                    { 1L, 3L, 3L },
                    { 2L, 3L, 28L },
                    { 1L, 4L, 4L },
                    { 1L, 5L, 5L },
                    { 1L, 6L, 6L },
                    { 2L, 6L, 31L },
                    { 1L, 7L, 7L },
                    { 2L, 7L, 32L },
                    { 1L, 25L, 25L },
                    { 2L, 26L, 26L },
                    { 2L, 28L, 30L },
                    { 1L, 8L, 8L },
                    { 1L, 9L, 9L },
                    { 2L, 9L, 34L },
                    { 2L, 29L, 33L },
                    { 1L, 10L, 10L },
                    { 1L, 11L, 11L },
                    { 1L, 12L, 12L },
                    { 1L, 13L, 13L },
                    { 1L, 14L, 14L },
                    { 2L, 27L, 29L },
                    { 2L, 38L, 50L }
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
                name: "IX_BikesColorSize_ColorId",
                table: "BikesColorSize",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_BikesColorSize_SizeId",
                table: "BikesColorSize",
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
                name: "BikesColorSize");

            migrationBuilder.DropTable(
                name: "BikesSpecifications");

            migrationBuilder.DropTable(
                name: "ImgContent");

            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.DropTable(
                name: "Size");

            migrationBuilder.DropTable(
                name: "Bikes");

            migrationBuilder.DropTable(
                name: "Specifications");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "StoreImages");

            migrationBuilder.DropTable(
                name: "SpecificationCategory");
        }
    }
}
