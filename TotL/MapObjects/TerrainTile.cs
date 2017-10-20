using PathFinder.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Toolkit;
using PathFinder._2D;

namespace TotL.MapObjects
{
    public class TerrainTile:MapObject
    {
        Terrains terrain;
        public TerrainTile(int x,int y,Terrains terrain)
        {
            this.terrain = terrain;
            X = x;
            Y = y;
            _locationX = X;
            _locationY = Y;

        }

        public override void Initialize()
        {
           
        }

        public override void LoadContent()
        {
            texture = TextureLoader.getTexture("transparent"/*Terrain.GetTerrainName(terrain)*/);
        }

        public override void UnloadContent()
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
