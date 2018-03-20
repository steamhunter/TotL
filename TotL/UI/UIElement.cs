using PathFinder._2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Toolkit;

namespace TotL.UI
{
    abstract class UIElement : _2DGraphicsElement
    {
        
        public int Width { get; set; }
        public int Height { get; set; }
        public string TextureName { get; set; }
        public UIElement(int x,int y,int width,int height,string textureName)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            TextureName = textureName;
        }

       
    }
}
