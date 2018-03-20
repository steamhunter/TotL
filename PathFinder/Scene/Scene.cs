using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PathFinder.Scene
{
    public abstract class Scene:IGameObject
    {
        public Terrain terrain;
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
        public abstract  void Initialize();

        public abstract void LoadContent();

        public abstract void UnloadContent();

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime);
    }
}
