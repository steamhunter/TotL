using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Toolkit;

namespace PathFinder
{
    public class GameObject:IGameObject
    {
        LinkedList<Component> components = new LinkedList<Component>();

        public void Draw(GameTime gameTime)
        {
            foreach (var item in components)
            {
                item.Draw(gameTime);
            }
        }

        public Component GetComponent(Type type)
        {
            return components.First(x => x.GetType() == type);
        }

        public void Initialize()
        {
            components.Add(new Components.Transform(this));
            foreach (var item in components)
            {
                item.Initialize();
            }
        }

        public void LoadContent()
        {
            foreach (var item in components)
            {
                item.LoadContent();
            }
        }

        public void UnloadContent()
        {
            foreach (var item in components)
            {
                item.UnloadContent();
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var item in components)
            {
                item.Update(gameTime);
            }
        }
    }
}
