using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Data.Entities
{
    //public class 
    public enum Color
    {
        [Description("Gloss Dove/Grey")]
        GlossDoveGrey = 1,
        [Description("Blue/Metallic")]
        BlueMetallic,
        [Description("Satin Carbon/Silver Holographic")]
        SatinCarbonSilverHolographic,
        [Description("Gloss Metallic/Crimson/RocketRed")]
        GlossMetallicCrimsonRocketRed,
        [Description("Satin Carbon/Tarmac Black")]
        SatinCarbonTarmacBlack
    }

    public enum Size
    {
        S = 1,
        M,
        L,
        Xl
    }
}
