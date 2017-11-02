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
using PathFinder._2D;
using TotL.Maps;
using PathFinder.Map;

namespace TotL
{
    class TotL : PathFinderGame
    {
       // protected Map map = new LabyrinthMap();
        protected Map map = new LabyrinthMap();





        public TotL()
        {
            deviceManager = new GraphicsDeviceManager(this);
            Mykeyboardmanager = new KeyboardManager(this);
            Mymousemanager = new MouseManager(this);
            Content.RootDirectory = "Content";
            PathFinder.Error.error.game = this;
        }

        protected override void Init()
        {
            Vars.config = configjson.getConfig();
            Mykeyboardmanager.Initialize();
            Mymousemanager.Initialize();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            deviceManager.IsFullScreen =Vars.config.isFullScreen;
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
            Vars.cellSize = (Vars.ScreenWidth * 0.83f) / 25f;
            cons.debugMessage(Vars.cellSize.ToString(),"TOTL init");
#pragma warning restore CS0618 // Type or member is obsolete
            deviceManager.ApplyChanges();
            IsMouseVisible = true;

            map.Initialize();
            

        }
       
        protected override void Load()
        {
            if (!Vars.noTextMode)
            {
                Vars.font = Content.Load<SharpDX.Toolkit.Graphics.SpriteFont>("myfont");
                map.LoadContent();
            }
            
        }
        protected override void TickDraw(GameTime gameTime)
        {

            spriteBatch.Begin();

            GraphicsDevice.Clear(Color.DarkGray);
            
            map.Draw(gameTime);

            spriteBatch.End();
        }
        protected override void TickUpdate(GameTime gameTime)
        {
                if (Mykeyboardmanager.GetState().IsKeyDown(Keys.E))
                {
                    Exit();
                }
                /*if (Vars.mapstate == internalstates.map_not_initialized)
                {
                    map.Initialize();
                }*/
                if (Vars.mapstate == internalstates.map_ready || Vars.mapstate == internalstates.on_map)
                {
                    map.Update(gameTime);
                } 
        }
    }
}
