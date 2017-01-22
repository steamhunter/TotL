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
           cons.debugMessage(now.ToString());
            Random random = new Random(now);
            Vars.seed = random.Next(10000000,99999999);
            cons.debugMessage(Vars.seed.ToString());
            random = new Random(Vars.seed);
            Vars.config = configjson.getConfig();

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
                    bool valid = false;
                    while (!valid)
                    {
                        Cell cell;

                        int fcw, ccw, osb, tsb,tcw, dec = 0;

                        fcw = Vars.config.fc_weight;
                        ccw = fcw + Vars.config.cross_weight;
                        dec = ccw + Vars.config.deadend_weight;
                        tsb = dec + Vars.config.twoside_weight;
                        tcw = tsb + Vars.config.tunnel_weight;
                        osb = tcw + Vars.config.oneside_weight;

                        int randomcellweight = random.Next(0, 101);
                        if (randomcellweight<fcw)
                        {
                            cell = new FullCell();

                            if (cell.CheckFitting(connect, co, cs, o, s))
                            {
                                valid = true;
                                cons.groupedMessage("fullcell", "TERGEN");
                                map[s, o] = cell;
                            }
                            else
                            {
                                valid = false;
                            }
                        }
                        else if (randomcellweight<ccw)
                        {
                            cell = new CrossCell();

                            if (cell.CheckFitting(connect, co, cs, o, s))
                            {
                                valid = true;
                                cons.groupedMessage("cross", "TERGEN");
                                map[s, o] = cell;
                            }
                            else
                            {
                                valid = false;
                            }
                        }
                        else if (randomcellweight<dec)
                        {
                            cell = new DeadEndCell();

                            if (cell.CheckFitting(connect, co, cs, o, s))
                            {
                                valid = true;
                                cons.groupedMessage("deadend", "TERGEN");
                                map[s, o] = cell;
                            }
                            else
                            {
                                valid = false;
                            }
                        }
                        else if (randomcellweight<tsb)
                        {
                            cell = new TwoSideBlocked();

                            if (cell.CheckFitting(connect, co, cs, o, s))
                            {
                                valid = true;
                                cons.groupedMessage("twoside", "TERGEN");
                                map[s, o] = cell;
                            }
                            else
                            {
                                valid = false;
                            }
                        }
                        else if (randomcellweight<tcw)
                        {
                            cell = new TunnelCell();

                            if (cell.CheckFitting(connect, co, cs, o, s))
                            {
                                valid = true;
                                cons.groupedMessage("tunnel", "TERGEN");
                                map[s, o] = cell;
                            }
                            else
                            {
                                valid = false;
                            }
                        }
                        else if(randomcellweight<osb)
                        {
                            cell = new OneSideBlocked();

                            if (cell.CheckFitting(connect, co, cs, o, s))
                            {
                                valid = true;
                                cons.groupedMessage("oneside", "TERGEN");
                                map[s, o] = cell;
                            }
                            else
                            {
                                valid = false;
                            }
                        }
                       
                    }
                    map[s, o].SetBlockingVolumes();

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
