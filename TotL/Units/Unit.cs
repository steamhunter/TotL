using PathFinder;
using PathFinder._2D;
using SharpDX;
using SharpDX.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TotL.Labyrinth;

namespace TotL.Units
{
    abstract class Unit : _2DGraphicsElement
    {
        //public int CoordinateX { get; set; }
        //public int CoordinateY { get; set; }
        public Vector2 Coordinate {get;set; }
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
        protected float unitsize = Vars.cellSize/8;

        public Unit(Vector2 coordinate)
        {
            Coordinate = coordinate;
        }

        public Unit(Vector2 coordinate, Vector2 targetcoordinate)
        {
            Coordinate = coordinate;
        }

        public virtual void DamageUnit(short dmg)
        {
            throw new InvalidCallException("hívás a unit alap fügvényre");
        }
        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException("Ez a változat nem használható Unit osztály leszármazotain");
        }
        public virtual void Update(Cell[,] connect,List<Unit> units)
        {
            throw new InvalidCallException("hívás a unit alap fügvényre");
        }
    }
}
