using PathFinder;
using PathFinder._2D;
using SharpDX;
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
    class Cell : _2DGraphicsElement, IConnections
    {
        private float _rotation = 0f;
        protected static float unitSize = (Vars.ScreenWidth * 0.83f) / 25f;
        private bool _up, _right, _left, _down;
        protected float LocationXoffset, LocationYoffset;
        private int _closedsides;
        protected List<RectangleF> _blockedvolumes = new List<RectangleF>();
        private bool _ispopulated=false;

        public Cell(int y,int x)
        {
            X = x;
            Y = y;
        }
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

        public virtual void setRotation(float rotation)
        {
            throw new InvalidCallException("hívás a Cell alap fügvényre" + GetType().ToString());
        }

        public virtual void SetBlockingVolumes()
        {
            throw new InvalidCallException("hívás a Cell alap fügvényre" + GetType().ToString());
        }

        public virtual bool CheckBlockingState(RectangleF location)
        {

            bool colision = false;
            for (int i = 0; i < _blockedvolumes.Count && !colision; i++)
            {
                colision = _blockedvolumes[i].Intersects(location);
            }
            return colision;
        }


        public virtual bool CheckFitting( Connection[,] connect ,int co,int cs,int o,int s)
        {
            throw new PathFinder.InvalidCallException("hívás a Cell alap fügvényre"+this.GetType().ToString());
        }

       
    }
}
