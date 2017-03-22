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

        /// <summary>
        /// intialize a SharpDX irányából megvalósítja az állapot elenőrzést.
        /// gyermek osztályból override old a LoopInitialize t.
        /// </summary>
        protected override void Initialize()
        {
            Vars.state = gamestates.initializing;
           
                LoopInitialize();
          
            Vars.state = gamestates.initialized_notLoaded;
            base.Initialize();
        }

        /// <summary>
        /// initialize a PathFinder motor irányából.
        /// gyermek osztályból ezt override old.
        /// </summary>
        protected virtual void LoopInitialize()
        {

        }

        /// <summary>
        /// Load a SharpDX irányából megvalósítja az állapot elenőrzést.
        /// gyermek osztályból override old a a Load ot.
        /// </summary>
        protected override void LoadContent()
        {

            if (Vars.state==gamestates.initialized_notLoaded)
            {
                
                    Load();
                Vars.state = gamestates.intitalized_loaded;
            }

            base.LoadContent();
        }

        /// <summary>
        /// load a PathFinder motor irányából
        /// gyermek osztályból ezt override old.
        /// </summary>
        protected virtual void Load()
        {

        }

        /// <summary>
        /// draw a SharpDX irányából megvalósítja az állapot elenőrzést.
        /// gyermek osztályból a TickDraw t override old.
        /// </summary>
        /// <param name="gameTime">Sharpdx.toolkit.GameTime</param>
        protected override void Draw(GameTime gameTime)
        {
            if (Vars.state==gamestates.intitalized_loaded)
            {
              
                    TickDraw(gameTime);

                base.Draw(gameTime);
            }
            
           
        }

        /// <summary>
        /// Draw a PathFinder motor irányából
        /// gyermek osztályból ezt override old.
        /// </summary>
        /// <param name="gameTime">Sharpdx.toolkit.GameTime</param>
        protected virtual void TickDraw(GameTime gameTime)
        {

        }

        /// <summary>
        /// Update SharpDX irányából  megvalósítja az állapot elenőrzést.
        /// gyermek osztályból a TickUpdate ot override old.
        /// </summary>
        /// <param name="gameTime">Sharpdx.toolkit.GameTime</param>
        protected override void Update(GameTime gameTime)
        {

            if (Vars.state==gamestates.intitalized_loaded)
            {

                /*Task.Run(() =>
                {*/
                    TickUpdate(gameTime);
                   
                //});
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// Update a PathFinder motor irányából 
        /// gyermek osztályból ezt override old.
        /// </summary>
        /// <param name="gameTime">Sharpdx.toolkit.GameTime</param>
        protected virtual void TickUpdate(GameTime gameTime)
        {
        }
    }
   
}
