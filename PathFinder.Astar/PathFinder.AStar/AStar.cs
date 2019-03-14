using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PathFinder.AStar.SettlersEngine;
using PathFinder.Scene;

namespace PathFinder.AStar
{
    public class AStar
    {
        public class PathNode : IPathNode<Object>
        {
            public Int32 X { get; set; }
            public Int32 Y { get; set; }
            public Boolean IsWall { get; set; }

            public bool IsWalkable(Object unused)
            {
                return !IsWall;
            }

            public bool IsWalkable(object inContext, IPathNode<object> centernode)
            {
                throw new NotImplementedException();
            }

            public object UserContext()
            {
                throw new Exception("invalid call");
            }
        }

        #region solvers
        private static Solver<Connection, Object> _astarsolver;
        public static Solver<Connection, Object> AstarSolver
        {
            get
            {
                try
                {
                    if (_astarsolver != null)
                    {
                        return _astarsolver;
                    }
                    throw new NullReferenceException("Solver");
                }
                catch (Exception e)
                {

                    throw new SolverException("Retrival of the Solver caused ",e); 
                }
               
                
            }
            set
            {
                _astarsolver = value;
            }
        }
        private static RelationalSolver<Connection, Object> _relastarsolver;
        public static RelationalSolver<Connection, Object> RelationalAstarSolver
        {
            get
            {
                try
                {
                    if (_relastarsolver != null)
                    {
                        return _relastarsolver;
                    }
                    throw new NullReferenceException("Realtional Solver");
                }
                catch (Exception e)
                {

                    throw new SolverException("Retrival of the Solver caused ", e);
                }
               
            }
            set
            {
                _relastarsolver = value;
            }
        }
        #endregion

        public class RelationalSolver<TPathNode, TUserContext> : RelationalSpatitalAstar<TPathNode, TUserContext> where TPathNode : IPathNode<TUserContext>
        {
            protected override Double Heuristic(PathNode inStart, PathNode inEnd)
            {
                return Math.Abs(inStart.X - inEnd.X) + Math.Abs(inStart.Y - inEnd.Y);
            }

            protected override Double NeighborDistance(PathNode inStart, PathNode inEnd)
            {
                return Heuristic(inStart, inEnd);
            }

            public RelationalSolver(TPathNode[,] inGrid)
                : base(inGrid)
            {
            }
        }
        public class Solver<TPathNode, TUserContext> : SpatialAStar<TPathNode, TUserContext> where TPathNode : IPathNode<TUserContext>
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
