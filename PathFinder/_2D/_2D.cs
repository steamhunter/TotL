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
    public class Unit2D
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

        public Unit2D()
        {
           


        }
       
        public virtual void update()
        {

            

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
