﻿using System;
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
        Unit2D[,] map= new Unit2D[25,15];
        public override void Initialize()
        {
            float unitSize = (Vars.ScreenWidth * 0.83f) / 25f;
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (i == 0 || j == 0 || i == 24 || j == 14)
                    {
                        map[i, j] = new Unit2D("oneSideBlockedCell", game);
                    }
                    else
                    {
                        map[i, j] = new Unit2D("CrossCell", game);
                    }
                    if (j == 14)
                    {
                        map[i, j].locationX = 20 + (i * unitSize);
                        map[i, j].locationY = 20 + ((j + 1) * unitSize);
                    }
                    else if (i == 24 && j != 0)
                    {
                        map[i, j].locationX = 20 + ((i + 1) * unitSize);
                        map[i, j].locationY = 20 + ((j + 1) * unitSize);
                    } else if (i == 24 && j == 0)
                    {
                        map[i, j].locationX = 20 + ((i + 1) * unitSize);
                        map[i, j].locationY = 20 + (j  * unitSize);
                    }
                    else if (j == 0 && i != 0)
                    {
                        map[i, j].locationX = 20 + ((i + 1) * unitSize);
                        map[i, j].locationY = 20 + (j * unitSize);
                    }
                    else
                    {
                        map[i, j].locationX = 20 + (i * unitSize);
                        map[i, j].locationY = 20 + (j * unitSize);
                    }
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
                for (int j = 0; j < 15; j++)
                {
                    if (j == 0 && i > 0)
                    {
                        map[i, j].draw(Vars.spriteBatch, Convert.ToSingle(Math.PI) / 2);
                    } else if (j == 14)
                    {
                        map[i, j].draw(Vars.spriteBatch, -Convert.ToSingle(Math.PI) / 2);
                    } else if (i==24)
                    {
                        map[i, j].draw(Vars.spriteBatch, Convert.ToSingle(Math.PI));
                    }
                    else
                    {
                        map[i, j].draw(Vars.spriteBatch);
                    }
                }
            }
        }
    }
}
