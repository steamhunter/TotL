using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PathFinder.Map
{
    public abstract class AreaBase
    {
        private Game igame;
        protected bool ready = false;
        public Game game
        {
            get
            {
                return igame;
            }
            set
            {
                igame = value;
                ready = true;
            }
        }
        public virtual void Initialize()
        {

        }
        public virtual void LoadContent()
        {

        }
        public virtual void UnloadContent()
        {

        }
        public virtual void Update(GameTime gameTime)
        {

        }
        public virtual void Draw(GameTime gameTime)
        {

        }
    }
}
