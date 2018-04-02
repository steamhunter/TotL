using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Toolkit;

namespace PathFinder.Components
{
    class Transform : Component, IRotatable
    {
        public Transform(GameObject parent) : base(parent)
        {
        }

        public float Rotation { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public delegate void SetRotation(float rotation);
        public SetRotation setRotation;
        
    }
}
