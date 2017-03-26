using PathFinder;
using PathFinder._2D;
using SharpDX;
using SharpDX.Toolkit.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotL.UI
{
    class ClusterStatus:_2DGraphicsElement
    {
        string texturename;
        public ClusterStatus(String texutreName)
        {
            texture = TextureFromFile.TextureProcessor.getTexture(texutreName+"_status");
            texturename = texutreName;
        }


        public void draw(int coordinateX,int coordinateY)
        {
            Vars.spriteBatch.Draw(texture, new RectangleF(coordinateX, coordinateY, 128, 128), null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);
        }
        public void select()
        {
            texture = TextureFromFile.TextureProcessor.getTexture(texturename + "_status_selected");
        }
        public void deSelect()
        {
            texture = TextureFromFile.TextureProcessor.getTexture(texturename + "_status");
        }
    }
}
