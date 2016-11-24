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
using TotL.labyrinthcells;

namespace TotL
{
    class mapArea: AreaBase
    {
        Cell[,] map= new Cell[25,15];
        public override void Initialize()
        {
            float unitSize = (Vars.ScreenWidth * 0.83f) / 25f;
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                   
                    if (i == 0 || j == 0 || i == 24 || j == 14)
                    {
                        map[i, j] = new OneSideBlocked();
                    }
                    else
                    {
                        map[i, j] = new CrossCell(game);
                    }
                    if (i == 0 && j == 0 || i == 24 && j == 0 || i == 0 && j == 14 || i == 24 && j == 14)
                    {
                        map[i, j] = new TwoSideBlocked();
                    }




                    if (i != 24 && j == 0)
                    {
                        map[i, j].locationX = 20 + ((i + 1) * unitSize);
                        map[i, j].locationY = 20 + (j * unitSize);
                    }
                    else if (j == 0 && i == 24)
                    {
                        map[i, j].locationX = 20 + ((i + 1) * unitSize);
                        map[i, j].locationY = 20 + ((j + 1) * unitSize);
                    }
                    else if(j == 14 && i != 0)
                    {
                        map[i, j].locationX = 20 + ((i) * unitSize);
                        map[i, j].locationY = 20 + ((j+1) * unitSize);
                    }
                    else if (i == 24 && j != 0 && j != 14)
                    {
                        map[i, j].locationX = 20 + ((i+1) * unitSize);
                        map[i, j].locationY = 20 + ((j+1) * unitSize);
                    }else 
                    {
                        map[i, j].locationX = 20 + ((i) * unitSize);
                        map[i, j].locationY = 20 + ((j) * unitSize);
                    }

                }
            }
            
        }
        public override void LoadContent()
        {
            
        }
        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (j == 0 && i != 24)
                    {
                        map[i, j].setRotation(Rotaitions.plus90);
                    }
                    else if (j == 0 && i == 24)
                    {
                        map[i, j].setRotation(Rotaitions.half);
                    }
                    else if (j == 14 && i != 0)
                    {
                        map[i, j].setRotation(Rotaitions.minus90);
                    }
                    else if (i == 24 && j != 0 && j!=14)
                    {
                        map[i, j].setRotation(Rotaitions.half);
                    }

                }
            }
        }
        public override void Draw(GameTime gameTime)
        {
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 15; j++)
                {

                    map[i, j].draw();
                  
                }
            }
        }
    }
}
