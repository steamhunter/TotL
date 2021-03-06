﻿using PathFinder;
using PathFinder._2D;
using SharpDX;
using SharpDX.Toolkit.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotL.Labyrinth
{
    public class TwoSideBlocked : Cell
    {
        public TwoSideBlocked(int x, int y) : base(x, y)
        {
            texture = TextureFromFile.TextureProcessor.getTexture("twoSideBlockedCell");
            setRotation(0f);
            closedsides = 2;
            X = x;
            Y = y;
        }

        public override void setRotation(float rotation)
        {
            base.rotation = rotation;
            if (rotation == Rotaitions.zero)
            {
                up = true;
                left = false;
                down = false;
                right = true;
            }
            else
            if (rotation == Rotaitions.minus90)
            {
                float unitSize = (Vars.ScreenWidth * 0.83f) / 25f;
                LocationXoffset = 0;// unitSize;
                up = true;
                left = true;
                down = false;
                right = false;

            }
            else
            if (rotation == Rotaitions.half)
            {
                float unitSize = (Vars.ScreenWidth * 0.83f) / 25f;
                LocationXoffset = unitSize;
                LocationYoffset = unitSize;
                up = false;
                left = true;
                down = true;
                right = false;

            }
            else
            if (rotation == Rotaitions.plus90)
            {
                float unitSize = (Vars.ScreenWidth * 0.83f) / 25f;
                LocationXoffset = unitSize;
                up = false;
                left = false;
                down = true;
                right = true;

            }

        }

        public override void update()
        {

        }
        public override void draw()
        {

            Vars.spriteBatch.Draw(texture, new RectangleF(locationX+LocationXoffset, locationY+LocationYoffset, unitSize, unitSize), null, Color.White, rotation, new Vector2(0, 0), SpriteEffects.None, 0f);


           /*  foreach (var item in _blockedvolumes)
             {
                 Vars.spriteBatch.Draw(TextureFromFile.TextureProcessor.getTexture("transparent"), item, Color.White);
             }*/

        }

        public override void SetBlockingVolumes()
        {
            float locationx = locationX + LocationXoffset;
            float locationy = locationY + LocationYoffset;

            if (rotation == Rotaitions.zero)
            {
                _blockedvolumes.Add(new RectangleF(locationx, locationy, unitSize / 4, unitSize));
                _blockedvolumes.Add(new RectangleF(locationx + unitSize / 4, (locationy + unitSize) - unitSize / 4, unitSize - unitSize / 4, unitSize / 4));
                _blockedvolumes.Add(new RectangleF((locationx + unitSize) - unitSize / 4, locationy, unitSize / 4, unitSize / 4));
            }
            else
            if (rotation == Rotaitions.plus90)
            {
                _blockedvolumes.Add(new RectangleF(locationx - unitSize, locationy, unitSize, unitSize / 4));
                _blockedvolumes.Add(new RectangleF(locationx - unitSize, locationy + unitSize / 4, unitSize / 4, unitSize - unitSize / 4));
                _blockedvolumes.Add(new RectangleF(locationx - unitSize / 4, (locationy + unitSize) - unitSize / 4, unitSize / 4, unitSize / 4));
            }
            else if (rotation == Rotaitions.minus90)
            {
                _blockedvolumes.Add(new RectangleF(locationx, locationy - unitSize, unitSize / 4, unitSize / 4));
                _blockedvolumes.Add(new RectangleF((locationx + unitSize) - unitSize / 4, locationy - unitSize, unitSize / 4, unitSize));
                _blockedvolumes.Add(new RectangleF(locationx, locationy - unitSize / 4, unitSize - unitSize / 4, unitSize / 4));
            }
            else if (rotation == Rotaitions.half)
            {
                _blockedvolumes.Add(new RectangleF(locationx - unitSize, locationy - unitSize, unitSize, unitSize / 4));
                _blockedvolumes.Add(new RectangleF(locationx - unitSize, locationy - unitSize / 4, unitSize / 4, unitSize / 4));
                _blockedvolumes.Add(new RectangleF(locationx - unitSize / 4, (locationy - unitSize) + unitSize / 4, unitSize / 4, unitSize - unitSize / 4));
            }
        }

        public override bool CheckFitting(Connection[,] connect, int co, int cs, int o, int s)
        {
            if (closedsides >= Connection.getClosedSides(connect[co, cs - 1], connect[co + 1, cs], connect[co, cs + 1], connect[co - 1, cs]))
            {
                if (Connection.isFiting(this, connect[co, cs - 1], connect[co + 1, cs], connect[co, cs + 1], connect[co - 1, cs]))
                {
                    locationX = 20 + ((o) * unitSize);
                    locationY = 20 + ((s) * unitSize);
                    connect[co, cs].up = up;
                    connect[co, cs].down = down;
                    connect[co, cs].left = left;
                    connect[co, cs].right = right;
                    return true;

                }
                else
                {
                    setRotation(Rotaitions.plus90);
                    if (Connection.isFiting(this, connect[co, cs - 1], connect[co + 1, cs], connect[co, cs + 1], connect[co - 1, cs]))
                    {
                        locationX = 20 + ((o) * unitSize);
                        locationY = 20 + ((s) * unitSize);
                        connect[co, cs].up = up;
                        connect[co, cs].down = down;
                        connect[co, cs].left = left;
                        connect[co, cs].right = right;
                        return true;
                    }
                    else
                    {
                        setRotation(Rotaitions.half);
                        if (Connection.isFiting(this, connect[co, cs - 1], connect[co + 1, cs], connect[co, cs + 1], connect[co - 1, cs]))
                        {
                            locationX = 20 + ((o) * unitSize);
                            locationY = 20 + ((s) * unitSize);
                            connect[co, cs].up = up;
                            connect[co, cs].down = down;
                            connect[co, cs].left = left;
                            connect[co, cs].right = right;
                            return true;
                        }
                        else
                        {
                            setRotation(Rotaitions.minus90);
                            if (Connection.isFiting(this, connect[co, cs - 1], connect[co + 1, cs], connect[co, cs + 1], connect[co - 1, cs]))
                            {
                                locationX = 20 + ((o) * unitSize);
                                locationY = 20 + ((s) * unitSize);
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
    }


}
