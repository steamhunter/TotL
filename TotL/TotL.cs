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
using PathFinder.Debug;

namespace TotL
{
    class TotL : PathFinderGame
    {
        public TotL()
        {
            deviceManager = new GraphicsDeviceManager(this);
          //  deviceManager.PreferredBackBufferWidth = 800;
           // deviceManager.PreferredBackBufferHeight = 600;
            Mykeyboardmanager = new KeyboardManager(this);
            Mymousemanager = new MouseManager(this);
            Content.RootDirectory = "Content";
            PathFinder.Error.error.game = this;
        }

        int unixTimestamp;
        protected mapArea map = new mapArea();
        protected override void LoopInitialize()
        {

            unixTimestamp = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            Mykeyboardmanager.Initialize();
            Mymousemanager.Initialize();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            deviceManager.IsFullScreen = true;
            Vars.device = deviceManager.GraphicsDevice;
            Vars.spriteBatch = spriteBatch;
            Vars.game = this;
#pragma warning disable CS0618 // Type or member is obsolete
            deviceManager.PreferredBackBufferWidth = GraphicsDevice.Adapter.DesktopBounds.Width;
            deviceManager.PreferredBackBufferHeight = GraphicsDevice.Adapter.DesktopBounds.Height;
            Vars.ScreenWidth =  GraphicsDevice.Adapter.DesktopBounds.Width;
            Vars.ScreenHeight = GraphicsDevice.Adapter.DesktopBounds.Height;
#pragma warning restore CS0618 // Type or member is obsolete
            deviceManager.ApplyChanges();
            IsMouseVisible = true;
            map.Initialize();

        }
       
        protected override void Load()
        {
            map.LoadContent();
        }
        protected override void TickDraw(GameTime gameTime)
        {
            if (Vars.state != gamestates.onreset)
            {
                spriteBatch.Begin();
                GraphicsDevice.Clear(Color.CornflowerBlue);


                map.Draw(gameTime);
                spriteBatch.End();
            }
        }
        protected override void TickUpdate(GameTime gameTime)
        {
            if (Mykeyboardmanager.GetState().IsKeyDown(Keys.E))
            {
                Exit();
            }


            map.Update(gameTime);
        }
    }
}
