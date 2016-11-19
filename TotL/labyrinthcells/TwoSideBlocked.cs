using PathFinder;
using PathFinder._2D;
using SharpDX;
using SharpDX.Toolkit.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotL.labyrinthcells
{
    class TwoSideBlocked : Unit2D
    {
        public TwoSideBlocked()
        {
            texture = TextureFromFile.TextureProcessor.getTexture("twoSideBlockedCell");
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
