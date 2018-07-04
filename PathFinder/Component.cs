using SharpDX.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder
{

    public class Component : IComponent
    {
        GameObject parent;
        public Component(GameObject parent)
        {
            this.parent = parent;
        }
        public T GetComponent<T>() where T:IComponent
        {
            return parent.GetComponent<T>();
        }

        public virtual void Draw(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public virtual void Initialize()
        {
            throw new NotImplementedException();
        }

        public virtual void LoadContent()
        {
            throw new NotImplementedException();
        }

        public virtual void UnloadContent()
        {
            throw new NotImplementedException();
        }

        public virtual void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
