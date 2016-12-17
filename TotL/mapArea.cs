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
using PathFinder.Debug;

namespace TotL
{
    class mapArea: AreaBase
    {
        Cell[,] map= new Cell[15,25];
        Connection[,] connect = new Connection[18,27];
        public override void Initialize()
        {
            float unitSize = (Vars.ScreenWidth * 0.83f) / 25f;
            int now = DateTime.Now.Millisecond*DateTime.Now.Second;
            Console.WriteLine(now);
            Random random = new Random(now);
            Vars.seed = random.Next(10000000,99999999);
            Console.WriteLine(Vars.seed);
            random = new Random(Vars.seed);

            for (int s = 0; s < 18; s++)
            {
                for (int o = 0; o < 27; o++)
                {
                    if (s == 0 || o == 0 || o == 26 || s == 16)
                    {
                        connect[s,o] = new Connection();
                        connect[s,o].up = false;
                        connect[s,o].down = false;
                        connect[s,o].left = false;
                        connect[s,o].right = false;
                        connect[s, o].closedsides = 4;
                    }
                    else
                    {
                        connect[s,o] = new Connection();
                        connect[s, o].closedsides = 0;
                        connect[s, o].up = true; //map[i - 1, j - 1].up;
                        connect[s, o].down = true; //map[i - 1, j - 1].down;
                        connect[s, o].left = true;// map[i - 1, j - 1].left;
                        connect[s, o].right = true;//map[i - 1, j - 1].right;
                        connect[s, o].isPopulated = false;
                    }
                }
            }
            int co = 0;
            int cs = 0;
            for (int s = 0; s < 15; s++)
            {
                cs++;
                for (int o = 0; o < 25; o++)
                {
                    co++;
                    cons.groupedMessage(s + " " +o , "TERGEN");

                    if (s==14&&o==2)
                    {
                        cons.infoMessage("break point");
                    }
                    bool valid = false;
                    while (!valid)
                    {
                        Cell cell;
                        if (s > 0 &&s!=14&& o == 0)
                        {
                            cell = new OneSideBlocked();
                            map[s,o] = cell;
                            map[s,o].locationX = 20 + ((o) * unitSize);
                            map[s,o].locationY = 20 + ((s) * unitSize);
                            valid = true;
                            break;
                        }
                        if (s==14&&o==0)
                        {
                            cell = new TwoSideBlocked();
                            map[s, o] = cell;
                            map[s, o].locationX = 20 + ((o) * unitSize);
                            map[s, o].locationY = 20 + ((s) * unitSize);
                            valid = true;
                            break;
                        }
                       
                        switch (random.Next(0,3))
                        {
                            case 0: cell = new CrossCell();
                                if (cell.closedsides == Connection.getClosedSides(connect[cs-1,co], connect[cs, co+1], connect[cs+1, co], connect[cs, co-1]))
                                {
                                    if (Connection.isFiting(cell, connect[cs - 1, co], connect[cs, co + 1], connect[cs + 1, co], connect[cs, co - 1]))
                                    {
                                        cell.locationX = 20 + ((o) * unitSize);
                                        cell.locationY = 20 + ((s) * unitSize);
                                        valid = true;
                                        connect[cs,co].up = cell.up;
                                        connect[cs, co].down = cell.down;
                                        connect[cs, co].left = cell.left;
                                        connect[cs, co].right = cell.right;
                                        
                                    }
                                    else
                                    {
                                        cell.setRotation(Rotaitions.plus90);
                                        if (Connection.isFiting(cell,connect[cs - 1, co], connect[cs, co + 1], connect[cs + 1, co], connect[cs, co - 1]))
                                        {
                                            cell.locationX = 20 + ((o) * unitSize);
                                            cell.locationY = 20 + ((s) * unitSize);
                                            valid = true;
                                            connect[cs, co].up = cell.up;
                                            connect[cs, co].down = cell.down;
                                            connect[cs, co].left = cell.left;
                                            connect[cs, co].right = cell.right;
                                        }
                                        else
                                        {
                                            cell.setRotation(Rotaitions.half);
                                            if (Connection.isFiting(cell, connect[cs - 1, co], connect[cs, co + 1], connect[cs + 1, co], connect[cs, co - 1]))
                                            {
                                                cell.locationX = 20 + ((o) * unitSize);
                                                cell.locationY = 20 + ((s) * unitSize);
                                                valid = true;
                                                connect[cs, co].up = cell.up;
                                                connect[cs, co].down = cell.down;
                                                connect[cs, co].left = cell.left;
                                                connect[cs, co].right = cell.right;
                                            }
                                            else
                                            {
                                                cell.setRotation(Rotaitions.minus90);
                                                if (Connection.isFiting(cell,connect[cs - 1, co], connect[cs, co + 1], connect[cs + 1, co], connect[cs, co - 1]))
                                                {
                                                    cell.locationX = 20 + ((o) * unitSize);
                                                    cell.locationY = 20 + ((s) * unitSize);
                                                    valid = true;
                                                    connect[cs, co].up = cell.up;
                                                    connect[cs, co].down = cell.down;
                                                    connect[cs, co].left = cell.left;
                                                    connect[cs, co].right = cell.right;
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
                                map[s,o] = cell;
                              
                                break;
                            case 1:
                                cell = new OneSideBlocked();
                                if (cell.closedsides == Connection.getClosedSides(connect[cs - 1, co], connect[cs, co + 1], connect[cs + 1, co], connect[cs, co - 1]))
                                {
                                    if (Connection.isFiting(cell, connect[cs - 1, co], connect[cs, co + 1], connect[cs + 1, co], connect[cs, co - 1]))
                                    {
                                        cell.locationX = 20 + ((o) * unitSize);
                                        cell.locationY = 20 + ((s) * unitSize);
                                        valid = true;
                                        connect[cs, co].up = cell.up;
                                        connect[cs, co].down = cell.down;
                                        connect[cs, co].left = cell.left;
                                        connect[cs, co].right = cell.right;
                                    }
                                    else
                                    {
                                        cell.setRotation(Rotaitions.plus90);
                                        if (Connection.isFiting(cell, connect[cs - 1, co], connect[cs, co + 1], connect[cs + 1, co], connect[cs, co - 1]))
                                        {
                                            cell.locationX = 20 + ((o) * unitSize);
                                            cell.locationY = 20 + ((s) * unitSize);
                                            valid = true;
                                            connect[cs, co].up = cell.up;
                                            connect[cs, co].down = cell.down;
                                            connect[cs, co].left = cell.left;
                                            connect[cs, co].right = cell.right;
                                        }
                                        else
                                        {
                                            cell.setRotation(Rotaitions.half);
                                            if (Connection.isFiting(cell, connect[cs - 1, co], connect[cs, co + 1], connect[cs + 1, co], connect[cs, co - 1]))
                                            {
                                                cell.locationX = 20 + ((o) * unitSize);
                                                cell.locationY = 20 + ((s) * unitSize);
                                                valid = true;
                                                connect[cs, co].up = cell.up;
                                                connect[cs, co].down = cell.down;
                                                connect[cs, co].left = cell.left;
                                                connect[cs, co].right = cell.right;
                                            }
                                            else
                                            {
                                                cell.setRotation(Rotaitions.minus90);
                                                if (Connection.isFiting(cell, connect[cs - 1, co], connect[cs, co + 1], connect[cs + 1, co], connect[cs, co - 1]))
                                                {
                                                    cell.locationX = 20 + ((o) * unitSize);
                                                    cell.locationY = 20 + ((s) * unitSize);
                                                    valid = true;
                                                    connect[cs, co].up = cell.up;
                                                    connect[cs, co].down = cell.down;
                                                    connect[cs, co].left = cell.left;
                                                    connect[cs, co].right = cell.right;
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
                                map[s,o] = cell;
                                
                                break;
                            case 2:
                                cell = new TwoSideBlocked();
                                if (cell.closedsides == Connection.getClosedSides(connect[cs - 1, co], connect[cs, co + 1], connect[cs + 1, co], connect[cs, co - 1]))
                                {
                                    if (Connection.isFiting(cell, connect[cs - 1, co], connect[cs, co + 1], connect[cs + 1, co], connect[cs, co - 1]))
                                    {
                                        cell.locationX = 20 + ((o) * unitSize);
                                        cell.locationY = 20 + ((s) * unitSize);
                                        valid = true;
                                        connect[cs, co].up = cell.up;
                                        connect[cs, co].down = cell.down;
                                        connect[cs, co].left = cell.left;
                                        connect[cs, co].right = cell.right;
                                    }
                                    else
                                    {
                                        cell.setRotation(Rotaitions.plus90);
                                        if (Connection.isFiting(cell, connect[cs - 1, co], connect[cs, co + 1], connect[cs + 1, co], connect[cs, co - 1]))
                                        {
                                            cell.locationX = 20 + ((o) * unitSize);
                                            cell.locationY = 20 + ((s) * unitSize);
                                            valid = true;
                                            connect[cs, co].up = cell.up;
                                            connect[cs, co].down = cell.down;
                                            connect[cs, co].left = cell.left;
                                            connect[cs, co].right = cell.right;
                                        }
                                        else
                                        {
                                            cell.setRotation(Rotaitions.half);
                                            if (Connection.isFiting(cell, connect[cs - 1, co], connect[cs, co + 1], connect[cs + 1, co], connect[cs, co - 1]))
                                            {
                                                cell.locationX = 20 + ((o) * unitSize);
                                                cell.locationY = 20 + ((s) * unitSize);
                                                valid = true;
                                                connect[cs, co].up = cell.up;
                                                connect[cs, co].down = cell.down;
                                                connect[cs, co].left = cell.left;
                                                connect[cs, co].right = cell.right;
                                            }
                                            else
                                            {
                                                cell.setRotation(Rotaitions.minus90);
                                                if (Connection.isFiting(cell, connect[cs - 1, co], connect[cs, co + 1], connect[cs + 1, co], connect[cs, co - 1]))
                                                {
                                                    cell.locationX = 20 + ((o) * unitSize);
                                                    cell.locationY = 20 + ((s) * unitSize);
                                                    valid = true;
                                                    connect[cs, co].up = cell.up;
                                                    connect[cs, co].down = cell.down;
                                                    connect[cs, co].left = cell.left;
                                                    connect[cs, co].right = cell.right;
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
                                map[s,o] = cell;
                               
                                break;
                            default:
                                break;
                        }
                       
                    }
                }
                co = 0;
            }
            cs = 0;
            co = 0;

           /* for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    map[j, i] = new OneSideBlocked();
                     map[j,i].locationX = 20 + ((j) * unitSize);
                     map[j,i].locationY = 20 + ((i) * unitSize);
                }
            }*/
                  
                   

            

        }
        public override void LoadContent()
        {
           /* for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 25; j++)
                {
                    if (i == 0 && j != 24)
                    {
                        map[j, i].setRotation(Rotaitions.plus90);
                    }
                    else if (i == 0 && j == 24)
                    {
                        map[j, i].setRotation(Rotaitions.half);
                    }
                    else if (i == 14 && j != 0)
                    {
                        map[j, i].setRotation(Rotaitions.minus90);
                    }
                    else if (j == 24 && i != 0 && i != 14)
                    {
                        map[j, i].setRotation(Rotaitions.half);
                    }

                }
            }*/
        }
        public override void Update(GameTime gameTime)
        {
           
        }
        public override void Draw(GameTime gameTime)
        {
            for (int s = 0; s < 15; s++)
            {
                for (int o = 0; o < 25; o++)
                {

                    map[s,o].draw();
                  
                }
            }
        }
    }
}
