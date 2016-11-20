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
    class CrossCell : Unit2D,IConnections
    {
        public CrossCell(Game game)
        {
            texture = TextureFromFile.TextureProcessor.getTexture("CrossCell");
            
        }

        bool _up,_down,_left,_right=true;
        public bool down
        {
            get
            {
                return _down;
            }    
        }
        public bool left
        {
            get
            {
                return _left;
            }
        }
        public bool right
        {
            get
            {
                return _right;
            }
        }
        public bool up
        {
            get
            {
                return _up;
            }
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
