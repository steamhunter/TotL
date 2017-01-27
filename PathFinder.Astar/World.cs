using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PathFinder.Astar
{
    /// <summary>
    /// Author: Roy Triesscheijn (http://www.roy-t.nl)
    /// Sample World class that only provides 'is free or not' information on a node
    /// </summary>
    public class World
    {
        private int sx;
        private int sy;
        //private int sz;
       // private int offsetIdx;
        private bool[,] worldBlocked; //extremely simple world where each node can be free or blocked: true=blocked        

        //Note: we use Y as height and Z as depth here!
        public int Left { get { return 0; } }
        public int Right { get { return sx - 2; } }
        public int Bottom { get { return 0; } }
        public int Top { get { return sy - 2; } }
       

        

        /// <summary>
        /// Creates a 2D world
        /// </summary>        
        public World(int width, int height)
        {
            // add 2 to compensate for the solid border around the world
            sx = width + 2;
            sy = height + 2;
           // sz = depth + 2;
            //offsetIdx = (0 + 1) + ((0 + 1) * sy) * sx;
            worldBlocked = new Boolean[sx , sy];

            // add solid border
            for (int x = 0; x < sx; ++x)
                for (int y = 0; y < sy; ++y)
                {
                    if (x==0||y==0||x==sx-1||y==sy-1)
                    {
                        MarkPositionEx(new Point2D(x, y), true);
                    }
                    
                   // MarkPositionEx(new Point2D(x, y, sz - 1), true);
                }

           /* for (int y = 0; y < sy; ++y)
                for (int z = 0; z < sz; ++z)
                {
                    MarkPositionEx(new Point2D(0, y, z), true);
                    MarkPositionEx(new Point2D(sx - 1, y, z), true);
                }

            for (int z = 0; z < sz; ++z)
                for (int x = 0; x < sx; ++x)
                {
                    MarkPositionEx(new Point2D(x, 0, z), true);
                    MarkPositionEx(new Point2D(x, sy - 1, z), true);
                }*/
        }

        /// <summary>
        /// Mark positions in the world als blocked (true) or unblocked (false)
        /// </summary>
        /// <param name="value">use true if you wan't to block the value</param>
        public void MarkPosition(Point2D position, bool value)
        {
            worldBlocked[position.X , position.Y] = value;
        }

        private void MarkPositionEx(Point2D position, bool value)
        {
            worldBlocked[position.X, position.Y ] = value;
        }

        /// <summary>
        /// Checks if a position is free or marked (and legal)
        /// </summary>        
        /// <returns>true if the position is free</returns>
        public bool PositionIsFree(Point2D position)
        {
            return
                !worldBlocked[position.X , position.Y];
        }
    }
}
