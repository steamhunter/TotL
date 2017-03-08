using PathFinder;
using PathFinder._2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotL.Units
{
    class Unit:_2DGraphicsElement
    {
        public int locationX { get; set; }
        public int locationY { get; set; }
        public Unit(int locationX,int locationY)
        {
            this.locationX = locationX;
            this.locationY = locationY;
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
            throw new InvalidCallException("hívás a unit alap fügvényre");
        }
    }
}
