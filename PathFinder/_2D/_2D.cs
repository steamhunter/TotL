using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFinder;
using Tex2D = SharpDX.Toolkit.Graphics.Texture2D;

namespace PathFinder._2D
{
    public class _2DGraphicsElement
    {
        protected Tex2D texture;
        public Rectangle rectangle;
        protected float _locationX;
        protected float _locationY;
        
        
        public static Tex2D getTexture(string entity, Game game)
        {

            /*
            switch (entity)
            {
                case "player": return game.Content.Load<Texture2D>("player");
                case "grass": return game.Content.Load<Texture2D>("terraincell");
                case "enemy": return game.Content.Load<Texture2D>("enemy");
                    


                default: return null;

            }*/
            throw new DeprecatedMethodException("getTexture is not useable no working code");
            

        }

        public _2DGraphicsElement()
        {
           


        }
       
        public virtual void update()
        {
            throw new NotImplementedException("hívás a 2D Graphics Element alap fügvényre");
        }


        public virtual void Load()
        {
            throw new NotImplementedException("hívás a 2D Graphics Element alap fügvényre");
        }

        public virtual void Initialize()
        {
            throw new NotImplementedException("hívás a 2D Graphics Element alap fügvényre");
        }
       
        public virtual void draw()
        {
            Tex2D tex = null;
            float unitSize = (Vars.ScreenWidth * 0.83f) / 25f;
            Vars.spriteBatch.Draw(texture, new RectangleF(_locationX, _locationY, unitSize, unitSize), null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
            Vars.spriteBatch.Draw(tex, new RectangleF(_locationX, _locationY, unitSize, unitSize), null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
        }
    }
}
