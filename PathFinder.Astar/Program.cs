﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Microsoft.Xna.Framework;

namespace PathFinder.Astar
{
    /// <summary>
    /// Author: Roy Triesscheijn (http://www.roy-t.nl)
    /// Small program that demonstrates the use of the PathFinder class
    /// </summary>
    class Program
    {
        /// <summary>
        /// Small benchmark that creates a 10x10x10 world and then tries to find a path from
        /// 0,0,0 to 9,5,8. Benchmark is run 100 times after which benchmark results and the path are shown        
        /// 1 out 3 nodes in the world are blocked
        /// </summary>        
        static void Main(string[] args)
        {
            double[] bench = new double[100];            
            World world = new World(10, 10);

            Random random = new Random();
            
            world.MarkPosition(new Point2D(0, 0), true);
                        
                    
                

            for (int i = 0; i < bench.Length; i++)
            {
                DateTime start = DateTime.Now;
                Astar.FindPath(world, Point2D.Zero, new Point2D(5, 8));
                TimeSpan ts = DateTime.Now - start;
                bench[i] = ts.TotalMilliseconds;
            }

            Console.Out.WriteLine("Total time: " + bench.Sum().ToString() + "ms");
            Console.Out.WriteLine("Average time: " + bench.Sum() / bench.Length + "ms");
            Console.Out.WriteLine("Max: " + bench.Max() + "ms");
            Console.Out.WriteLine("Min: " + bench.Min() + "ms");

            Console.Out.WriteLine("Output: ");
            SearchNode crumb2 = Astar.FindPath(world, Point2D.Zero, new Point2D(5, 8));                                   
            Console.Out.WriteLine("Start: " + crumb2.position.ToString());
            while (crumb2.next != null)
            {
                Console.Out.WriteLine("Route: " + crumb2.next.position.ToString());
                crumb2 = crumb2.next;
            }
            Console.Out.WriteLine("Finished at: " + crumb2.position.ToString());            
            Console.ReadLine();
        }
    }
}
