using PathFinder;
using PathFinder._2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotL.labyrinthcells
{
    class Rotaitions
    {
       public static float zero = 0f;
       public static float plus90 = Convert.ToSingle(Math.PI) / 2;
       public static float half = Convert.ToSingle(Math.PI);
       public static float minus90 = -Convert.ToSingle(Math.PI) / 2;
    }
    class Cell : Unit2D, IConnections
    {
        private float _rotation = 0f;
        static float unitSize = (Vars.ScreenWidth * 0.83f) / 25f;
        private bool _up, _right, _left, _down;
        protected float LocationXoffset, LocationYoffset;
        private int _closedsides;
        public bool down
        {
            get
            {
                return _down;
            }
            set
            {
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
                _right = value;
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
                _up = value;
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

        public float locationX {
            get
            {
                return _locationX;
            }
            set
            {
                _locationX = LocationXoffset + value;
            }
        }
        public float locationY
        {
            get
            {
                return _locationY;
            }
            set
            {
                _locationY = LocationYoffset + value;
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

        public virtual void setRotation(float rotation) { }



        public static bool CheckFitting(ref Cell cell, Connection[,] connect ,int co,int cs,int o,int s)
        { 
            if (cell.closedsides >= Connection.getClosedSides(connect[cs - 1, co], connect[cs, co + 1], connect[cs + 1, co], connect[cs, co - 1]))
            {
                if (Connection.isFiting(cell, connect[cs - 1, co], connect[cs, co + 1], connect[cs + 1, co], connect[cs, co - 1]))
                {
                    cell.locationX = 20 + ((o) * unitSize);
                    cell.locationY = 20 + ((s) * unitSize);
                    connect[cs, co].up = cell.up;
                    connect[cs, co].down = cell.down;
                    connect[cs, co].left = cell.left;
                    connect[cs, co].right = cell.right;
                    return true;

                }
                else
                {
                    cell.setRotation(Rotaitions.plus90);
                    if (Connection.isFiting(cell, connect[cs - 1, co], connect[cs, co + 1], connect[cs + 1, co], connect[cs, co - 1]))
                    {
                        cell.locationX = 20 + ((o) * unitSize);
                        cell.locationY = 20 + ((s) * unitSize);
                        connect[cs, co].up = cell.up;
                        connect[cs, co].down = cell.down;
                        connect[cs, co].left = cell.left;
                        connect[cs, co].right = cell.right;
                        return true;
                    }
                    else
                    {
                        cell.setRotation(Rotaitions.half);
                        if (Connection.isFiting(cell, connect[cs - 1, co], connect[cs, co + 1], connect[cs + 1, co], connect[cs, co - 1]))
                        {
                            cell.locationX = 20 + ((o) * unitSize);
                            cell.locationY = 20 + ((s) * unitSize);
                            connect[cs, co].up = cell.up;
                            connect[cs, co].down = cell.down;
                            connect[cs, co].left = cell.left;
                            connect[cs, co].right = cell.right;
                            return true;
                        }
                        else
                        {
                            cell.setRotation(Rotaitions.minus90);
                            if (Connection.isFiting(cell, connect[cs - 1, co], connect[cs, co + 1], connect[cs + 1, co], connect[cs, co - 1]))
                            {
                                cell.locationX = 20 + ((o) * unitSize);
                                cell.locationY = 20 + ((s) * unitSize);
                                connect[cs, co].up = cell.up;
                                connect[cs, co].down = cell.down;
                                connect[cs, co].left = cell.left;
                                connect[cs, co].right = cell.right;
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            else
            {
                return false;
            }
          

        }
    }
}
