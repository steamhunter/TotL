using PathFinder;
using PathFinder._2D;
using PathFinder.AStar;
using SharpDX;
using SharpDX.Toolkit.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TotL.Labyrinth;

namespace TotL.Units
{
    class PlayerUnit:Unit
    {
        private int GetCoordinateFromLocation(int location)
        {
            int unitsize = Convert.ToInt32(Vars.unitSize);
            return (20 + (location * unitsize)) + (unitsize / 2);
        }
        public PlayerUnit(int locationX,int locationY):base(locationX,locationY)
        {

            this.locationX = locationX;
            this.locationY = locationY;
            texture = TextureFromFile.TextureProcessor.getTexture("transparent");
        }

        public PlayerUnit(int locationX, int locationY, Vector2 navcoordinate):base(locationX,locationY,navcoordinate)
        {

            this.locationX = locationX;
            this.locationY = locationY;
            this.navcoordinate=navcoordinate;
            texture = TextureFromFile.TextureProcessor.getTexture("transparent");
        }

        public override void Initialize()
        {
        }

        public override void Load()
        {
        }
        public override void update(Cell[,] map)
        {
            int X = (locationX - 20) / (int)Vars.unitSize;
            int Y = (locationY - 20) / (int)Vars.unitSize;
            if (hasTarget)
            {

                path = AStar.AstarSolver.Search(new System.Drawing.Point(X, Y), new System.Drawing.Point((int)target.X, (int)target.Y), map);
                path.RemoveFirst();
            }
            if (target.X == X && target.Y == Y)
            {
                path = null;
                hasnavcoordinate = false;
                hasTarget = false;
            }
            else if(!hasnavcoordinate&&hasTarget)
            {
                navcoordinate = new Vector2(GetCoordinateFromLocation(path.First.Value.X),GetCoordinateFromLocation(path.First.Value.Y));
                hasnavcoordinate = true;
            }
            if (hasnavcoordinate)
            {
                if ((int)navcoordinate.X != locationX || navcoordinate.Y != locationY)
                {

                    if (!map[X, Y].CheckBlockingState(new RectangleF(locationX + 1, locationY + 1, 16, 16)))
                    {
                        if ((int)navcoordinate.X != locationX)
                        {
                            locationX += 1;
                        }
                        if ((int)navcoordinate.Y != locationY)
                        {
                            locationY += 1;
                        }
                    }
                }
                else
                {
                    hasnavcoordinate = false;
                    if (path != null)
                    {
                        path.RemoveFirst();
                    }
                }
            }
        }
        public override void draw()
        {
                 Vars.spriteBatch.Draw(texture,new RectangleF(locationX,locationY,16,16),null,Color.White,0f,new Vector2(0,0),SpriteEffects.None,0f);
        }
    }
}
