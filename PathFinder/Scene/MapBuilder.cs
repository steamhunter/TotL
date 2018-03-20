using PathFinder;
using PathFinder.Debug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.Scene
{
    public abstract class MapBuilder
    {
       
        public Connection[,] connect;
        public TerrainTile[,] map;

        public MapBuilder(Connection[,] connect, TerrainTile[,] map)
        {
            this.connect = connect;
            this.map = map;
        }

        public abstract void Build();
        

        protected void GenerateBorder(Connection[,] connect)
        {
            for (int s = 0; s < 18; s++)
            {
                for (int o = 0; o < 27; o++)
                {
                    if (s == 0 || o == 0 || o == 26 || s == 16)
                    {
                        connect[o, s] = new Connection(o, s);
                        connect[o, s].up = false;
                        connect[o, s].down = false;
                        connect[o, s].left = false;
                        connect[o, s].right = false;
                        connect[o, s].closedsides = 4;
                    }
                    else
                    {
                        connect[o, s] = new Connection(o, s);
                        connect[o, s].closedsides = 0;
                        connect[o, s].up = true; //map[i - 1, j - 1].up;
                        connect[o, s].down = true; //map[i - 1, j - 1].down;
                        connect[o, s].left = true;// map[i - 1, j - 1].left;
                        connect[o, s].right = true;//map[i - 1, j - 1].right;
                        connect[o, s].IsPopulated = false;
                    }
                }
            }
        }
    }
}
