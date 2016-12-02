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
    class TwoSideBlocked : Cell
    {
        public TwoSideBlocked()
        {
            texture = TextureFromFile.TextureProcessor.getTexture("twoSideBlockedCell");
            setRotation(0f);
            closedsides = 2;

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
            if (rotation == Rotaitions.plus90)
            {
                float unitSize = (Vars.ScreenWidth * 0.83f) / 25f;
                locationX += unitSize;
                up = true;
                left = true;
                down = false;
                right = false;

            }else
            if (rotation == Rotaitions.half)
            {
                float unitSize = (Vars.ScreenWidth * 0.83f) / 25f;
                locationX += unitSize;
                locationY += unitSize;
                up = false;
                left = true;
                down = true;
                right = false;

            }else
            if (rotation == Rotaitions.minus90)
            {
                float unitSize = (Vars.ScreenWidth * 0.83f) / 25f;
                locationY += unitSize;
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
            
            float unitSize = (Vars.ScreenWidth * 0.83f) / 25f;
            Vars.spriteBatch.Draw(texture, new RectangleF(locationX, locationY, unitSize, unitSize), null, Color.White, rotation, new Vector2(0, 0), SpriteEffects.None, 0f);

        }
    }
    
    
}
