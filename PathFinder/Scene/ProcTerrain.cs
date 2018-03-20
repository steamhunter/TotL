using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Toolkit;

namespace PathFinder.Scene
{
    public abstract class ProcTerrain : Terrain
    {
        public ProcTerrain(TerrainTile[,] terrain,Connection[,] connection)
        {
            map = terrain;
            connect = connection;
        }

        public Connection[,] connect;
    }
}
