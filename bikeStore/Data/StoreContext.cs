
using bikeStore.Data.Entities;
using BikeStore.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace bikeStore.Data
{
    public class StoreDbContext : DbContext
    {
        public DbSet<Bike> Bikes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Specification> Specifications { get; set; }

        #region Data for Seed
        // for seed data only
        
        private readonly List<Category> _dummyCategories = new List<Category> {
            new Category{ CatId = 1, CatName = "Mountain", IsCategoryActive = true},
            new Category{ CatId = 2, CatName = "Road", IsCategoryActive = true},
            new Category{ CatId = 3, CatName = "Active", IsCategoryActive = true},
            new Category{ CatId = 4, CatName = "Electric", IsCategoryActive = true},

            new Category{ CatId = 5, MainCatId = 1, CatName = "Cross Country", IsCategoryActive = true},
            new Category{ CatId = 6, MainCatId = 1, CatName = "Trail", IsCategoryActive = true},
            new Category{ CatId = 7, MainCatId = 1, CatName = "Downhill", IsCategoryActive = true},
            new Category{ CatId = 8, MainCatId = 1, CatName = "Dirt Jump", IsCategoryActive = true},

            new Category{ CatId = 9, MainCatId = 2, CatName = "Performance", IsCategoryActive = true},
            new Category{ CatId = 10, MainCatId = 2, CatName = "Adventure & Gravel", IsCategoryActive = true},
            new Category{ CatId = 11, MainCatId = 2, CatName = "Cyclocross", IsCategoryActive = true},
            new Category{ CatId = 12, MainCatId = 2, CatName = "Triathlon", IsCategoryActive = true},

            new Category{ CatId = 13, MainCatId = 3, CatName = "Fitness", IsCategoryActive = true},
            new Category{ CatId = 14, MainCatId = 3, CatName = "Transport", IsCategoryActive = true},
            new Category{ CatId = 15, MainCatId = 3, CatName = "Comfort", IsCategoryActive = true},

            new Category{ CatId = 16, MainCatId = 4, CatName = "Road", IsCategoryActive = true},
            new Category{ CatId = 17, MainCatId = 4, CatName = "Mountain", IsCategoryActive = true},
            new Category{ CatId = 18, MainCatId = 4, CatName = "Active", IsCategoryActive = true}
            };

        private readonly List<SpecificationCategory> _dummySpecCategories = new List<SpecificationCategory>
        {
            new SpecificationCategory { SpecCatId = 1, SpecCatName = "Drivetrain", IsSpecCatActive = true},
            new SpecificationCategory { SpecCatId = 2, SpecCatName = "Suspension", IsSpecCatActive = true},
            new SpecificationCategory { SpecCatId = 3, SpecCatName = "Wheels", IsSpecCatActive = true},
            new SpecificationCategory { SpecCatId = 4, SpecCatName = "Cockpit", IsSpecCatActive = true},
            new SpecificationCategory { SpecCatId = 5, SpecCatName = "Brakes", IsSpecCatActive = true},
            new SpecificationCategory { SpecCatId = 6, SpecCatName = "Accessories", IsSpecCatActive = true},
            new SpecificationCategory { SpecCatId = 7, SpecCatName = "Frameset", IsSpecCatActive = true}
        };

        private readonly List<Specification> _dummySpecifications = new List<Specification>
        {
             new Specification { SpecId = 1, SpecCatId = 1, Brand = "SRAM", Type = "CHAIN", Model="XX1" , Description = "(Rainbow)"},
             new Specification { SpecId = 2, SpecCatId = 1, Brand = "SRAM", Type = "BOTTOM BRACKET", Model="DUB",Description = "threaded BB" },
             new Specification { SpecId = 3, SpecCatId = 1, Brand = "QUARQ", Type = "CRANKSET", Model="XX1 Eagle Power Meter",Description = "Boost™ 148, DUB, 170/175mm" },
             new Specification { SpecId = 4, SpecCatId = 1, Brand = "SRAM", Type = "SHIFT LEVERS", Model="XX1 Eagle AXS",Description = "trigger, 12-speed" },
             new Specification { SpecId = 5, SpecCatId = 1, Brand = "SRAM", Type = "CASSETTE", Model="XG-1299 Eagle",Description = "10-50t" },
             new Specification { SpecId = 6, SpecCatId = 1, Brand = "Alloy", Type = "CHAINRINGS", Model="",Description = "32T" },
             new Specification { SpecId = 7, SpecCatId = 1, Brand = "SRAM", Type = "REAR DERAILLEUR", Model="XX1 Eagle AXS",Description = "" },
             new Specification { SpecId = 8, SpecCatId = 2, Brand = "RockShox", Type = "FORK", Model="SID Brain Ultimate",Description = "top-adjust Brain Fade, tapered carbon crown and steerer, 15x110mm Maxle® Stealth thru-axle, 42mm offset, 100mm of travel" },
             new Specification { SpecId = 9, SpecCatId = 2, Brand = "RockShox", Type = "REAR SHOCK", Model="Custom Micro Brain shock w/ Spike Valve",Description = "AUTOSAG, 51x257mm" },
             new Specification { SpecId = 10, SpecCatId = 3, Brand = "Roval", Type = "FRONT HUB", Model="Control SL", Description = "sealed cartridge bearings, 15mm thru-axle, 110mm spacing, 24h"  },
             new Specification { SpecId = 11, SpecCatId = 3, Brand = "Roval", Type = "REAR HUB", Model = "Control SL", Description = "DT Swiss Star Ratchet, 54t engagement, SRAM XD driver body, 12mm thru-axle, 148mm spacing, 28h" },
             new Specification { SpecId = 12, SpecCatId = 3, Brand = "Presta", Type = "INNER TUBES", Model = "", Description = "60mm valve"},
             new Specification { SpecId = 13, SpecCatId = 3, Brand = "DT Swiss", Type = "SPOKES", Model = "", Description = "Competition Race"},
             new Specification { SpecId = 14, SpecCatId = 3, Brand = "Roval", Type = "RIMS", Model = "Control SL", Description = "hookless carbon, 25mm internal width, tubeless-ready, hand-built"},
             new Specification { SpecId = 15, SpecCatId = 3, Brand = "Specialized", Type = "FRONT TIRE", Model = "Fast Trak", Description = "Control casing, GRIPTON® compound, 60 TPI, 2Bliss Ready, 29x2.3"},
             new Specification { SpecId = 16, SpecCatId = 3, Brand = "Specialized", Type = "REAR TIRE", Model = "Fast Trak", Description = "Control casing, GRIPTON® compound, 60 TPI, 2Bliss Ready, 29x2.3"},
             new Specification { SpecId = 17, SpecCatId = 4, Brand = "Specialized", Type = "SADDLE", Model = "Body Geometry S-Works Power", Description = "carbon fiber rails, carbon fiber base, 143mm"},
             new Specification { SpecId = 18, SpecCatId = 4, Brand = "Specialized", Type = "SEATPOST", Model = "S-Works FACT carbon", Description = "10mm setback, 30.9mm"},
             new Specification { SpecId = 19, SpecCatId = 4, Brand = "Specialized", Type = "STEM", Model = "S-Works SL", Description = "alloy, titanium bolts, 6-degree rise"},
             new Specification { SpecId = 20, SpecCatId = 4, Brand = "Specialized", Type = "HANDLEBARS", Model = "S-Works Carbon XC Mini Rise", Description = "6-degree upsweep, 8-degree backsweep, 10mm rise, 760mm, 31.8mm"},
             new Specification { SpecId = 21, SpecCatId = 4, Brand = "Specialized", Type = "GRIPS", Model = "Trail Grips", Description = ""},
             new Specification { SpecId = 22, SpecCatId = 5, Brand = "SRAM", Type = "FRONT BRAKE", Model = "Level Ultimate", Description = "hydraulic disc"},
             new Specification { SpecId = 23, SpecCatId = 5, Brand = "SRAM", Type = "REAR BRAKE", Model = "Level Ultimate", Description = "hydraulic disc"},
             new Specification { SpecId = 24, SpecCatId = 6, Brand = "Specialized", Type = "PEDALS", Model = "Dirt", Description = ""},
             new Specification { SpecId = 25, SpecCatId = 7, Brand = "Specialized", Type = "SEAT BINDER", Model = "S-Works FACT 12m", Description = "XC Geometry, Rider-First Engineered™, threaded BB, 12x148mm rear spacing, internal cable routing, 100mm of travel"},

             new Specification { SpecId = 26, SpecCatId = 1, Brand = "SRAM", Type = "CHAIN", Model="GX Eagle" , Description = "12-speed"},
             new Specification { SpecId = 27, SpecCatId = 1, Brand = "SRAM", Type = "SHIFT LEVERS", Model="X01 Eagle",Description = "trigger, 12-speed" },
             new Specification { SpecId = 28, SpecCatId = 1, Brand = "SRAM", Type = "CASSETTE", Model="XG-1295 Eagle",Description = "12-speed, 10-50t" },
             new Specification { SpecId = 29, SpecCatId = 2, Brand = "RockShox", Type = "FORK", Model="SID Brain 29",Description = "Position Sensitive, top-adjust Brain Fade, 15x110mm Maxle® Stealth thru-axle, 42mm offset, 100mm of travel" },
             new Specification { SpecId = 30, SpecCatId = 3, Brand = "Specialized", Type = "FRONT HUB", Model="", Description = "sealed cartridge bearings, 15x110mm spacing, 28h"  },
             new Specification { SpecId = 31, SpecCatId = 3, Brand = "DT Swiss", Type = "REAR HUB", Model = "350", Description = "Star Ratchet, 36t engagement, SRAM XD driver body, 12mm thru-axle, 148mm spacing, 28h" },
             new Specification { SpecId = 32, SpecCatId = 3, Brand = "DT Swiss", Type = "SPOKES", Model = "Industry", Description = ""},
             new Specification { SpecId = 33, SpecCatId = 3, Brand = "Roval", Type = "RIMS", Model = "Control Carbon", Description = "25mm internal width, tubeless-ready"},
             new Specification { SpecId = 34, SpecCatId = 4, Brand = "Specialized", Type = "SADDLE", Model = "Body Geometry Power", Description = "Hollow Titanium rails, 143mm"},
             new Specification { SpecId = 35, SpecCatId = 4, Brand = "Specialized", Type = "SEATPOST", Model = "Carbon", Description = " single-bolt, 30.9mm"},
             new Specification { SpecId = 36, SpecCatId = 4, Brand = "Specialized", Type = "STEM", Model = "XC", Description = "3D-forged alloy, 4-bolt, 6-degree rise"},
             new Specification { SpecId = 37, SpecCatId = 5, Brand = "SRAM", Type = "FRONT BRAKE", Model = "Level TLM", Description = "hydraulic disc"},
             new Specification { SpecId = 38, SpecCatId = 7, Brand = "Specialized", Type = "SEAT BINDER", Model = "", Description = "Alloy, 34.9mm"}

             //new Specification { SpecId = 11, SpecCategoryId = 3, SpecBrand = "", SpecType = "", SpecModel = "", SpecDescription = ""}
        };

        private readonly List<Bike> _dummyBikes = new List<Bike>
        {
            new Bike { BikeId = 1, Brand = "Specialized", CategoryId = 5, Model = "S-Works Epic AXS", IsInStock = true,  Price = 1299.6M},
            new Bike { BikeId = 2, Brand = "Specialized", CategoryId = 5, Model = "Epic Pro", IsInStock = true, Price = 899.5M}
        };

        //private readonly List<BikeJunction> _dummyBikeJunctions = new List<BikeJunction>
        //{
        //    new BikeJunction { BJId = 1, BId = 1, BJColor = Color.GlossDoveGrey , BJSize = Size.M },
        //    new BikeJunction { BJId = 2, BId = 2, BJColor = Color.BlueMetallic , BJSize = Size.M }
        //};

        private readonly List<BikesSpecifications> _dummyBikeSpecJunctions = new List<BikesSpecifications>
        {
            new BikesSpecifications{ Id = 1, BikeId = 1, SpecificationId = 1},
            new BikesSpecifications{ Id = 2, BikeId = 1, SpecificationId = 2},
            new BikesSpecifications{ Id = 3, BikeId = 1, SpecificationId = 3},
            new BikesSpecifications{ Id = 4, BikeId = 1, SpecificationId = 4},
            new BikesSpecifications{ Id = 5, BikeId = 1, SpecificationId = 5},
            new BikesSpecifications{ Id = 6, BikeId = 1, SpecificationId = 6},
            new BikesSpecifications{ Id = 7, BikeId = 1, SpecificationId = 7},
            new BikesSpecifications{ Id = 8, BikeId = 1, SpecificationId = 8},
            new BikesSpecifications{ Id = 9, BikeId = 1, SpecificationId = 9},
            new BikesSpecifications{ Id = 10, BikeId = 1, SpecificationId = 10},
            new BikesSpecifications{ Id = 11, BikeId = 1, SpecificationId = 11},
            new BikesSpecifications{ Id = 12, BikeId = 1, SpecificationId = 12},
            new BikesSpecifications{ Id = 13, BikeId = 1, SpecificationId = 13},
            new BikesSpecifications{ Id = 14, BikeId = 1, SpecificationId = 14},
            new BikesSpecifications{ Id = 15, BikeId = 1, SpecificationId = 15},
            new BikesSpecifications{ Id = 16, BikeId = 1, SpecificationId = 16},
            new BikesSpecifications{ Id = 17, BikeId = 1, SpecificationId = 17},
            new BikesSpecifications{ Id = 18, BikeId = 1, SpecificationId = 18},
            new BikesSpecifications{ Id = 19, BikeId = 1, SpecificationId = 19},
            new BikesSpecifications{ Id = 20, BikeId = 1, SpecificationId = 20},
            new BikesSpecifications{ Id = 21, BikeId = 1, SpecificationId = 21},
            new BikesSpecifications{ Id = 22, BikeId = 1, SpecificationId = 22},
            new BikesSpecifications{ Id = 23, BikeId = 1, SpecificationId = 23},
            new BikesSpecifications{ Id = 24, BikeId = 1, SpecificationId = 24},
            new BikesSpecifications{ Id = 25, BikeId = 1, SpecificationId = 25},

            new BikesSpecifications{ Id = 26, BikeId = 2, SpecificationId = 26},
            new BikesSpecifications{ Id = 27, BikeId = 2, SpecificationId = 2},
            new BikesSpecifications{ Id = 28, BikeId = 2, SpecificationId = 3},
            new BikesSpecifications{ Id = 29, BikeId = 2, SpecificationId = 27},
            new BikesSpecifications{ Id = 30, BikeId = 2, SpecificationId = 28},
            new BikesSpecifications{ Id = 31, BikeId = 2, SpecificationId = 6},
            new BikesSpecifications{ Id = 32, BikeId = 2, SpecificationId = 7},
            new BikesSpecifications{ Id = 33, BikeId = 2, SpecificationId = 29},
            new BikesSpecifications{ Id = 34, BikeId = 2, SpecificationId = 9},
            new BikesSpecifications{ Id = 35, BikeId = 2, SpecificationId = 30},
            new BikesSpecifications{ Id = 36, BikeId = 2, SpecificationId = 31},
            new BikesSpecifications{ Id = 37, BikeId = 2, SpecificationId = 32},
            new BikesSpecifications{ Id = 38, BikeId = 2, SpecificationId = 33},
            new BikesSpecifications{ Id = 39, BikeId = 2, SpecificationId = 14},
            new BikesSpecifications{ Id = 40, BikeId = 2, SpecificationId = 15},
            new BikesSpecifications{ Id = 41, BikeId = 2, SpecificationId = 16},
            new BikesSpecifications{ Id = 42, BikeId = 2, SpecificationId = 34},
            new BikesSpecifications{ Id = 43, BikeId = 2, SpecificationId = 35},
            new BikesSpecifications{ Id = 44, BikeId = 2, SpecificationId = 36},
            new BikesSpecifications{ Id = 45, BikeId = 2, SpecificationId = 20},
            new BikesSpecifications{ Id = 46, BikeId = 2, SpecificationId = 21},
            new BikesSpecifications{ Id = 47, BikeId = 2, SpecificationId = 37},
            new BikesSpecifications{ Id = 48, BikeId = 2, SpecificationId = 23},
            new BikesSpecifications{ Id = 49, BikeId = 2, SpecificationId = 24},
            new BikesSpecifications{ Id = 50, BikeId = 2, SpecificationId = 38}
        };



        #endregion

        public StoreDbContext(DbContextOptions<StoreDbContext> contextOptions) : base(contextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Bike>()
                   .Property(p => p.Price)
                   .HasColumnType("decimal(18,2)");
            builder.Entity<BikesSpecifications>().HasKey(s => new { s.BikeId, s.SpecificationId });
            builder.Entity<BikesColorSize>().HasKey(c => new { c.BikeId, c.ColorId, c.SizeId });
            // if no data, seed db with dummy data

            builder.Entity<Category>().HasData(_dummyCategories);
            builder.Entity<SpecificationCategory>().HasData(_dummySpecCategories);
            builder.Entity<Specification>().HasData(_dummySpecifications);
            builder.Entity<Bike>().HasData(_dummyBikes);
            builder.Entity<BikesSpecifications>().HasData(_dummyBikeSpecJunctions);
            //builder.Entity<BikeJunction>().HasData(_dummyBikeJunctions);

            base.OnModelCreating(builder);
        }
    }
}
