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
            base.rotation = rotation;
           
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
    }
}
