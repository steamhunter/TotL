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
        protected mapArea map = new mapArea();
        protected override void LoopInitialize()
        {
           
            Mykeyboardmanager.Initialize();
            Mymousemanager.Initialize();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            deviceManager.IsFullScreen =true;
            Vars.device = deviceManager.GraphicsDevice;
            Vars.spriteBatch = spriteBatch;
            Vars.game = this;
            Vars.mykeyboardmanager = Mykeyboardmanager;
            Vars.mymousemanager = Mymousemanager;
#pragma warning disable CS0618 // Type or member is obsolete
            deviceManager.PreferredBackBufferWidth = GraphicsDevice.Adapter.DesktopBounds.Width;
            deviceManager.PreferredBackBufferHeight = GraphicsDevice.Adapter.DesktopBounds.Height;
            Vars.ScreenWidth =  GraphicsDevice.Adapter.DesktopBounds.Width;
            Vars.ScreenHeight = GraphicsDevice.Adapter.DesktopBounds.Height;
            Vars.unitSize = (Vars.ScreenWidth * 0.83f) / 25f;
            cons.debugMessage(Vars.unitSize.ToString());
#pragma warning restore CS0618 // Type or member is obsolete
            deviceManager.ApplyChanges();
            IsMouseVisible = true;
            

        }
       
        protected override void Load()
        {
            if (!Vars.noTextMode)
            {
                Vars.font = Content.Load<SharpDX.Toolkit.Graphics.SpriteFont>("myfont");
            }
            
            
        }
        int loadwaitcounter = 0;
        protected override void TickDraw(GameTime gameTime)
        {

            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.DarkGray);

            if (Vars.mapstate == internalstates.map_ready || Vars.mapstate == internalstates.on_map)
            {
               
                map.Draw(gameTime);
            }
            else
            {
                spriteBatch.DrawString(Vars.font, "betöltés", new Vector2(Vars.ScreenWidth/2-50,Vars.ScreenHeight/2-50), Color.Black);
            }
           
            spriteBatch.End();
        }
        protected override void TickUpdate(GameTime gameTime)
        {


            
                if (Mykeyboardmanager.GetState().IsKeyDown(Keys.E))
                {
                    Exit();
                }
                if (Vars.mapstate == internalstates.map_initializing || loadwaitcounter <= 300)
                {
                    loadwaitcounter++;
                }
                if (Vars.mapstate == internalstates.map_not_initialized)
                {
                    map.Initialize();
                }
                if ((Vars.mapstate == internalstates.map_ready || Vars.mapstate == internalstates.on_map) && loadwaitcounter > 300)
                {
                    map.Update(gameTime);
                }
           
            
            
            
        }
    }
}
