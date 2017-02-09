using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PathFinder.AStar
{
    class AStar
    {
        public class PathNode : SettlersEngine.IPathNode<Object>
        {
            public Int32 X { get; set; }
            public Int32 Y { get; set; }
            public Boolean IsWall { get; set; }

            public bool IsWalkable(Object unused)
            {
                return !IsWall;
            }
        }

        public class Solver<TPathNode, TUserContext> : SettlersEngine.SpatialAStar<TPathNode, TUserContext> where TPathNode : SettlersEngine.IPathNode<TUserContext>
        {
            protected override Double Heuristic(PathNode inStart, PathNode inEnd)
            {
                return Math.Abs(inStart.X - inEnd.X) + Math.Abs(inStart.Y - inEnd.Y);
            }

            protected override Double NeighborDistance(PathNode inStart, PathNode inEnd)
            {
                return Heuristic(inStart, inEnd);
            }

            public Solver(TPathNode[,] inGrid)
                : base(inGrid)
            {
            }
        }
    }
}
