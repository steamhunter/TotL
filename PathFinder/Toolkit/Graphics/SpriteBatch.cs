using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Toolkit.Graphics;

namespace PathFinder.Toolkit.Graphics
{
     public class SpriteBatch : SharpDX.Toolkit.Graphics.SpriteBatch
    {
        public SpriteBatch(GraphicsDevice graphicsDevice, int batchCapacity = 2048) : base(graphicsDevice, batchCapacity)
        {
        }
    }
}
