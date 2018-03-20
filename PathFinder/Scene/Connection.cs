using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFinder;


namespace PathFinder.Scene
{
    public class Connection : IConnections, IPathNode<Object>,IProcGeneralable
    {
        private bool _up, _down, _left, _right;
        private int _closedsides;
        private float _rotation;
        private bool _ispopulated;
        public Connection(int x, int y)
        {
            IsPopulated = false;
            _rotation = 0f;
            X = x;
            Y = y;
        }
        public static int getClosedSides(Connection top, Connection right, Connection bottom, Connection left)
        {
            int closedsides = 0;
            if (top.IsPopulated && top.down == false)
            {
                closedsides++;
            }
            if (right.IsPopulated && right.left == false)
            {
                closedsides++;
            }
            if (left.IsPopulated && left.right == false)
            {
                closedsides++;
            }
            if (bottom.IsPopulated && bottom.up == false)
            {
                closedsides++;
            }
            return closedsides;
        }
        public static bool isFiting(TerrainTile cell, Connection top, Connection right, Connection bottom, Connection left)
        {
            if (cell.up == top.down || !top.IsPopulated)
            {
                if (cell.right == right.left || !right.IsPopulated)
                {
                    if (cell.down == bottom.up || !bottom.IsPopulated)
                    {
                        if (cell.left == left.right || !left.IsPopulated)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }



        public bool down
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

        public bool left
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

        public bool right
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

        public float rotation
        {
            get
            {
                return _rotation;
            }

            set
            {
                _rotation = value;
            }
        }

        public bool up
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

        public int closedsides
        {
            get
            {
                return _closedsides;
            }

            set
            {
                _closedsides = value;
            }
        }

        public bool IsPopulated
        {
            get
            {
                return _ispopulated;
            }

            set
            {
                _ispopulated = value;
            }
        }

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

        public bool IsWalkable(object inContext, IPathNode<object> centernode)
        {



            Connection centerConnection = centernode.UserContext() as Connection;
            if (Y == centernode.Y && X == centernode.X)
            {
                return closedsides != 4;
            }
            if (Y < centernode.Y && X == centernode.X)
            {
                return centerConnection.up && down;
            }
            if (Y == centernode.Y && X < centernode.X)
            {
                return centerConnection.left && right;
            }
            if (Y == centernode.Y && X > centernode.X)
            {
                return centerConnection.right && left;
            }
            if (Y > centernode.Y && X == centernode.X)
            {
                return centerConnection.down && up;
            }

            throw new Exception("Invalid path is open check at: start " + centerConnection.Y + " " + centerConnection.X + Environment.NewLine + "with context " + Y + " " + X);
        }

        public object UserContext()
        {
            throw new Exception("ivalid call on connection");
        }
    }



}
