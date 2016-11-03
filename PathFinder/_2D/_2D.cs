using SharpDX;

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
        Texture2D texture;
        public Rectangle rectangle;
        public int locationX;
        public int locationY;
        
        
        public static Texture2D getTexture(string entity, Game game)
        {

            switch (entity)
            {
                case "player": return game.Content.Load<Texture2D>("player");
                case "grass": return game.Content.Load<Texture2D>("terraincell");
                case "enemy": return game.Content.Load<Texture2D>("enemy");
                    


                default: return null;

            }

        }
        public Unit2D(string entity, Game game)
        {
            texture = getTexture(entity, game);
            

        }
        public void update()
        {

            

        }
        public void draw(SpriteBatch spritebatch)
        {

           
            spritebatch.Draw(texture, new Vector2(locationX, locationY), null, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
        }
        public void draw(SpriteBatch spritebatch, float rotation)
        {
            spritebatch.Draw(texture, new Vector2(locationX, locationY), null, Color.White, rotation, new Vector2(0, 0), 1f, SpriteEffects.None, 0);
        }
    }
}
