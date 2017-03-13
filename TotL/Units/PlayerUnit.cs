using PathFinder;
using PathFinder._2D;
using PathFinder.AStar;
using PathFinder.Debug;
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
            return (20 + (location * unitsize)) + (unitsize / 2)-8;
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
            if (hasTarget&&path==null)
            {

                path = AStar.AstarSolver.Search(new System.Drawing.Point(X+1, Y+1), new System.Drawing.Point((int)target.X+1, (int)target.Y+1), map);
               // path.RemoveFirst();
            }
            if (Math.Abs(GetCoordinateFromLocation((int)target.X)-locationX) ==0 &&Math.Abs(GetCoordinateFromLocation((int)target.Y)-locationY)==0)
            {
                path = null;
                hasnavcoordinate = false;
                hasTarget = false;
            }
            else if(!hasnavcoordinate&&hasTarget)
            {
                navcoordinate = new Vector2(GetCoordinateFromLocation(path.First.Value.X-1),GetCoordinateFromLocation(path.First.Value.Y-1));
                hasnavcoordinate = true;
            }
            if (hasnavcoordinate)
            {
                if ((int)navcoordinate.X != locationX || navcoordinate.Y != locationY)
                {

                    if (!map[X, Y].CheckBlockingState(new RectangleF(locationX + 1, locationY + 1, 16, 16)))
                    {
                        if ((int)navcoordinate.X > locationX)
                        {
                            locationX += 1;
                        }
                        if ((int)navcoordinate.X < locationX)
                        {
                            locationX -= 1;
                        }
                        if ((int)navcoordinate.Y > locationY)
                        {
                            locationY += 1;
                        }
                        if ((int)navcoordinate.Y < locationY)
                        {
                            locationY -= 1;
                        }
                    }
                    else
                    {
                        cons.debugMessage(map[X, Y].ToString());
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
                
            if (Vars.path_debug_Draw && path != null)
            {
                foreach (var item in path)
                {
                    
                    Vars.spriteBatch.Draw(TextureFromFile.TextureProcessor.getTexture("transparent"), new RectangleF((20 + ((item.X - 1) * Vars.unitSize + (Vars.unitSize / 2))) - 5, (20 + ((item.Y - 1) * Vars.unitSize + (Vars.unitSize / 2))) - 5, 10, 10), null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);

                }
            }
        }
    }
}
