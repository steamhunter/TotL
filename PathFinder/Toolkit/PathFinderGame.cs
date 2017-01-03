using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
using SharpDX;
using PathFinder;
using SharpDX.Direct3D11;
using SharpDX.Toolkit.Input;

namespace PathFinder.Toolkit
{
   public class PathFinderGame: Game
    {
        protected GraphicsDeviceManager deviceManager;
        protected KeyboardManager Mykeyboardmanager;
        protected MouseManager Mymousemanager;
        protected Graphics.SpriteBatch spriteBatch;
        protected override void Initialize()
        {
            Vars.state = gamestates.initializing;
           
                LoopInitialize();
          
            Vars.state = gamestates.initialized_notLoaded;
            base.Initialize();
        }
        protected virtual void LoopInitialize()
        {

        }
        protected override void LoadContent()
        {

            if (Vars.state==gamestates.initialized_notLoaded)
            {
                
                    Load();
                Vars.state = gamestates.intitalized_loaded;
            }

            base.LoadContent();
        }
        protected virtual void Load()
        {

        }
        protected override void Draw(GameTime gameTime)
        {
            if (Vars.state==gamestates.intitalized_loaded)
            {
              
                    TickDraw(gameTime);

                base.Draw(gameTime);
            }
            
           
        }
        protected virtual void TickDraw(GameTime gameTime)
        {

        }
        protected override void Update(GameTime gameTime)
        {

            if (Vars.state==gamestates.intitalized_loaded)
            {


                    TickUpdate(gameTime);
                   
            }
            base.Update(gameTime);
        }
        protected virtual void TickUpdate(GameTime gameTime)
        {
        }
    }
   
}
