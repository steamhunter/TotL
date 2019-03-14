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

namespace PathFinder.Components
{
    public class Drawer:Component
    {
        public Tex2D Texture { get; set; }
        public Rectangle Rectangle { get; set; }



        public static Tex2D GetTexture(string entity, Game game)
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

        public Drawer(GameObject parent):base(parent) {}



        public override void Draw(GameTime gameTime)
        {
            Transform transform=GetComponent<Transform>();
            float unitSize = (Vars.ScreenWidth * 0.83f) / 25f;
            Vars.spriteBatch.Draw(Texture, new RectangleF(transform.X, transform.Y, unitSize, unitSize), null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
        }


    }
}

