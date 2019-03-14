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
        public Connection[,] Connect { get; set; }
        public TerrainTile[,] Map { get; set; }

        public MapBuilder(Connection[,] connect, TerrainTile[,] map)
        {
            this.Connect = connect;
            this.Map = map;
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
                        connect[o, s] = new Connection(o, s)
                        {
                            Up = false,
                            Down = false,
                            Left = false,
                            Right = false,
                            ClosedSides = 4
                        };
                    }
                    else
                    {
                        connect[o, s] = new Connection(o, s)
                        {
                            ClosedSides = 0,
                            Up = true, //map[i - 1, j - 1].up;
                            Down = true, //map[i - 1, j - 1].down;
                            Left = true,// map[i - 1, j - 1].left;
                            Right = true,//map[i - 1, j - 1].right;
                            IsPopulated = false
                        };
                    }
                }
            }
        }
    }
}
