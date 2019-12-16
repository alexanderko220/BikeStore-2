
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
             new Specification { SpecId = 1, SpecCatId = 1, SpecBrand = "SRAM", SpecType = "CHAIN", SpecModel="XX1" , SpecDescr = "(Rainbow)"},
             new Specification { SpecId = 2, SpecCatId = 1, SpecBrand = "SRAM", SpecType = "BOTTOM BRACKET", SpecModel="DUB",SpecDescr = "threaded BB" },
             new Specification { SpecId = 3, SpecCatId = 1, SpecBrand = "QUARQ", SpecType = "CRANKSET", SpecModel="XX1 Eagle Power Meter",SpecDescr = "Boost™ 148, DUB, 170/175mm" },
             new Specification { SpecId = 4, SpecCatId = 1, SpecBrand = "SRAM", SpecType = "SHIFT LEVERS", SpecModel="XX1 Eagle AXS",SpecDescr = "trigger, 12-speed" },
             new Specification { SpecId = 5, SpecCatId = 1, SpecBrand = "SRAM", SpecType = "CASSETTE", SpecModel="XG-1299 Eagle",SpecDescr = "10-50t" },
             new Specification { SpecId = 6, SpecCatId = 1, SpecBrand = "Alloy", SpecType = "CHAINRINGS", SpecModel="",SpecDescr = "32T" },
             new Specification { SpecId = 7, SpecCatId = 1, SpecBrand = "SRAM", SpecType = "REAR DERAILLEUR", SpecModel="XX1 Eagle AXS",SpecDescr = "" },
             new Specification { SpecId = 8, SpecCatId = 2, SpecBrand = "RockShox", SpecType = "FORK", SpecModel="SID Brain Ultimate",SpecDescr = "top-adjust Brain Fade, tapered carbon crown and steerer, 15x110mm Maxle® Stealth thru-axle, 42mm offset, 100mm of travel" },
             new Specification { SpecId = 9, SpecCatId = 2, SpecBrand = "RockShox", SpecType = "REAR SHOCK", SpecModel="Custom Micro Brain shock w/ Spike Valve",SpecDescr = "AUTOSAG, 51x257mm" },
             new Specification { SpecId = 10, SpecCatId = 3, SpecBrand = "Roval", SpecType = "FRONT HUB", SpecModel="Control SL", SpecDescr = "sealed cartridge bearings, 15mm thru-axle, 110mm spacing, 24h"  },
             new Specification { SpecId = 11, SpecCatId = 3, SpecBrand = "Roval", SpecType = "REAR HUB", SpecModel = "Control SL", SpecDescr = "DT Swiss Star Ratchet, 54t engagement, SRAM XD driver body, 12mm thru-axle, 148mm spacing, 28h" },
             new Specification { SpecId = 12, SpecCatId = 3, SpecBrand = "Presta", SpecType = "INNER TUBES", SpecModel = "", SpecDescr = "60mm valve"},
             new Specification { SpecId = 13, SpecCatId = 3, SpecBrand = "DT Swiss", SpecType = "SPOKES", SpecModel = "", SpecDescr = "Competition Race"},
             new Specification { SpecId = 14, SpecCatId = 3, SpecBrand = "Roval", SpecType = "RIMS", SpecModel = "Control SL", SpecDescr = "hookless carbon, 25mm internal width, tubeless-ready, hand-built"},
             new Specification { SpecId = 15, SpecCatId = 3, SpecBrand = "Specialized", SpecType = "FRONT TIRE", SpecModel = "Fast Trak", SpecDescr = "Control casing, GRIPTON® compound, 60 TPI, 2Bliss Ready, 29x2.3"},
             new Specification { SpecId = 16, SpecCatId = 3, SpecBrand = "Specialized", SpecType = "REAR TIRE", SpecModel = "Fast Trak", SpecDescr = "Control casing, GRIPTON® compound, 60 TPI, 2Bliss Ready, 29x2.3"},
             new Specification { SpecId = 17, SpecCatId = 4, SpecBrand = "Specialized", SpecType = "SADDLE", SpecModel = "Body Geometry S-Works Power", SpecDescr = "carbon fiber rails, carbon fiber base, 143mm"},
             new Specification { SpecId = 18, SpecCatId = 4, SpecBrand = "Specialized", SpecType = "SEATPOST", SpecModel = "S-Works FACT carbon", SpecDescr = "10mm setback, 30.9mm"},
             new Specification { SpecId = 19, SpecCatId = 4, SpecBrand = "Specialized", SpecType = "STEM", SpecModel = "S-Works SL", SpecDescr = "alloy, titanium bolts, 6-degree rise"},
             new Specification { SpecId = 20, SpecCatId = 4, SpecBrand = "Specialized", SpecType = "HANDLEBARS", SpecModel = "S-Works Carbon XC Mini Rise", SpecDescr = "6-degree upsweep, 8-degree backsweep, 10mm rise, 760mm, 31.8mm"},
             new Specification { SpecId = 21, SpecCatId = 4, SpecBrand = "Specialized", SpecType = "GRIPS", SpecModel = "Trail Grips", SpecDescr = ""},
             new Specification { SpecId = 22, SpecCatId = 5, SpecBrand = "SRAM", SpecType = "FRONT BRAKE", SpecModel = "Level Ultimate", SpecDescr = "hydraulic disc"},
             new Specification { SpecId = 23, SpecCatId = 5, SpecBrand = "SRAM", SpecType = "REAR BRAKE", SpecModel = "Level Ultimate", SpecDescr = "hydraulic disc"},
             new Specification { SpecId = 24, SpecCatId = 6, SpecBrand = "Specialized", SpecType = "PEDALS", SpecModel = "Dirt", SpecDescr = ""},
             new Specification { SpecId = 25, SpecCatId = 7, SpecBrand = "Specialized", SpecType = "SEAT BINDER", SpecModel = "S-Works FACT 12m", SpecDescr = "XC Geometry, Rider-First Engineered™, threaded BB, 12x148mm rear spacing, internal cable routing, 100mm of travel"},

             new Specification { SpecId = 26, SpecCatId = 1, SpecBrand = "SRAM", SpecType = "CHAIN", SpecModel="GX Eagle" , SpecDescr = "12-speed"},
             new Specification { SpecId = 27, SpecCatId = 1, SpecBrand = "SRAM", SpecType = "SHIFT LEVERS", SpecModel="X01 Eagle",SpecDescr = "trigger, 12-speed" },
             new Specification { SpecId = 28, SpecCatId = 1, SpecBrand = "SRAM", SpecType = "CASSETTE", SpecModel="XG-1295 Eagle",SpecDescr = "12-speed, 10-50t" },
             new Specification { SpecId = 29, SpecCatId = 2, SpecBrand = "RockShox", SpecType = "FORK", SpecModel="SID Brain 29",SpecDescr = "Position Sensitive, top-adjust Brain Fade, 15x110mm Maxle® Stealth thru-axle, 42mm offset, 100mm of travel" },
             new Specification { SpecId = 30, SpecCatId = 3, SpecBrand = "Specialized", SpecType = "FRONT HUB", SpecModel="", SpecDescr = "sealed cartridge bearings, 15x110mm spacing, 28h"  },
             new Specification { SpecId = 31, SpecCatId = 3, SpecBrand = "DT Swiss", SpecType = "REAR HUB", SpecModel = "350", SpecDescr = "Star Ratchet, 36t engagement, SRAM XD driver body, 12mm thru-axle, 148mm spacing, 28h" },
             new Specification { SpecId = 32, SpecCatId = 3, SpecBrand = "DT Swiss", SpecType = "SPOKES", SpecModel = "Industry", SpecDescr = ""},
             new Specification { SpecId = 33, SpecCatId = 3, SpecBrand = "Roval", SpecType = "RIMS", SpecModel = "Control Carbon", SpecDescr = "25mm internal width, tubeless-ready"},
             new Specification { SpecId = 34, SpecCatId = 4, SpecBrand = "Specialized", SpecType = "SADDLE", SpecModel = "Body Geometry Power", SpecDescr = "Hollow Titanium rails, 143mm"},
             new Specification { SpecId = 35, SpecCatId = 4, SpecBrand = "Specialized", SpecType = "SEATPOST", SpecModel = "Carbon", SpecDescr = " single-bolt, 30.9mm"},
             new Specification { SpecId = 36, SpecCatId = 4, SpecBrand = "Specialized", SpecType = "STEM", SpecModel = "XC", SpecDescr = "3D-forged alloy, 4-bolt, 6-degree rise"},
             new Specification { SpecId = 37, SpecCatId = 5, SpecBrand = "SRAM", SpecType = "FRONT BRAKE", SpecModel = "Level TLM", SpecDescr = "hydraulic disc"},
             new Specification { SpecId = 38, SpecCatId = 7, SpecBrand = "Specialized", SpecType = "SEAT BINDER", SpecModel = "", SpecDescr = "Alloy, 34.9mm"}

             //new Specification { SpecId = 11, SpecCategoryId = 3, SpecBrand = "", SpecType = "", SpecModel = "", SpecDescription = ""}
        };

        private readonly List<Bike> _dummyBikes = new List<Bike>
        {
            new Bike { BId = 1, BBrand = "Specialized", BCategoryId = 5, BModel = "S-Works Epic AXS", IsInStock = true,  BPrice = 1299.6M},
            new Bike { BId = 2, BBrand = "Specialized", BCategoryId = 5, BModel = "Epic Pro", IsInStock = true, BPrice = 899.5M}
        };

        private readonly List<BikeJunction> _dummyBikeJunctions = new List<BikeJunction>
        {
            new BikeJunction { BJId = 1, BId = 1, BJColor = Color.GlossDoveGrey , BJSize = Size.M },
            new BikeJunction { BJId = 2, BId = 2, BJColor = Color.BlueMetallic , BJSize = Size.M }
        };

        private readonly List<BikeSpecJunction> _dummyBikeSpecJunctions = new List<BikeSpecJunction>
        {
            new BikeSpecJunction{ BSJId = 1, BSJBikeId = 1, BSJSpecId = 1},
            new BikeSpecJunction{ BSJId = 2, BSJBikeId = 1, BSJSpecId = 2},
            new BikeSpecJunction{ BSJId = 3, BSJBikeId = 1, BSJSpecId = 3},
            new BikeSpecJunction{ BSJId = 4, BSJBikeId = 1, BSJSpecId = 4},
            new BikeSpecJunction{ BSJId = 5, BSJBikeId = 1, BSJSpecId = 5},
            new BikeSpecJunction{ BSJId = 6, BSJBikeId = 1, BSJSpecId = 6},
            new BikeSpecJunction{ BSJId = 7, BSJBikeId = 1, BSJSpecId = 7},
            new BikeSpecJunction{ BSJId = 8, BSJBikeId = 1, BSJSpecId = 8},
            new BikeSpecJunction{ BSJId = 9, BSJBikeId = 1, BSJSpecId = 9},
            new BikeSpecJunction{ BSJId = 10, BSJBikeId = 1, BSJSpecId = 10},
            new BikeSpecJunction{ BSJId = 11, BSJBikeId = 1, BSJSpecId = 11},
            new BikeSpecJunction{ BSJId = 12, BSJBikeId = 1, BSJSpecId = 12},
            new BikeSpecJunction{ BSJId = 13, BSJBikeId = 1, BSJSpecId = 13},
            new BikeSpecJunction{ BSJId = 14, BSJBikeId = 1, BSJSpecId = 14},
            new BikeSpecJunction{ BSJId = 15, BSJBikeId = 1, BSJSpecId = 15},
            new BikeSpecJunction{ BSJId = 16, BSJBikeId = 1, BSJSpecId = 16},
            new BikeSpecJunction{ BSJId = 17, BSJBikeId = 1, BSJSpecId = 17},
            new BikeSpecJunction{ BSJId = 18, BSJBikeId = 1, BSJSpecId = 18},
            new BikeSpecJunction{ BSJId = 19, BSJBikeId = 1, BSJSpecId = 19},
            new BikeSpecJunction{ BSJId = 20, BSJBikeId = 1, BSJSpecId = 20},
            new BikeSpecJunction{ BSJId = 21, BSJBikeId = 1, BSJSpecId = 21},
            new BikeSpecJunction{ BSJId = 22, BSJBikeId = 1, BSJSpecId = 22},
            new BikeSpecJunction{ BSJId = 23, BSJBikeId = 1, BSJSpecId = 23},
            new BikeSpecJunction{ BSJId = 24, BSJBikeId = 1, BSJSpecId = 24},
            new BikeSpecJunction{ BSJId = 25, BSJBikeId = 1, BSJSpecId = 25},

            new BikeSpecJunction{ BSJId = 26, BSJBikeId = 2, BSJSpecId = 26},
            new BikeSpecJunction{ BSJId = 27, BSJBikeId = 2, BSJSpecId = 2},
            new BikeSpecJunction{ BSJId = 28, BSJBikeId = 2, BSJSpecId = 3},
            new BikeSpecJunction{ BSJId = 29, BSJBikeId = 2, BSJSpecId = 27},
            new BikeSpecJunction{ BSJId = 30, BSJBikeId = 2, BSJSpecId = 28},
            new BikeSpecJunction{ BSJId = 31, BSJBikeId = 2, BSJSpecId = 6},
            new BikeSpecJunction{ BSJId = 32, BSJBikeId = 2, BSJSpecId = 7},
            new BikeSpecJunction{ BSJId = 33, BSJBikeId = 2, BSJSpecId = 29},
            new BikeSpecJunction{ BSJId = 34, BSJBikeId = 2, BSJSpecId = 9},
            new BikeSpecJunction{ BSJId = 35, BSJBikeId = 2, BSJSpecId = 30},
            new BikeSpecJunction{ BSJId = 36, BSJBikeId = 2, BSJSpecId = 31},
            new BikeSpecJunction{ BSJId = 37, BSJBikeId = 2, BSJSpecId = 32},
            new BikeSpecJunction{ BSJId = 38, BSJBikeId = 2, BSJSpecId = 33},
            new BikeSpecJunction{ BSJId = 39, BSJBikeId = 2, BSJSpecId = 14},
            new BikeSpecJunction{ BSJId = 40, BSJBikeId = 2, BSJSpecId = 15},
            new BikeSpecJunction{ BSJId = 41, BSJBikeId = 2, BSJSpecId = 16},
            new BikeSpecJunction{ BSJId = 42, BSJBikeId = 2, BSJSpecId = 34},
            new BikeSpecJunction{ BSJId = 43, BSJBikeId = 2, BSJSpecId = 35},
            new BikeSpecJunction{ BSJId = 44, BSJBikeId = 2, BSJSpecId = 36},
            new BikeSpecJunction{ BSJId = 45, BSJBikeId = 2, BSJSpecId = 20},
            new BikeSpecJunction{ BSJId = 46, BSJBikeId = 2, BSJSpecId = 21},
            new BikeSpecJunction{ BSJId = 47, BSJBikeId = 2, BSJSpecId = 37},
            new BikeSpecJunction{ BSJId = 48, BSJBikeId = 2, BSJSpecId = 23},
            new BikeSpecJunction{ BSJId = 49, BSJBikeId = 2, BSJSpecId = 24},
            new BikeSpecJunction{ BSJId = 50, BSJBikeId = 2, BSJSpecId = 38}
        };



        #endregion

        public StoreDbContext(DbContextOptions<StoreDbContext> contextOptions) : base(contextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Bike>()
                   .Property(p => p.BPrice)
                   .HasColumnType("decimal(18,2)");

            // if no data, seed db with dummy data

            builder.Entity<Category>().HasData(_dummyCategories);
            builder.Entity<SpecificationCategory>().HasData(_dummySpecCategories);
            builder.Entity<Specification>().HasData(_dummySpecifications);
            builder.Entity<Bike>().HasData(_dummyBikes);
            builder.Entity<BikeSpecJunction>().HasData(_dummyBikeSpecJunctions);
            builder.Entity<BikeJunction>().HasData(_dummyBikeJunctions);

            base.OnModelCreating(builder);
        }
    }
}
