using PathFinder;
using PathFinder._2D;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TotL.Labyrinth;

namespace TotL.Units
{
    class Unit : _2DGraphicsElement
    {
        public int CoordinateX { get; set; }
        public int CoordinateY { get; set; }
        public bool hasnavcoordinate { get; set; }
        private Vector2 _navcoordinate;
        public Vector2 navcoordinate
        {
            get
            {
                return _navcoordinate;
            }
            set
            {
                hasnavcoordinate = true;
                _navcoordinate = value;
            }
        }
        private Vector2 _target;
        protected Vector2 navcoordinate2;
        protected bool relocation;
        public bool hasTarget { get; set; }
        public bool haspath { get; set; }
        public Vector2 target
        {
            get
            {
                return _target;
            }
            set
            {
                hasTarget = true;
                haspath = false;
                _target = value;
            }
        }
        private LinkedList<Connection> _path;
        protected LinkedList<Connection> path
        {
            get {
                return _path;
            }
            set
            {
                _path = value;
                haspath = true;
            }
        }
        public short HP;

        public Unit(int coordinateX, int coordinateY)
        {
            this.CoordinateX = coordinateX;
            this.CoordinateY = coordinateY;
        }

        public Unit(int coordinateX, int coordinateY, Vector2 targetcoordinate)
        {
            this.CoordinateX = coordinateX;
            this.CoordinateY = coordinateY;
        }

        public virtual void damageUnit(short dmg)
        {
            throw new InvalidCallException("hívás a unit alap fügvényre");
        }
        public override void draw()
        {
            throw new InvalidCallException("hívás a unit alap fügvényre");
        }

        public override void Initialize()
        {
            throw new InvalidCallException("hívás a unit alap fügvényre");
        }
        public override void Load()
        {
            throw new InvalidCallException("hívás a unit alap fügvényre");
        }

        public override void update()
        {
            throw new InvalidCallException("Unit nem fogható meg mint 2DGraphicsElement");
        }
        public virtual void update(Cell[,] connect)
        {
            throw new InvalidCallException("hívás a unit alap fügvényre");
        }
    }
}
