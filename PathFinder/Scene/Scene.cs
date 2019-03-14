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
        private Game game;
        protected bool ready = false;
        public Game Game
        {
            get
            {
                return game;
            }
            set
            {
                game = value;
                ready = true;
            }
        }

        public Terrain Terrain { get; set; }

        public abstract  void Initialize();

        public abstract void LoadContent();

        public abstract void UnloadContent();

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(GameTime gameTime);
    }
}
