using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.Direct3D11;
using System.IO;
using Tex2D = SharpDX.Toolkit.Graphics.Texture2D;

namespace PathFinder._2D
{
    public class TextureLoader
    {
        private struct Stexture
        {
            public string textureName;
            public Tex2D texture;
        }
        static List<Stexture> textures = new List<Stexture>();
        public static Tex2D GetTexture(string textureName)
        {
            
            foreach (var item in textures)
            {
                if (item.textureName == textureName)
                {
                    return item.texture;
                }
            }

            Tex2D res = Vars.game.Content.Load<Tex2D>(textureName); 
            Stexture tmp = new Stexture();
            tmp.texture = res;
            tmp.textureName = textureName;
            textures.Add(tmp);
            return res;
        }
    }
}
