﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFinder._2D;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
using PathFinder;
using SharpDX;

namespace TotL.labyrinthcells
{
    class CrossCell : Cell 
    {
        public CrossCell()
        {
            texture = TextureFromFile.TextureProcessor.getTexture("CrossCell");
            closedsides = 0;
            
        }


        public override void setRotation(float rotation)
        {
            base.rotation = 0;
           
            up = true;
            left = true;
            down = true;
            right = true;
            
        }

        public override void draw()
        {
            float unitSize = (Vars.ScreenWidth * 0.83f) / 25f;
            Vars.spriteBatch.Draw(texture, new RectangleF(locationX, locationY, unitSize, unitSize), null, Color.White, rotation, new Vector2(0, 0), SpriteEffects.None, 0f);

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
    }
}
