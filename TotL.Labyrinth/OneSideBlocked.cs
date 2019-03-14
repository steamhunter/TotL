using PathFinder._2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFinder;
using SharpDX;
using SharpDX.Toolkit.Graphics;
using SharpDX.Toolkit;
using PathFinder.Scene;
using PathFinder.Components;

namespace TotL.Labyrinth
{
    public class OneSideBlocked : TerrainTile
    {
        public OneSideBlocked(int x,int y):base(x,y) 
        {
           GetComponent<Drawer>().Texture = TextureLoader.GetTexture("OneSideBlockedCell");

            SetRotation(0f);
            ClosedSides = 1;
        }

        public override void SetRotation(float rotation)
        {
            base.Rotation = rotation;
            if (rotation == Rotaitions.zero)
            {
                Up = true;
                Left = false;
                Down = true;
                Right = true;
            }
            else
            if (rotation == Rotaitions.minus90)
            {
                float unitSize = (Vars.ScreenWidth * 0.83f) / 25f;
                LocationXoffset = 0;// unitSize;
                Up = true;
                Left = true;
                Down = false;
                Right = true;

            }
            else
            if (rotation == Rotaitions.half)
            {
                float unitSize = (Vars.ScreenWidth * 0.83f) / 25f;
                LocationXoffset = unitSize;
                LocationYoffset = unitSize;
                Up = true;
                Left = true;
                Down = true;
                Right = false;

            }
            else
            if (rotation == Rotaitions.plus90)
            {
                float unitSize = (Vars.ScreenWidth * 0.83f) / 25f;
                LocationXoffset = unitSize;
                Up = false;
                Left = true;
                Down = true;
                Right = true;

            }
            
        }

        public override void Draw(GameTime gameTime)
        {
            Vars.spriteBatch.Draw(GetComponent<Drawer>().Texture, new RectangleF(LocationX+LocationXoffset, LocationY+LocationYoffset, unitSize, unitSize), null, Color.White, Rotation, new Vector2(0, 0), SpriteEffects.None, 0f);

            /*foreach (var item in _blockedvolumes)
            {
                Vars.spriteBatch.Draw(TextureFromFile.TextureProcessor.getTexture("transparent"), item, Color.White);
            }*/
        }

        public override bool CheckFitting(Connection[,] connect, int co, int cs, int o, int s)
        {
            if (ClosedSides >= Connection.GetClosedSides(connect[co, cs-1], connect[co+1, cs], connect[co, cs+1], connect[co-1, cs]))
            {
                if (Connection.IsFiting(this, connect[co, cs - 1], connect[co + 1, cs], connect[co, cs + 1], connect[co - 1, cs]))
                {
                    LocationX = 20 + ((o) * unitSize);
                    LocationY = 20 + ((s) * unitSize);
                    connect[co, cs].Up = Up;
                    connect[co, cs].Down = Down;
                    connect[co, cs].Left = Left;
                    connect[co, cs].Right = Right;
                    return true;

                }
                else
                {
                    SetRotation(Rotaitions.plus90);
                    if (Connection.IsFiting(this, connect[co, cs - 1], connect[co + 1, cs], connect[co, cs + 1], connect[co - 1, cs]))
                    {
                        LocationX = 20 + ((o) * unitSize);
                        LocationY = 20 + ((s) * unitSize);
                        connect[co, cs].Up = Up;
                        connect[co, cs].Down = Down;
                        connect[co, cs].Left = Left;
                        connect[co, cs].Right = Right;
                        return true;
                    }
                    else
                    {
                        SetRotation(Rotaitions.half);
                        if (Connection.IsFiting(this, connect[co, cs - 1], connect[co + 1, cs], connect[co, cs + 1], connect[co - 1, cs]))
                        {
                            LocationX = 20 + ((o) * unitSize);
                            LocationY = 20 + ((s) * unitSize);
                            connect[co, cs].Up = Up;
                            connect[co, cs].Down = Down;
                            connect[co, cs].Left = Left;
                            connect[co, cs].Right = Right;
                            return true;
                        }
                        else
                        {
                            SetRotation(Rotaitions.minus90);
                            if (Connection.IsFiting(this, connect[co, cs - 1], connect[co + 1, cs], connect[co, cs + 1], connect[co - 1, cs]))
                            {
                                LocationX = 20 + ((o) * unitSize);
                                LocationY = 20 + ((s) * unitSize);
                                connect[co, cs].Up = Up;
                                connect[co, cs].Down = Down;
                                connect[co, cs].Left = Left;
                                connect[co, cs].Right = Right;
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

        public override void SetBlockingVolumes()
        {

            float locationx = LocationX + LocationXoffset;
            float locationy = LocationY + LocationYoffset;
            if (Rotation == Rotaitions.zero)
            {
                _blockedVolumes.Add(new RectangleF(locationx, locationy, unitSize / 4, unitSize));
                _blockedVolumes.Add(new RectangleF((locationx + unitSize) - unitSize / 4, locationy, unitSize / 4, unitSize / 4));
                _blockedVolumes.Add(new RectangleF((locationx + unitSize) - unitSize / 4, (locationy + unitSize) - unitSize / 4, unitSize / 4, unitSize / 4));
            }
            else
            if (Rotation == Rotaitions.plus90)
            {
                _blockedVolumes.Add(new RectangleF(locationx - unitSize, locationy, unitSize, unitSize / 4));
                _blockedVolumes.Add(new RectangleF(locationx - unitSize, locationy + (unitSize - unitSize / 4), unitSize / 4, unitSize / 4));
                _blockedVolumes.Add(new RectangleF(locationx - unitSize / 4, (locationy + unitSize) - unitSize / 4, unitSize / 4, unitSize / 4));
            }
            else
            if (Rotation == Rotaitions.half)
            {
                _blockedVolumes.Add(new RectangleF(locationx - unitSize / 4, locationy - unitSize, unitSize / 4, unitSize));
                _blockedVolumes.Add(new RectangleF(locationx - unitSize, locationy - unitSize / 4, unitSize / 4, unitSize / 4));
                _blockedVolumes.Add(new RectangleF(locationx - unitSize, locationy - unitSize, unitSize / 4, unitSize / 4));
            }
            else if (Rotation == Rotaitions.minus90)
            {
                _blockedVolumes.Add(new RectangleF(locationx, locationy - unitSize / 4, unitSize, unitSize / 4));
                _blockedVolumes.Add(new RectangleF(locationx, locationy - unitSize, unitSize / 4, unitSize / 4));
                _blockedVolumes.Add(new RectangleF((locationx + unitSize) - unitSize / 4, locationy - unitSize, unitSize / 4, unitSize / 4));
            }
        }

        public override string ToString()
        {
            return "OneSideBlocked";
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void LoadContent()
        {
            throw new NotImplementedException();
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override void UnloadContent()
        {
            throw new NotImplementedException();
        }
    }
}
