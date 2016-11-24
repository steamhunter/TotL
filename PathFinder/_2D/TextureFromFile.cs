using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder._2D
{
    public class TextureFromFile
    {
        public class TextureProcessor
        {
            private struct Sicon
            {
                public string texturename;
                public ShaderResourceView ico;
            }
            static List<Sicon> icons = new List<Sicon>();
            public static ShaderResourceView getTexture( string texturename)
            {
              
                foreach (var item in icons)
                {
                    if (item.texturename ==texturename)
                    {
                        return item.ico;
                    }
                }

                ShaderResourceView res = ShaderResourceView.FromFile(Vars.device, "Content\\" + texturename + ".png");
                Sicon tmp = new Sicon();
                tmp.ico = res;
                tmp.texturename = texturename;
                icons.Add(tmp);
                return res;
            }
        }
    }
}
