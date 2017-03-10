using PathFinder;
using PathFinder._2D;
using SharpDX;
using SharpDX.Toolkit.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotL.Units
{
    class PlayerUnit:Unit
    {
        Vector2 targetlocation;
        public PlayerUnit(int locationX,int locationY):base(locationX,locationY)
        {

            this.locationX = locationX;
            this.locationY = locationY;
            texture = TextureFromFile.TextureProcessor.getTexture("transparent");
        }

        public PlayerUnit(int locationX, int locationY, Vector2 targetlocation):base(locationX,locationY,targetlocation)
        {

            this.locationX = locationX;
            this.locationY = locationY;
            this.targetlocation=targetlocation;
            texture = TextureFromFile.TextureProcessor.getTexture("transparent");
        }

        public override void Initialize()
        {
        }

        public override void Load()
        {
        }
        public override void update()
        {
            if (targetlocation!=null)
            {
                if ((int)targetlocation.X!=locationX||targetlocation.Y!=locationY)
                {
                    if ((int)targetlocation.X!=locationX)
                    {
                        locationX += 1;
                    }
                    if ((int)targetlocation.Y!=locationY)
                    {
                        locationY += 1;
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
