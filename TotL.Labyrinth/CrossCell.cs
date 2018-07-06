using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFinder._2D;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
using PathFinder;
using SharpDX;
using PathFinder.Scene;

namespace TotL.Labyrinth
{
    public class CrossCell : TerrainTile 
    {
        public CrossCell(int x,int y):base(x,y)
        {
            GetComponent<PathFinder.Components.Drawer>().Texture = TextureLoader.getTexture("CrossCell");
            closedsides = 0;
        }

        public override void SetRotation(float rotation)
        {
            base.Rotation = 0;
           
            up = true;
            left = true;
            down = true;
            right = true;
            
        }

        public override void Draw(GameTime gameTime)
        {
            Vars.spriteBatch.Draw(GetComponent<PathFinder.Components.Drawer>().Texture, new RectangleF(LocationX+LocationXoffset, LocationY+LocationYoffset, unitSize, unitSize), null, Color.White, Rotation, new Vector2(0, 0), SpriteEffects.None, 0f);
           /* foreach (var item in _blockedvolumes)
            {
                Vars.spriteBatch.Draw(TextureFromFile.TextureProcessor.getTexture("transparent"), item, Color.White);
            }*/

        }

        public override void SetBlockingVolumes()
        {
            float locationx = LocationX + LocationXoffset;
            float locationy = LocationY + LocationYoffset;
            _blockedvolumes.Add(new RectangleF((locationx + unitSize) - unitSize / 4, locationy, unitSize / 4, unitSize / 4));
            _blockedvolumes.Add(new RectangleF((locationx + unitSize) - unitSize / 4, (locationy + unitSize) - unitSize / 4, unitSize / 4, unitSize / 4));
            _blockedvolumes.Add(new RectangleF(locationx, locationy, unitSize / 4, unitSize / 4));
            _blockedvolumes.Add(new RectangleF(locationx , (locationy + unitSize) - unitSize / 4, unitSize / 4, unitSize / 4));
        }

        public override bool CheckFitting(Connection[,] connect, int co, int cs, int o, int s)
        {
            if (closedsides >= Connection.getClosedSides(connect[co, cs-1], connect[co+1, cs], connect[co, cs+1], connect[co-1, cs]))
            {
                if (Connection.isFiting(this, connect[co, cs - 1], connect[co + 1, cs], connect[co, cs + 1], connect[co - 1, cs]))
                {
                    LocationX = 20 + ((o) * unitSize);
                    LocationY = 20 + ((s) * unitSize);
                    connect[co, cs].up = up;
                    connect[co, cs].down = down;
                    connect[co, cs].left = left;
                    connect[co, cs].right = right;
                    return true;

                }
                else
                {
                    SetRotation(Rotaitions.plus90);
                    if (Connection.isFiting(this,connect[co, cs - 1], connect[co + 1, cs], connect[co, cs + 1], connect[co - 1, cs]))
                    {
                        LocationX = 20 + ((o) * unitSize);
                        LocationY = 20 + ((s) * unitSize);
                        connect[co, cs].up = up;
                        connect[co, cs].down = down;
                        connect[co, cs].left = left;
                        connect[co, cs].right = right;
                        return true;
                    }
                    else
                    {
                        SetRotation(Rotaitions.half);
                        if (Connection.isFiting(this, connect[co, cs - 1], connect[co + 1, cs], connect[co, cs + 1], connect[co - 1, cs]))
                        {
                            LocationX = 20 + ((o) * unitSize);
                            LocationY = 20 + ((s) * unitSize);
                            connect[co, cs].up = up;
                            connect[co, cs].down = down;
                            connect[co, cs].left = left;
                            connect[co, cs].right = right;
                            return true;
                        }
                        else
                        {
                            SetRotation(Rotaitions.minus90);
                            if (Connection.isFiting(this, connect[co, cs - 1], connect[co + 1, cs], connect[co, cs + 1], connect[co - 1, cs]))
                            {
                                LocationX = 20 + ((o) * unitSize);
                                LocationY = 20 + ((s) * unitSize);
                                connect[co, cs].up = up;
                                connect[co, cs].down = down;
                                connect[co, cs].left = left;
                                connect[co, cs].right = right;
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

        public override string ToString()
        {
            return "CrossCell";
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void LoadContent()
        {
           
        }

        public override void Initialize()
        {
            
        }

        public override void UnloadContent()
        {
            
        }
    }
}
