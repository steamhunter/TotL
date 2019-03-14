using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFinder;


namespace PathFinder.Scene
{
    public class Connection : IConnections, IPathNode<Object>,IProcedualyGenerateable
    {
        private bool _up;
        bool _down;
        bool _left;
        bool _right;

        public Connection(int x, int y)
        {
            IsPopulated = false;
            Rotation = 0f;
            X = x;
            Y = y;
        }
        public static int GetClosedSides(Connection top, Connection right, Connection bottom, Connection left)
        {
            int closedSidesCount = 0;
            if (top.IsPopulated && top.Down == false)
            {
                closedSidesCount++;
            }
            if (right.IsPopulated && right.Left == false)
            {
                closedSidesCount++;
            }
            if (left.IsPopulated && left.Right == false)
            {
                closedSidesCount++;
            }
            if (bottom.IsPopulated && bottom.Up == false)
            {
                closedSidesCount++;
            }
            return closedSidesCount;
        }
        public static bool IsFiting(TerrainTile cell, Connection top, Connection right, Connection bottom, Connection left)
        {
            if (cell.Up == top.Down || !top.IsPopulated)
            {
                if (cell.Right == right.Left || !right.IsPopulated)
                {
                    if (cell.Down == bottom.Up || !bottom.IsPopulated)
                    {
                        if (cell.Left == left.Right || !left.IsPopulated)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }



        public bool Down
        {
            get
            {
                return _down;
            }

            set
            {
                IsPopulated = true;
                _down = value;
            }
        }

        public bool Left
        {
            get
            {
                return _left;
            }

            set
            {
                IsPopulated = true;
                _left = value;
            }
        }

        public bool Right
        {
            get
            {
                return _right;
            }

            set
            {
                IsPopulated = true;
                _right = value;
            }
        }

        public float Rotation { get; set; }

        public bool Up
        {
            get
            {
                return _up;
            }

            set
            {
                IsPopulated = true;
                _up = value;
            }
        }

        public int ClosedSides { get; set; }

        public bool IsPopulated { get; set; }

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

        public void SetRotation(float rotation)
        {
            throw new InvalidCallException("setRotation nem hívható a connection osztályon");
        }



        public bool IsWalkable(object inContext)
        {
            //      [ , ][<,0][ , ]
            //      [0,<][0,0][0,>]
            //      [ , ][>,0][ , ]


            throw new Exception(" hívás a híbás paraméterü változatra");
        }

        public bool IsWalkable(object inContext, IPathNode<object> centerNode)
        {



            Connection centerConnection = centerNode.UserContext() as Connection;
            if (Y == centerNode.Y && X == centerNode.X)
            {
                return ClosedSides != 4;
            }
            if (Y < centerNode.Y && X == centerNode.X)
            {
                return centerConnection.Up && Down;
            }
            if (Y == centerNode.Y && X < centerNode.X)
            {
                return centerConnection.Left && Right;
            }
            if (Y == centerNode.Y && X > centerNode.X)
            {
                return centerConnection.Right && Left;
            }
            if (Y > centerNode.Y && X == centerNode.X)
            {
                return centerConnection.Down && Up;
            }

            throw new Exception("Invalid path is open check at: start " + centerConnection.Y + " " + centerConnection.X + Environment.NewLine + "with context " + Y + " " + X);
        }

        public object UserContext()
        {
            throw new Exception("ivalid call on connection");
        }
    }



}
