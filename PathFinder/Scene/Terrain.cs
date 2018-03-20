using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Toolkit;

namespace PathFinder.Scene
{
    public abstract class Terrain : IGameObject
    {
        public TerrainTile[,] map;
        public abstract void Draw(GameTime gameTime);
        public abstract void Initialize();
        public abstract void LoadContent();
        public abstract void UnloadContent();
        public abstract void Update(GameTime gameTime);
    }
}
