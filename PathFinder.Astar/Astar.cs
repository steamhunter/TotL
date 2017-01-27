using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PathFinder.Astar
{    
    /// <summary>
    /// Author: Roy Triesscheijn (http://www.roy-t.nl)
    /// Class providing 3D pathfinding capabilities using A*.
    /// Heaviliy optimized for speed therefore uses slightly more memory
    /// On rare cases finds the 'almost optimal' path instead of the perfect path
    /// this is because we immediately return when we find the exit instead of finishing
    /// 'neighbour' loop.
    /// </summary>
    public static class Astar
    {                   
        /// <summary>
        /// Method that switfly finds the best path from start to end.
        /// </summary>
        /// <returns>The starting breadcrumb traversable via .next to the end or null if there is no path</returns>        
        public static SearchNode FindPath(World world, Point2D start, Point2D end)
        {
            //note we just flip start and end here so you don't have to.            
            return FindPathReversed(world, end, start); 
        }        

        /// <summary>
        /// Method that switfly finds the best path from start to end. Doesn't reverse outcome
        /// </summary>
        /// <returns>The end breadcrump where each .next is a step back)</returns>
        private static SearchNode FindPathReversed(World world, Point2D start, Point2D end)
        {
            SearchNode startNode = new SearchNode(start, 0, 0, null);

            MinHeap openList = new MinHeap();            
            openList.Add(startNode);

            int sx = world.Right;
            int sy = world.Top;

            bool[,] brWorld = new bool[sx , sy];
            brWorld[start.X , start.Y] = true;

            while (openList.HasNext())
            {                
                SearchNode current = openList.ExtractFirst();

               /* if (current.position.EqualsSS(new Point2D(1,1)))
                {
                    return new SearchNode(end, current.pathCost + 1, current.cost + 1, current);
                }*/
                if (Math.Abs(current.position.X-end.X)<=1&&Math.Abs(current.position.Y-end.Y)<=1 )//with math abs==0 it get null not going into it
                {

                    return new SearchNode(end, current.pathCost + 1, current.cost + 1, current);//returns with (1,1) not (1,0)or(0,1)
                }

                
                for (int i = 0; i < surrounding.Length; i++)
                {
                    Surr surr = surrounding[i];
                    Point2D tmp = new Point2D(current.position, surr.Point);

                    if (tmp.X < 10 && tmp.Y < 10 && tmp.X >= 0 && tmp.Y >= 0)
                    {
                        if (world.PositionIsFree(tmp) && brWorld[tmp.X, tmp.Y] == false)
                        {
                            brWorld[tmp.X, tmp.Y] = true;
                            int pathCost = current.pathCost + surr.Cost;
                            int cost = pathCost + tmp.GetDistanceSquared(end);
                            SearchNode node = new SearchNode(tmp, cost, pathCost, current);
                            openList.Add(node);
                        }
                    }
                    
                }
            }
            return null; //no path found
        }

        class Surr
        {
            public Surr(int x, int y)
            {
                Point = new Point2D(x, y);
                Cost = x * x + y * y ;
            }

            public Point2D Point;
            public int Cost;
        }

        //Neighbour options
        private static Surr[] surrounding = new Surr[]{
             new Surr(0,1), new Surr(-1,0), new Surr(1,0), new Surr(0,-1),        
        };
    }           
}
