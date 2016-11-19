using PathFinder._2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Toolkit;
using PathFinder;
using SharpDX;
using SharpDX.Toolkit.Graphics;

namespace TotL.labyrinthcells
{
    class OneSideBlocked : Unit2D
    {
        public OneSideBlocked() 
        {
            texture = TextureFromFile.TextureProcessor.getTexture("OneSideBlockedCell");
        }
        public override void draw()
        {
            float unitSize = (Vars.ScreenWidth * 0.83f) / 25f;

           
            Vars.spriteBatch.Draw(texture, new RectangleF(locationX, locationY, unitSize, unitSize), Color.White);
        }
        public override void draw(float rotation)
        {
            float unitSize = (Vars.ScreenWidth * 0.83f) / 25f;
            Vars.spriteBatch.Draw(texture, new RectangleF(locationX, locationY, unitSize, unitSize), null, Color.White, rotation, new Vector2(0, 0), SpriteEffects.None, 0f);

        }
    }
}
