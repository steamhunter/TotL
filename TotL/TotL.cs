using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFinder.Toolkit;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Input;
using PathFinder.Toolkit.Graphics;
using PathFinder;
using SharpDX;

namespace TotL
{
    class TotL : PathFinderGame
    {
        public TotL()
        {
            deviceManager = new GraphicsDeviceManager(this);
            deviceManager.PreferredBackBufferWidth = 600;
            deviceManager.PreferredBackBufferHeight = 800;
            Mykeyboardmanager = new KeyboardManager(this);
            Mymousemanager = new MouseManager(this);
            Content.RootDirectory = "Content";
            PathFinder.Error.error.game = this;
        }
        protected override void LoopInitialize()
        {
           
            Mykeyboardmanager.Initialize();
            Mymousemanager.Initialize();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            deviceManager.IsFullScreen = false;
            Vars.device = deviceManager.GraphicsDevice;
            Vars.spriteBatch = spriteBatch;
#pragma warning disable CS0618 // Type or member is obsolete
            deviceManager.PreferredBackBufferWidth = GraphicsDevice.Adapter.DesktopBounds.Width;
            deviceManager.PreferredBackBufferHeight = GraphicsDevice.Adapter.DesktopBounds.Height;
#pragma warning restore CS0618 // Type or member is obsolete

            IsMouseVisible = true;
        }
       
        protected override void Load()
        {

        }
        protected override void TickDraw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();


            spriteBatch.End();
        }
        protected override void TickUpdate(GameTime gameTime)
        {

        }
    }
}
