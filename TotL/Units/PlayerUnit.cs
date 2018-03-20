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
using SharpDX.Toolkit;

namespace TotL.Units
{
    class PlayerUnit:Unit
    {
        private int GetCoordinateFromLocation(int location)
        {
            int unitsize = Convert.ToInt32(Vars.cellSize);
            return (20 + (location * unitsize)) + (unitsize / 2)-8;
        }
        public PlayerUnit(Vector2 coordinate):base(coordinate)
        {

            Coordinate = coordinate;
            texture =TextureLoader.getTexture("transparent");
            HP = 100;
        }

        public PlayerUnit(Vector2 coordinate, Vector2 navcoordinate):base(coordinate,navcoordinate)
        {

            Coordinate = coordinate;
            this.navcoordinate=navcoordinate;
            texture = TextureLoader.getTexture("transparent");
            HP = 100;
        }

        public override void Initialize()
        {
        }

        public override void LoadContent()
        {
        }
        public override void DamageUnit(short dmg)
        {
            if (Vars.random.Next(0, 100) <40)
            {
                HP -= dmg;
            }


        }

        private short attackskiptick = 0;
        public override void Update(TerrainTile[,] map,List<Unit> units)
        {
            int X = ((int)Coordinate.X - 20) / (int)Vars.cellSize;
            int Y = ((int)Coordinate.Y - 20) / (int)Vars.cellSize;

            #region pathing
            if (Math.Abs(GetCoordinateFromLocation((int)target.X)-Coordinate.X) ==0 &&Math.Abs(GetCoordinateFromLocation((int)target.Y)-Coordinate.Y)==0)
            {
                path = null;
                hasnavcoordinate = false;
                hasTarget = false;
                haspath = false;
            }
            if (hasTarget && !haspath)
            {
                try
                {
                    path = AStar.RelationalAstarSolver.Search(new System.Drawing.Point(X + 1, Y + 1), new System.Drawing.Point((int)target.X + 1, (int)target.Y + 1), map);
                    if (path == null)
                    {
                        hasTarget = false;
                        haspath = false;
                    }
                    
                }
                catch (Exception)
                {

                    hasTarget = false;
                    haspath = false;
                }
                
            }
            else if(!hasnavcoordinate&&hasTarget&&haspath)
            {
                navcoordinate = new Vector2(GetCoordinateFromLocation(path.First.Value.X-1),GetCoordinateFromLocation(path.First.Value.Y-1));
                hasnavcoordinate = true;
            }
            if (hasnavcoordinate)
            {
                if ((int)navcoordinate.X != Coordinate.X || navcoordinate.Y != Coordinate.Y)
                {

                    if (!map[X, Y].IsIntersectsWith(new RectangleF(Coordinate.X + 1, Coordinate.Y + 1, Vars.cellSize/4, Vars.cellSize/4))||relocation)
                    {
                        
                        if ((int)navcoordinate.X > Coordinate.X)
                        {
                          
                            Coordinate += new Vector2(1,0);
                        }
                        if ((int)navcoordinate.X < Coordinate.X)
                        {

                            Coordinate += new Vector2(-1, 0);
                        }
                        if ((int)navcoordinate.Y > Coordinate.Y)
                        {

                            Coordinate += new Vector2(0, 1);
                        }
                        if ((int)navcoordinate.Y < Coordinate.Y)
                        {

                            Coordinate += new Vector2(0, -1);
                        }
                    }
                    else
                    {
                       
                        navcoordinate2 = navcoordinate;
                        navcoordinate = new Vector2(GetCoordinateFromLocation(X),GetCoordinateFromLocation(Y));
                        relocation = true;
                    }
                }
                else
                {
                    if (relocation)
                    {
                        navcoordinate = navcoordinate2;
                        relocation = false;
                        
                    }
                    else
                    {
                        hasnavcoordinate = false;
                    }

                    if (path != null)
                    {
                        path.RemoveFirst();
                    }
                }
            }
            #endregion

            if (map[X,Y] is UnitBase)
            {
                if (!(map[X,Y] as UnitBase).isdestroyed)
                {
                    attackskiptick++;
                    if (attackskiptick>=10)
                    {
                        (map[X, Y] as UnitBase).Damagebuilding(1, "friendly");
                        attackskiptick = 0;
                    }
                  
                }
               
            }
        }
        public override void Draw(GameTime gameTime)
        {
                 Vars.spriteBatch.Draw(texture,new RectangleF(Coordinate.X,Coordinate.Y,unitsize,unitsize),null,Color.White,0f,new Vector2(0,0),SpriteEffects.None,0f);
                
            if (Vars.path_debug_Draw && path != null)
            {
                foreach (var item in path)
                {
                    
                    Vars.spriteBatch.Draw(TextureLoader.getTexture("transparent"), new RectangleF((20 + ((item.X - 1) * Vars.cellSize + (Vars.cellSize / 2))) - 5, (20 + ((item.Y - 1) * Vars.cellSize + (Vars.cellSize / 2))) - 5, 10, 10), null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);

                }
            }
        }



        public override void UnloadContent()
        {
           
        }
    }
}
