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
    public abstract class _2DGraphicsElement : IGameObject
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

        public abstract void Update(GameTime gameTime);

        public abstract void LoadContent();

        public abstract void Initialize();


        public virtual void Draw(GameTime gameTime)
        {
            
            float unitSize = (Vars.ScreenWidth * 0.83f) / 25f;
            Vars.spriteBatch.Draw(texture, new RectangleF(_locationX, _locationY, unitSize, unitSize), null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
        }

        public abstract void UnloadContent();

    }
}

