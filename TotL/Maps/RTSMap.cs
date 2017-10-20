using PathFinder.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Toolkit;
using TotL.MapObjects;
using TotL.UI;

namespace TotL.Maps
{
    class RTSMap : Map
    {
        TerrainTile[,] terrain = new TerrainTile[200, 200];
        List<UIElement> UI = new List<UIElement>();
        public override void Draw(GameTime gameTime)
        {
            foreach (var item in terrain)
            {
                item.Draw(gameTime);
            }
        }
        
        public override void Initialize()
        {
            for (int s = 0; s < 200; s++)
            {
                for (int o = 0; o < 200; o++)
                {
                    terrain[o, s] = new TerrainTile(o, s, Terrains.grass);
                }
            }
        }

        public override void LoadContent()
        {
            foreach (var item in terrain)
            {
                item.LoadContent();
            }
        }

        public override void UnloadContent()
        {
           
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var item in terrain)
            {
                item.Update(gameTime);
            }
        }
    }
}
