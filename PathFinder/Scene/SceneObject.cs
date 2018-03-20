using PathFinder._2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Toolkit;

namespace PathFinder.Scene
{
    public abstract class SceneObject : _2DGraphicsElement, IRotatable
    {
        public float Rotation { get; set; }

        public abstract void SetRotation(float rotation);
    }
}
