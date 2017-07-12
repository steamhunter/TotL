using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFinder;


namespace TotL.Labyrinth
{
    public class Connection : IConnections, IPathNode<Object>,ICell
    {
        private bool _up, _down, _left, _right;
        private int _closedsides;
        private float _rotation;
        private bool _ispopulated;
        public Connection(int x, int y)
        {
            isPopulated = false;
            _rotation = 0f;
            X = x;
            Y = y;
        }
        public static int getClosedSides(Connection top, Connection right, Connection bottom, Connection left)
        {
            int closedsides = 0;
            if (top.isPopulated && top.down == false)
            {
                closedsides++;
            }
            if (right.isPopulated && right.left == false)
            {
                closedsides++;
            }
            if (left.isPopulated && left.right == false)
            {
                closedsides++;
            }
            if (bottom.isPopulated && bottom.up == false)
            {
                closedsides++;
            }
            return closedsides;
        }
        public static bool isFiting(Cell cell, Connection top, Connection right, Connection bottom, Connection left)
        {
            if (cell.up == top.down || !top.isPopulated)
            {
                if (cell.right == right.left || !right.isPopulated)
                {
                    if (cell.down == bottom.up || !bottom.isPopulated)
                    {
                        if (cell.left == left.right || !left.isPopulated)
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
                isPopulated = true;
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
                isPopulated = true;
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
                isPopulated = true;
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
                isPopulated = true;
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

        public bool isPopulated
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

        public void setRotation(float rotation)
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
