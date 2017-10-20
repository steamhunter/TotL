using PathFinder._2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Toolkit;

namespace PathFinder.Map
{
    public abstract class MapObject : _2DGraphicsElement, IGameObject
    {
        public int X
        {
            get;


            set;
        }

        public int Y
        {
            get;

            set;
        }
    }
}
