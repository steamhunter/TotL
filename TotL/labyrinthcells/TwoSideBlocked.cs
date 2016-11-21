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
    class TwoSideBlocked : Unit2D,IConnections
    {
        public TwoSideBlocked()
        {
            texture = TextureFromFile.TextureProcessor.getTexture("twoSideBlockedCell");
        }

        bool _up, _right = true;
        bool _left, _down = false;
        public bool down
        {
            get
            {
                return _down;
            }set
            {
                _down = value;
            }
        }
        public bool left
        {
            get
            {
                return _left;
            }
            set
            {
                _left = value;
            }
        }
        public bool right
        {
            get
            {
                return _right;
            }
            set
            {
                _right = value;
            }
        }
        public bool up
        {
            get
            {
                return _up;
            }
            set
            {
                _up = value;
            }
        }
        float _rotation;
        public float rotation
        {
            get
            {
                return _rotation;
            }

            set
            {
                _rotation=value;
            }
        }

        public override void draw()
        {
           
            float unitSize = (Vars.ScreenWidth * 0.83f) / 25f;
            Vars.spriteBatch.Draw(texture, new RectangleF(locationX, locationY, unitSize, unitSize), null, Color.White, _rotation, new Vector2(0, 0), SpriteEffects.None, 0f);

        }

        public float rotate(float rotate)
        {
            if (rotate == Convert.ToSingle(Math.PI) / 2)
            {
                up = true;
                left = true;
                down = false;
                right = false;

            }
            _rotation = rotate;
            return rotate;
        }
    }
    
    
}
