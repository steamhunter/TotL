﻿using PathFinder;
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
    class Unit:_2DGraphicsElement
    {
        public int locationX { get; set; }
        public int locationY { get; set; }
        public Unit(int locationX,int locationY)
        {
            this.locationX = locationX;
            this.locationY = locationY;
        }

        public Unit(int locationX, int locationY, Vector2 targetlocation)
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
            throw new InvalidCallException("Unit nem fogható meg mint 2DGraphicsElement");
        }
        public virtual void update(Cell [,] map)
        {
            throw new InvalidCallException("hívás a unit alap fügvényre");
        }
    }
}
