using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BikeStore.Migrations
{
    public partial class InitialDb : Migration
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
                    SIId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SIDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreImages", x => x.SIId);
                });

            migrationBuilder.CreateTable(
                name: "Specification",
                columns: table => new
                {
                    SpecId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SpecType = table.Column<string>(nullable: true),
                    SpecBrand = table.Column<string>(nullable: true),
                    SpecModel = table.Column<string>(nullable: true),
                    SpecDescr = table.Column<string>(nullable: true),
                    SpecCatId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specification", x => x.SpecId);
                    table.ForeignKey(
                        name: "FK_Specification_SpecificationCategory_SpecCatId",
                        column: x => x.SpecCatId,
                        principalTable: "SpecificationCategory",
                        principalColumn: "SpecCatId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bikes",
                columns: table => new
                {
                    BId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BBrand = table.Column<string>(nullable: true),
                    BModel = table.Column<string>(nullable: true),
                    IsInStock = table.Column<bool>(nullable: false),
                    BPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BThumbImgContent = table.Column<byte[]>(nullable: true),
                    BCategoryId = table.Column<long>(nullable: false),
                    BImgId = table.Column<long>(nullable: false),
                    BImagesSIId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bikes", x => x.BId);
                    table.ForeignKey(
                        name: "FK_Bikes_Categories_BCategoryId",
                        column: x => x.BCategoryId,
                        principalTable: "Categories",
                        principalColumn: "CatId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bikes_StoreImages_BImagesSIId",
                        column: x => x.BImagesSIId,
                        principalTable: "StoreImages",
                        principalColumn: "SIId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImgContent",
                columns: table => new
                {
                    ICId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ICName = table.Column<string>(nullable: true),
                    ICMimeType = table.Column<string>(nullable: true),
                    ICContent = table.Column<byte[]>(nullable: true),
                    StoreImagesSIId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImgContent", x => x.ICId);
                    table.ForeignKey(
                        name: "FK_ImgContent_StoreImages_StoreImagesSIId",
                        column: x => x.StoreImagesSIId,
                        principalTable: "StoreImages",
                        principalColumn: "SIId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BikeJunction",
                columns: table => new
                {
                    BJId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BId = table.Column<long>(nullable: false),
                    BJColor = table.Column<int>(nullable: false),
                    BJSize = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BikeJunction", x => x.BJId);
                    table.ForeignKey(
                        name: "FK_BikeJunction_Bikes_BId",
                        column: x => x.BId,
                        principalTable: "Bikes",
                        principalColumn: "BId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BikeSpecJunction",
                columns: table => new
                {
                    BSJId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BSJBikeId = table.Column<long>(nullable: false),
                    BSJSpecId = table.Column<long>(nullable: false),
                    BSJSpecificationSpecId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BikeSpecJunction", x => x.BSJId);
                    table.ForeignKey(
                        name: "FK_BikeSpecJunction_Bikes_BSJBikeId",
                        column: x => x.BSJBikeId,
                        principalTable: "Bikes",
                        principalColumn: "BId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BikeSpecJunction_Specification_BSJSpecificationSpecId",
                        column: x => x.BSJSpecificationSpecId,
                        principalTable: "Specification",
                        principalColumn: "SpecId",
                        onDelete: ReferentialAction.Restrict);
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
                columns: new[] { "BId", "BBrand", "BCategoryId", "BImagesSIId", "BImgId", "BModel", "BPrice", "BThumbImgContent", "IsInStock" },
                values: new object[,]
                {
                    { 1L, "Specialized", 5L, null, 0L, "S-Works Epic AXS", 1299.6m, null, true },
                    { 2L, "Specialized", 5L, null, 0L, "Epic Pro", 899.5m, null, true }
                });

            migrationBuilder.InsertData(
                table: "Specification",
                columns: new[] { "SpecId", "SpecBrand", "SpecCatId", "SpecDescr", "SpecModel", "SpecType" },
                values: new object[,]
                {
                    { 30L, "Specialized", 3L, "sealed cartridge bearings, 15x110mm spacing, 28h", "", "FRONT HUB" },
                    { 31L, "DT Swiss", 3L, "Star Ratchet, 36t engagement, SRAM XD driver body, 12mm thru-axle, 148mm spacing, 28h", "350", "REAR HUB" },
                    { 32L, "DT Swiss", 3L, "", "Industry", "SPOKES" },
                    { 33L, "Roval", 3L, "25mm internal width, tubeless-ready", "Control Carbon", "RIMS" },
                    { 17L, "Specialized", 4L, "carbon fiber rails, carbon fiber base, 143mm", "Body Geometry S-Works Power", "SADDLE" },
                    { 18L, "Specialized", 4L, "10mm setback, 30.9mm", "S-Works FACT carbon", "SEATPOST" },
                    { 19L, "Specialized", 4L, "alloy, titanium bolts, 6-degree rise", "S-Works SL", "STEM" },
                    { 20L, "Specialized", 4L, "6-degree upsweep, 8-degree backsweep, 10mm rise, 760mm, 31.8mm", "S-Works Carbon XC Mini Rise", "HANDLEBARS" },
                    { 21L, "Specialized", 4L, "", "Trail Grips", "GRIPS" },
                    { 34L, "Specialized", 4L, "Hollow Titanium rails, 143mm", "Body Geometry Power", "SADDLE" },
                    { 35L, "Specialized", 4L, " single-bolt, 30.9mm", "Carbon", "SEATPOST" },
                    { 36L, "Specialized", 4L, "3D-forged alloy, 4-bolt, 6-degree rise", "XC", "STEM" },
                    { 22L, "SRAM", 5L, "hydraulic disc", "Level Ultimate", "FRONT BRAKE" },
                    { 23L, "SRAM", 5L, "hydraulic disc", "Level Ultimate", "REAR BRAKE" },
                    { 37L, "SRAM", 5L, "hydraulic disc", "Level TLM", "FRONT BRAKE" },
                    { 24L, "Specialized", 6L, "", "Dirt", "PEDALS" },
                    { 16L, "Specialized", 3L, "Control casing, GRIPTON® compound, 60 TPI, 2Bliss Ready, 29x2.3", "Fast Trak", "REAR TIRE" },
                    { 15L, "Specialized", 3L, "Control casing, GRIPTON® compound, 60 TPI, 2Bliss Ready, 29x2.3", "Fast Trak", "FRONT TIRE" },
                    { 14L, "Roval", 3L, "hookless carbon, 25mm internal width, tubeless-ready, hand-built", "Control SL", "RIMS" },
                    { 13L, "DT Swiss", 3L, "Competition Race", "", "SPOKES" },
                    { 1L, "SRAM", 1L, "(Rainbow)", "XX1", "CHAIN" },
                    { 2L, "SRAM", 1L, "threaded BB", "DUB", "BOTTOM BRACKET" },
                    { 3L, "QUARQ", 1L, "Boost™ 148, DUB, 170/175mm", "XX1 Eagle Power Meter", "CRANKSET" },
                    { 4L, "SRAM", 1L, "trigger, 12-speed", "XX1 Eagle AXS", "SHIFT LEVERS" },
                    { 5L, "SRAM", 1L, "10-50t", "XG-1299 Eagle", "CASSETTE" },
                    { 6L, "Alloy", 1L, "32T", "", "CHAINRINGS" },
                    { 7L, "SRAM", 1L, "", "XX1 Eagle AXS", "REAR DERAILLEUR" },
                    { 25L, "Specialized", 7L, "XC Geometry, Rider-First Engineered™, threaded BB, 12x148mm rear spacing, internal cable routing, 100mm of travel", "S-Works FACT 12m", "SEAT BINDER" },
                    { 26L, "SRAM", 1L, "12-speed", "GX Eagle", "CHAIN" },
                    { 28L, "SRAM", 1L, "12-speed, 10-50t", "XG-1295 Eagle", "CASSETTE" },
                    { 8L, "RockShox", 2L, "top-adjust Brain Fade, tapered carbon crown and steerer, 15x110mm Maxle® Stealth thru-axle, 42mm offset, 100mm of travel", "SID Brain Ultimate", "FORK" },
                    { 9L, "RockShox", 2L, "AUTOSAG, 51x257mm", "Custom Micro Brain shock w/ Spike Valve", "REAR SHOCK" },
                    { 29L, "RockShox", 2L, "Position Sensitive, top-adjust Brain Fade, 15x110mm Maxle® Stealth thru-axle, 42mm offset, 100mm of travel", "SID Brain 29", "FORK" },
                    { 10L, "Roval", 3L, "sealed cartridge bearings, 15mm thru-axle, 110mm spacing, 24h", "Control SL", "FRONT HUB" },
                    { 11L, "Roval", 3L, "DT Swiss Star Ratchet, 54t engagement, SRAM XD driver body, 12mm thru-axle, 148mm spacing, 28h", "Control SL", "REAR HUB" },
                    { 12L, "Presta", 3L, "60mm valve", "", "INNER TUBES" },
                    { 27L, "SRAM", 1L, "trigger, 12-speed", "X01 Eagle", "SHIFT LEVERS" },
                    { 38L, "Specialized", 7L, "Alloy, 34.9mm", "", "SEAT BINDER" }
                });

            migrationBuilder.InsertData(
                table: "BikeJunction",
                columns: new[] { "BJId", "BId", "BJColor", "BJSize" },
                values: new object[,]
                {
                    { 1L, 1L, 1, 2 },
                    { 2L, 2L, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "BikeSpecJunction",
                columns: new[] { "BSJId", "BSJBikeId", "BSJSpecId", "BSJSpecificationSpecId" },
                values: new object[,]
                {
                    { 47L, 2L, 37L, null },
                    { 27L, 2L, 2L, null },
                    { 28L, 2L, 3L, null },
                    { 29L, 2L, 27L, null },
                    { 30L, 2L, 28L, null },
                    { 31L, 2L, 6L, null },
                    { 32L, 2L, 7L, null },
                    { 33L, 2L, 29L, null },
                    { 34L, 2L, 9L, null },
                    { 35L, 2L, 30L, null },
                    { 48L, 2L, 23L, null },
                    { 36L, 2L, 31L, null },
                    { 38L, 2L, 33L, null },
                    { 39L, 2L, 14L, null },
                    { 40L, 2L, 15L, null },
                    { 41L, 2L, 16L, null },
                    { 42L, 2L, 34L, null },
                    { 43L, 2L, 35L, null },
                    { 44L, 2L, 36L, null },
                    { 45L, 2L, 20L, null },
                    { 46L, 2L, 21L, null },
                    { 26L, 2L, 26L, null },
                    { 37L, 2L, 32L, null },
                    { 25L, 1L, 25L, null },
                    { 24L, 1L, 24L, null },
                    { 1L, 1L, 1L, null },
                    { 2L, 1L, 2L, null },
                    { 3L, 1L, 3L, null },
                    { 4L, 1L, 4L, null },
                    { 5L, 1L, 5L, null },
                    { 6L, 1L, 6L, null },
                    { 7L, 1L, 7L, null },
                    { 8L, 1L, 8L, null },
                    { 9L, 1L, 9L, null },
                    { 10L, 1L, 10L, null },
                    { 11L, 1L, 11L, null },
                    { 12L, 1L, 12L, null },
                    { 13L, 1L, 13L, null },
                    { 14L, 1L, 14L, null },
                    { 15L, 1L, 15L, null },
                    { 16L, 1L, 16L, null },
                    { 17L, 1L, 17L, null },
                    { 18L, 1L, 18L, null },
                    { 19L, 1L, 19L, null },
                    { 20L, 1L, 20L, null },
                    { 21L, 1L, 21L, null },
                    { 22L, 1L, 22L, null },
                    { 23L, 1L, 23L, null },
                    { 49L, 2L, 24L, null },
                    { 50L, 2L, 38L, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BikeJunction_BId",
                table: "BikeJunction",
                column: "BId");

            migrationBuilder.CreateIndex(
                name: "IX_Bikes_BCategoryId",
                table: "Bikes",
                column: "BCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Bikes_BImagesSIId",
                table: "Bikes",
                column: "BImagesSIId");

            migrationBuilder.CreateIndex(
                name: "IX_BikeSpecJunction_BSJBikeId",
                table: "BikeSpecJunction",
                column: "BSJBikeId");

            migrationBuilder.CreateIndex(
                name: "IX_BikeSpecJunction_BSJSpecificationSpecId",
                table: "BikeSpecJunction",
                column: "BSJSpecificationSpecId");

            migrationBuilder.CreateIndex(
                name: "IX_ImgContent_StoreImagesSIId",
                table: "ImgContent",
                column: "StoreImagesSIId");

            migrationBuilder.CreateIndex(
                name: "IX_Specification_SpecCatId",
                table: "Specification",
                column: "SpecCatId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BikeJunction");

            migrationBuilder.DropTable(
                name: "BikeSpecJunction");

            migrationBuilder.DropTable(
                name: "ImgContent");

            migrationBuilder.DropTable(
                name: "Bikes");

            migrationBuilder.DropTable(
                name: "Specification");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "StoreImages");

            migrationBuilder.DropTable(
                name: "SpecificationCategory");
        }
    }
}
