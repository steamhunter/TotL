using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFinder;


namespace TotL.Labyrinth
{
    public class Connection : IConnections, IPathNode<Object>
    {
        private int x, y;
        private float _rotation;
        public Connection(int y, int x)
        {
            isPopulated = false;
            _rotation = 0f;
            this.x = x;
            this.y = y;
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
            get;

            set;
        }

        public bool left
        {
            get;

            set;
        }

        public bool right
        {
            get;

            set;
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
            get;

            set;
        }

        public int closedsides
        {
            get;

            set;
        }

        public bool isPopulated
        {
            get;

            set;
        }

        int IConnections.X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        int IConnections.Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }

        bool IConnections.isPopulated
        {
            get;

            set;
        }

        int IConnections.closedsides
        {
            get;

            set;
        }

        bool IConnections.up
        {
            get;

            set;
        }

        bool IConnections.down
        {
            get;

            set;
        }

        bool IConnections.left
        {
            get;

            set;
        }

        bool IConnections.right
        {
            get;

            set;
        }

        float IConnections.rotation
        {
            get;

            set;
        }

        int IPathNode<object>.X
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }

        int IPathNode<object>.Y
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        protected int GetXasPathNode()
        {
            return y;
        }
        protected int GetYasPathNode()
        {
            return x;
        }

        public int GetXasPathFinder()
        {
            return x;
        }
        public int GetYasPathFinder()
        {
            return y;
        }

        void IConnections.setRotation(float rotation)
        {
            throw new InvalidCallException("setRotation nem hívható a connection osztályon");
        }

        bool IPathNode<object>.IsWalkable(object inContext)
        {
            throw new Exception(" hívás a híbás paraméterü változatra");
        }

        bool IPathNode<object>.IsWalkable(object inContext, IPathNode<object> centernode)
        {
            Connection centerConnection = centernode.UserContext() as Connection;
            if (GetYasPathNode() == centernode.Y && GetXasPathNode() == centernode.X)
            {
                return closedsides != 4;
            }
            if (GetYasPathNode() < centernode.Y && GetXasPathNode() == centernode.X)
            {
                return centerConnection.up && down;
            }
            if (GetYasPathNode() == centernode.Y && GetXasPathNode() < centernode.X)
            {
                return centerConnection.left && right;
            }
            if (GetYasPathNode() == centernode.Y && GetXasPathNode() > centernode.X)
            {
                return centerConnection.right && left;
            }
            if (GetYasPathNode() > centernode.Y && GetXasPathNode() == centernode.X)
            {
                return centerConnection.down && up;
            }

            throw new Exception("Invalid path is open check at: start " + GetXasPathNode() + " " + GetYasPathNode() + Environment.NewLine + "with context " + GetXasPathNode() + " " + GetYasPathNode());
        }

        object IPathNode<object>.UserContext()
        {
            throw new Exception("ivalid call on connection");
        }
    }



}






