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
        public PlayerUnit(int locationX,int locationY):base(locationX,locationY)
        {
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
        }
        public override void draw()
        {
            Vars.spriteBatch.Draw(texture,new RectangleF(locationX,locationY,16,16),null,Color.White,0f,new Vector2(0,0),SpriteEffects.None,0f);
        }
    }
}
