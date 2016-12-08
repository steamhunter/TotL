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
        Connection[,] connect = new Connection[27,18];
        public override void Initialize()
        {
            float unitSize = (Vars.ScreenWidth * 0.83f) / 25f;
            int now = DateTime.Now.Millisecond*DateTime.Now.Second;
            Console.WriteLine(now);
            Random random = new Random(now);
            Vars.seed = random.Next(10000000,99999999);
            Console.WriteLine(Vars.seed);
            random = new Random(Vars.seed);

            for (int i = 0; i < 27; i++)
            {
                for (int j = 0; j < 18; j++)
                {
                    if (i == 0 || j == 0 || i == 26 || j == 17)
                    {
                        connect[i, j] = new Connection();
                        connect[i, j].up = false;
                        connect[i, j].down = false;
                        connect[i, j].left = false;
                        connect[i, j].right = false;
                    }
                    else
                    {
                        connect[i, j] = new Connection();
                        connect[i, j].up = true; //map[i - 1, j - 1].up;
                        connect[i, j].down = true; //map[i - 1, j - 1].down;
                        connect[i, j].left = true;// map[i - 1, j - 1].left;
                        connect[i, j].right = true;//map[i - 1, j - 1].right;
                    }
                }
            }
            int ci = 0;
            int cj = 0;
            for (int i = 0; i < 25; i++)
            {
                ci++;
                for (int j = 0; j < 15; j++)
                {
                    cj++;
                    Console.WriteLine(i+" "+j);
                    bool valid = false;
                    while (!valid)
                    {
                        switch ((random.Next(0, 3)))
                        {
                            case 0:
                                    Cell cell= new OneSideBlocked();
                                if (cell.closedsides==Connection.getClosedSides(connect[ci - 1, cj], connect[ci, cj + 1], connect[ci + 1, cj], connect[ci, cj - 1]))
                                {
                                    if (Connection.isFiting(cell, connect[ci - 1, cj], connect[ci, cj + 1], connect[ci + 1, cj], connect[ci, cj - 1]))
                                    {
                                        valid = true;
                                        connect[ci, cj].up =cell.up;
                                        connect[ci, cj].down = cell.down;
                                        connect[ci, cj].left =  cell.left;
                                        connect[ci, cj].right = cell.right;
                                    }
                                    else
                                    {
                                        cell.setRotation(Rotaitions.plus90);
                                        if (Connection.isFiting(cell, connect[ci - 1, cj], connect[ci, cj + 1], connect[ci + 1, cj], connect[ci, cj - 1]))
                                        {
                                            valid = true;
                                            connect[ci, cj].up = cell.up;
                                            connect[ci, cj].down = cell.down;
                                            connect[ci, cj].left = cell.left;
                                            connect[ci, cj].right = cell.right;
                                        }
                                        else
                                        {
                                            cell.setRotation(Rotaitions.half);
                                            if (Connection.isFiting(cell, connect[ci - 1, cj], connect[ci, cj + 1], connect[ci + 1, cj], connect[ci, cj - 1]))
                                            {
                                                valid = true;
                                                connect[ci, cj].up = cell.up;
                                                connect[ci, cj].down = cell.down;
                                                connect[ci, cj].left = cell.left;
                                                connect[ci, cj].right = cell.right;
                                            }
                                            else
                                            {
                                                cell.setRotation(Rotaitions.minus90);
                                                if (Connection.isFiting(cell, connect[ci - 1, cj], connect[ci, cj + 1], connect[ci + 1, cj], connect[ci, cj - 1]))
                                                {
                                                    valid = true;
                                                    connect[ci, cj].up = cell.up;
                                                    connect[ci, cj].down = cell.down;
                                                    connect[ci, cj].left = cell.left;
                                                    connect[ci, cj].right = cell.right;
                                                }
                                                else
                                                {
                                                    valid = false;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    
                                    
                                }
                                else
                                {
                                    valid = false;
                                    break;
                                }
                                map[i, j] = cell;
                                break;
                            case 1:
                                Cell cell2 = new CrossCell();



                                if (cell2.closedsides == Connection.getClosedSides(connect[ci - 1, cj], connect[ci, cj + 1], connect[ci + 1, cj], connect[ci, cj - 1]))
                                {
                                    if (Connection.isFiting(cell2, connect[ci - 1, cj], connect[ci, cj + 1], connect[ci + 1, cj], connect[ci, cj - 1]))
                                    {
                                        valid = true;
                                        connect[ci, cj].up = cell2.up;
                                        connect[ci, cj].down = cell2.down;
                                        connect[ci, cj].left = cell2.left;
                                        connect[ci, cj].right = cell2.right;
                                    }
                                    else
                                    {
                                        cell2.setRotation(Rotaitions.plus90);
                                        if (Connection.isFiting(cell2, connect[ci - 1, cj], connect[ci, cj + 1], connect[ci + 1, cj], connect[ci, cj - 1]))
                                        {
                                            valid = true;
                                            connect[ci, cj].up = cell2.up;
                                            connect[ci, cj].down = cell2.down;
                                            connect[ci, cj].left = cell2.left;
                                            connect[ci, cj].right = cell2.right;
                                        }
                                        else
                                        {
                                            cell2.setRotation(Rotaitions.half);
                                            if (Connection.isFiting(cell2, connect[ci - 1, cj], connect[ci, cj + 1], connect[ci + 1, cj], connect[ci, cj - 1]))
                                            {
                                                valid = true;
                                                connect[ci, cj].up = cell2.up;
                                                connect[ci, cj].down = cell2.down;
                                                connect[ci, cj].left = cell2.left;
                                                connect[ci, cj].right = cell2.right;
                                            }
                                            else
                                            {
                                                cell2.setRotation(Rotaitions.minus90);
                                                if (Connection.isFiting(cell2, connect[ci - 1, cj], connect[ci, cj + 1], connect[ci + 1, cj], connect[ci, cj - 1]))
                                                {
                                                    valid = true;
                                                    connect[ci, cj].up = cell2.up;
                                                    connect[ci, cj].down = cell2.down;
                                                    connect[ci, cj].left = cell2.left;
                                                    connect[ci, cj].right = cell2.right;
                                                }
                                                else
                                                {
                                                    valid = false;
                                                    break;
                                                }
                                            }
                                        }
                                    }


                                }
                                else
                                {
                                    valid = false;
                                    break;
                                }
                                map[i, j] = cell2;
                                break;
                            case 2:
                                Cell cell3 = new TwoSideBlocked();

                                if (cell3.closedsides == Connection.getClosedSides(connect[ci - 1, cj], connect[ci, cj + 1], connect[ci + 1, cj], connect[ci, cj - 1]))
                                {
                                    if (Connection.isFiting(cell3, connect[ci - 1, cj], connect[ci, cj + 1], connect[ci + 1, cj], connect[ci, cj - 1]))
                                    {
                                        valid = true;
                                        connect[ci, cj].up = cell3.up;
                                        connect[ci, cj].down = cell3.down;
                                        connect[ci, cj].left = cell3.left;
                                        connect[ci, cj].right = cell3.right;
                                    }
                                    else
                                    {
                                        cell3.setRotation(Rotaitions.plus90);
                                        if (Connection.isFiting(cell3, connect[ci - 1, cj], connect[ci, cj + 1], connect[ci + 1, cj], connect[ci, cj - 1]))
                                        {
                                            valid = true;
                                            connect[ci, cj].up = cell3.up;
                                            connect[ci, cj].down = cell3.down;
                                            connect[ci, cj].left = cell3.left;
                                            connect[ci, cj].right = cell3.right;
                                        }
                                        else
                                        {
                                            cell3.setRotation(Rotaitions.half);
                                            if (Connection.isFiting(cell3, connect[ci - 1, cj], connect[ci, cj + 1], connect[ci + 1, cj], connect[ci, cj - 1]))
                                            {
                                                valid = true;
                                                connect[ci, cj].up = cell3.up;
                                                connect[ci, cj].down = cell3.down;
                                                connect[ci, cj].left = cell3.left;
                                                connect[ci, cj].right = cell3.right;
                                            }
                                            else
                                            {
                                                cell3.setRotation(Rotaitions.minus90);
                                                if (Connection.isFiting(cell3, connect[ci - 1, cj], connect[ci, cj + 1], connect[ci + 1, cj], connect[ci, cj - 1]))
                                                {
                                                    valid = true;
                                                    connect[ci, cj].up = cell3.up;
                                                    connect[ci, cj].down = cell3.down;
                                                    connect[ci, cj].left = cell3.left;
                                                    connect[ci, cj].right = cell3.right;
                                                }
                                                else
                                                {
                                                    valid = false;
                                                    break;
                                                }
                                            }
                                        }
                                    }


                                }
                                else
                                {
                                    valid = false;
                                    break;
                                }
                                map[i, j] = cell3;
                                break;
                            default:
                                break;
                        }
                    }




                    /*if (i == 0 || j == 0 || i == 24 || j == 14)
                    {
                        map[i, j] = new OneSideBlocked();
                    }
                    else
                    {
                        switch ((random.Next(0, 3)))
                        {
                            case 0: map[i, j] = new OneSideBlocked();
                                break;
                            case 1: map[i, j] = new CrossCell();
                                break;
                            case 2: map[i, j] = new TwoSideBlocked();
                                break;
                            default:
                                break;
                        }
                        

                    }*/
                    /*if (i == 0 && j == 0 || i == 24 && j == 0 || i == 0 && j == 14 || i == 24 && j == 14)
                    {
                        map[i, j] = new TwoSideBlocked();
                    }*/

                    map[i, j].locationX = 20 + ((i) * unitSize);
                    map[i, j].locationY = 20 + ((j) * unitSize);
                   

                }
                cj = 0;
            }
            ci = 0;
            

        }
        public override void LoadContent()
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
                    else if (i == 24 && j != 0 && j != 14)
                    {
                        map[i, j].setRotation(Rotaitions.half);
                    }

                }
            }
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

                    map[i, j].draw();
                  
                }
            }
        }
    }
}
