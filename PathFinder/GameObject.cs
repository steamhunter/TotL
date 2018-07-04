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
        public GameObject parent;
        public GameObject()
        {
            parent = this;
            Components.Add(new Components.Transform(this));
        }

        internal LinkedList<IComponent> Components { get; private set; } = new LinkedList<IComponent>();

        public virtual void Draw(GameTime gameTime)
        {
            foreach (var item in Components)
            {
                item.Draw(gameTime);
            }
        }

        public T GetComponent<T>() where T:IComponent
        {
            return (T)Components.First(x => x.GetType().Equals(typeof(T)));
        }

        public void AddComponent<T>(T comp) where T : IComponent
        {
            if (Components.Count(x => x.GetType() == typeof(T))==0)
            {
                Components.Add(comp);
            }
        }

        public virtual void Initialize()
        {
           
            foreach (var item in Components)
            {
                item.Initialize();
            }
        }

        public virtual void LoadContent()
        {
            foreach (var item in Components)
            {
                item.LoadContent();
            }
        }

        public virtual void UnloadContent()
        {
            foreach (var item in Components)
            {
                item.UnloadContent();
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (var item in Components)
            {
                item.Update(gameTime);
            }
        }
    }
}
