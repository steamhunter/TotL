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

namespace TotL.Labyrinth
{
    public class CrossCell : Cell 
    {
        public CrossCell(int x,int y):base(x,y)
        {
            texture = TextureFromFile.TextureProcessor.getTexture("CrossCell");
            closedsides = 0;
            X = x;
            Y = y;
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
            
            Vars.spriteBatch.Draw(texture, new RectangleF(locationX, locationY, unitSize, unitSize), null, Color.White, rotation, new Vector2(0, 0), SpriteEffects.None, 0f);
           /* foreach (var item in _blockedvolumes)
            {
                Vars.spriteBatch.Draw(TextureFromFile.TextureProcessor.getTexture("transparent"), item, Color.White);
            }*/

        }
        public override void SetBlockingVolumes()
        {
           
            _blockedvolumes.Add(new RectangleF((locationX + unitSize) - unitSize / 4, locationY, unitSize / 4, unitSize / 4));
            _blockedvolumes.Add(new RectangleF((locationX + unitSize) - unitSize / 4, (locationY + unitSize) - unitSize / 4, unitSize / 4, unitSize / 4));
            _blockedvolumes.Add(new RectangleF(locationX, locationY, unitSize / 4, unitSize / 4));
            _blockedvolumes.Add(new RectangleF(locationX , (locationY + unitSize) - unitSize / 4, unitSize / 4, unitSize / 4));
        }

        public override bool CheckFitting(Connection[,] connect, int co, int cs, int o, int s)
        {
            if (closedsides >= Connection.getClosedSides(connect[co, cs-1], connect[co+1, cs], connect[co, cs+1], connect[co-1, cs]))
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
                    if (Connection.isFiting(this,connect[co, cs - 1], connect[co + 1, cs], connect[co, cs + 1], connect[co - 1, cs]))
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
