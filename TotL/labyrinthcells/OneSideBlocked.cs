using PathFinder._2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFinder;
using SharpDX;
using SharpDX.Toolkit.Graphics;

namespace TotL.labyrinthcells
{
    class OneSideBlocked : Cell
    {
        public OneSideBlocked() 
        {
            texture = TextureFromFile.TextureProcessor.getTexture("OneSideBlockedCell");

            setRotation(0f);
            closedsides = 1;
        }

        public override void setRotation(float rotation)
        {
            base.rotation = rotation;
            if (rotation == Rotaitions.zero)
            {
                up = true;
                left = false;
                down = true;
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
                right = true;

            }
            else
            if (rotation == Rotaitions.half)
            {
                float unitSize = (Vars.ScreenWidth * 0.83f) / 25f;
                LocationXoffset = unitSize;
                LocationYoffset = unitSize;
                up = true;
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
                left = true;
                down = true;
                right = true;

            }
            
        }

        public override void draw()
        {
            Vars.spriteBatch.Draw(texture, new RectangleF(locationX, locationY, unitSize, unitSize), null, Color.White, rotation, new Vector2(0, 0), SpriteEffects.None, 0f);

            /*foreach (var item in _blockedvolumes)
            {
                Vars.spriteBatch.Draw(TextureFromFile.TextureProcessor.getTexture("transparent"), item, Color.White);
            }*/
        }

        public override bool CheckFitting(Connection[,] connect, int co, int cs, int o, int s)
        {
            if (closedsides >= Connection.getClosedSides(connect[cs - 1, co], connect[cs, co + 1], connect[cs + 1, co], connect[cs, co - 1]))
            {
                if (Connection.isFiting(this, connect[cs - 1, co], connect[cs, co + 1], connect[cs + 1, co], connect[cs, co - 1]))
                {
                    locationX = 20 + ((o) * unitSize);
                    locationY = 20 + ((s) * unitSize);
                    connect[cs, co].up = up;
                    connect[cs, co].down = down;
                    connect[cs, co].left = left;
                    connect[cs, co].right = right;
                    return true;

                }
                else
                {
                    setRotation(Rotaitions.plus90);
                    if (Connection.isFiting(this, connect[cs - 1, co], connect[cs, co + 1], connect[cs + 1, co], connect[cs, co - 1]))
                    {
                        locationX = 20 + ((o) * unitSize);
                        locationY = 20 + ((s) * unitSize);
                        connect[cs, co].up = up;
                        connect[cs, co].down = down;
                        connect[cs, co].left = left;
                        connect[cs, co].right = right;
                        return true;
                    }
                    else
                    {
                        setRotation(Rotaitions.half);
                        if (Connection.isFiting(this, connect[cs - 1, co], connect[cs, co + 1], connect[cs + 1, co], connect[cs, co - 1]))
                        {
                            locationX = 20 + ((o) * unitSize);
                            locationY = 20 + ((s) * unitSize);
                            connect[cs, co].up = up;
                            connect[cs, co].down = down;
                            connect[cs, co].left = left;
                            connect[cs, co].right = right;
                            return true;
                        }
                        else
                        {
                            setRotation(Rotaitions.minus90);
                            if (Connection.isFiting(this, connect[cs - 1, co], connect[cs, co + 1], connect[cs + 1, co], connect[cs, co - 1]))
                            {
                                locationX = 20 + ((o) * unitSize);
                                locationY = 20 + ((s) * unitSize);
                                connect[cs, co].up = up;
                                connect[cs, co].down = down;
                                connect[cs, co].left = left;
                                connect[cs, co].right = right;
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


            if (rotation == Rotaitions.zero)
            {
                _blockedvolumes.Add(new RectangleF(locationX, locationY, unitSize / 4, unitSize));
                _blockedvolumes.Add(new RectangleF((locationX + unitSize) - unitSize / 4, locationY, unitSize / 4, unitSize / 4));
                _blockedvolumes.Add(new RectangleF((locationX + unitSize) - unitSize / 4, (locationY + unitSize) - unitSize / 4, unitSize / 4, unitSize / 4));
            }
            else
            if (rotation == Rotaitions.plus90)
            {
                _blockedvolumes.Add(new RectangleF(locationX - unitSize, locationY, unitSize, unitSize / 4));
                _blockedvolumes.Add(new RectangleF(locationX - unitSize, locationY + (unitSize - unitSize / 4), unitSize / 4, unitSize / 4));
                _blockedvolumes.Add(new RectangleF(locationX - unitSize / 4, (locationY + unitSize) - unitSize / 4, unitSize / 4, unitSize / 4));
            }
            else
            if (rotation == Rotaitions.half)
            {
                _blockedvolumes.Add(new RectangleF(locationX - unitSize / 4, locationY - unitSize, unitSize / 4, unitSize));
                _blockedvolumes.Add(new RectangleF(locationX - unitSize, locationY - unitSize / 4, unitSize / 4, unitSize / 4));
                _blockedvolumes.Add(new RectangleF(locationX - unitSize, locationY - unitSize, unitSize / 4, unitSize / 4));
            }
            else if (rotation == Rotaitions.minus90)
            {
                _blockedvolumes.Add(new RectangleF(locationX, locationY - unitSize / 4, unitSize, unitSize / 4));
                _blockedvolumes.Add(new RectangleF(locationX, locationY - unitSize, unitSize / 4, unitSize / 4));
                _blockedvolumes.Add(new RectangleF((locationX + unitSize) - unitSize / 4, locationY - unitSize, unitSize / 4, unitSize / 4));
            }
        }


    }
}
