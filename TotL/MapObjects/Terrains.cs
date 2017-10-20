using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotL.MapObjects
{
    public enum Terrains
    {
        grass
    }
    public static class Terrain
    {
        public static string GetTerrainName(Terrains terrain)
        {
            switch (terrain)
            {
                case Terrains.grass:return "grass";
                default:return null;
            }
        }
    }
}
