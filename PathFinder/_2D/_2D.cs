using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder._2D
{
    public class Unit2D
    {
        ShaderResourceView texture;
        public Rectangle rectangle;
        public int locationX;
        public int locationY;
        
        
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
       
        public Unit2D(string texturename, Game game)
        {
            texture = TextureFromFile.TextureProcessor.getTexture(texturename);


        }
        public void update()
        {

            

        }
        public void draw(SpriteBatch spritebatch)
        {

           
            //spritebatch.Draw(texture, new Vector2(locationX, locationY), null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
            spritebatch.Draw(texture, new RectangleF(locationX, locationY, 32, 32), Color.CornflowerBlue);
        }
        public void draw(SpriteBatch spritebatch, float rotation)
        {
            spritebatch.Draw(texture, new Vector2(locationX, locationY), null, Color.White, rotation, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
        }
    }
}
