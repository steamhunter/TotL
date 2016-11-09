using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFinder.Map;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
using PathFinder._2D;
using PathFinder;

namespace TotL
{
    class mapArea: AreaBase
    {
        Unit2D[,] map= new Unit2D[25,18];
        public override void Initialize()
        {
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 18; j++)
                {
                    map[i, j] = new Unit2D("CrossCell", game);
                    map[i, j].locationX = i * 32;
                    map[i, j].locationY = j * 32;
                }
            }
            
        }
        public override void LoadContent()
        {
            
        }
        public override void Update(GameTime gameTime)
        {
            
        }
        public override void Draw(GameTime gameTime)
        {
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 18; j++)
                {
                    map[i, j].draw(Vars.spriteBatch);
                }
            }
        }
    }
}
