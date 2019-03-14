using PathFinder;
using PathFinder._2D;
using SharpDX;
using SharpDX.Toolkit.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Toolkit;
using PathFinder.Components;

namespace TotL.UI
{
    class ClusterStatus:UIElement
    {
        Texture2D active;
        Texture2D notActive;

        public ClusterStatus(int x, int y, int width, int height, string textureName) : base(x, y, width, height, textureName)
        {
            Width = width;
            Height = height;
            TextureName = textureName;
        }

        


        public override void Draw(GameTime gameTime)
        {
            Vars.spriteBatch.Draw(GetComponent<Drawer>().Texture, new RectangleF(transform.X, transform.Y, Width, Height), null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
        }
        public void Select()
        {
           GetComponent<Drawer>().Texture = active;
        }
        public void DeSelect()
        {
            GetComponent<Drawer>().Texture = notActive;
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void LoadContent()
        {
            GetComponent<Drawer>().Texture = TextureLoader.GetTexture(TextureName + "_status");
            notActive = GetComponent<Drawer>().Texture;
            active = TextureLoader.GetTexture(TextureName + "_status_selected");
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override void UnloadContent()
        {
            throw new NotImplementedException();
        }
    }
}
