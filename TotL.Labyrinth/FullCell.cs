using PathFinder;
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
    public class FullCell:Cell
    {
        public FullCell(int y,int x):base(y,x)
        {
            texture = TextureFromFile.TextureProcessor.getTexture("FullCell");
            setRotation(0f);
            closedsides = 4;
            X = x;
            Y = y;
        }

        public override void setRotation(float rotation)
        {
            base.rotation = 0;
           
                up = false;
                left = false;
                down = false;
                right = false;
           

        }

        public override void update()
        {

        }
        public override void draw()
        {

            float unitSize = (Vars.ScreenWidth * 0.83f) / 25f;
              Vars.spriteBatch.Draw(texture, new RectangleF(locationX, locationY, unitSize, unitSize), null, Color.White, rotation, new Vector2(0, 0), SpriteEffects.None, 0f);

        }
        public override void SetBlockingVolumes()
        {
            _blockedvolumes.Add(rectangle);
            //base.SetBlockingVolumes();
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
                    return false;
                }
                
                   
            }
            else
            {
                return false;
            }


        }
    }
}

