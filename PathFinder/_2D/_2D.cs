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

namespace PathFinder._2D
{
    public class _2DGraphicsElement
    {
        protected ShaderResourceView texture;
        public Rectangle rectangle;
        protected float _locationX;
        protected float _locationY;
        
        
        public static SharpDX.Toolkit.Graphics.Texture2D getTexture(string entity, Game game)
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
        /*public virtual void draw()
        {
            float unitSize = (Vars.ScreenWidth * 0.83f) / 25f;

            //spritebatch.Draw(texture, new Vector2(locationX, locationY), null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
           Vars.spriteBatch.Draw(texture, new RectangleF(locationX, locationY, unitSize, unitSize),Color.White);
        
        }*/
        public virtual void draw()
        {
            float unitSize = (Vars.ScreenWidth * 0.83f) / 25f;
            Vars.spriteBatch.Draw(texture, new RectangleF(_locationX, _locationY, unitSize, unitSize), null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);

        }
    }
}
