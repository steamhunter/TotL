using PathFinder._2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Toolkit;
using PathFinder;
using PathFinder.Components;

namespace TotL.UI
{
    abstract class UIElement : GameObject
    {
        
        public int Width { get; set; }
        public int Height { get; set; }
        public string TextureName { get; set; }
        public Transform transform;
        public UIElement(int x,int y,int width,int height,string textureName)
        {
            transform = GetComponent<Transform>();
            AddComponent(new Drawer(parent));
            transform.X = x;
            transform.Y = y;
            Width = width;
            Height = height;
            TextureName = textureName;
        }

       
    }
}
