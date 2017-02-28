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
using TotL.Labyrinth;
using PathFinder.Debug;
using PathFinder.AStar;

namespace TotL
{
    class mapArea: AreaBase
    {
        int bs, bo;
        int es, eo;
        Cell[,] map= new Cell[25,15];
        Connection[,] connect = new Connection[27,18];
        public override void Initialize()
        {

            float unitSize = (Vars.ScreenWidth * 0.83f) / 25f;
            int now = DateTime.Now.Millisecond * DateTime.Now.Second;
            cons.debugMessage(now.ToString());
            Random random = new Random(now);
            Vars.seed = random.Next(10000000, 99999999);
            cons.debugMessage(Vars.seed.ToString());
            random = new Random(Vars.seed);
            Vars.config = configjson.getConfig();

            for (int s = 0; s < 18; s++)
            {
                for (int o = 0; o < 27; o++)
                {
                    if (s == 0 || o == 0 || o == 26 || s == 16)
                    {
                        connect[o,s] = new Connection(o,s);
                        connect[o,s].up = false;
                        connect[o,s].down = false;
                        connect[o,s].left = false;
                        connect[o,s].right = false;
                        connect[o,s].closedsides = 4;
                    }
                    else
                    {
                        connect[o,s] = new Connection(o,s);
                        connect[o,s].closedsides = 0;
                        connect[o,s].up = true; //map[i - 1, j - 1].up;
                        connect[o,s].down = true; //map[i - 1, j - 1].down;
                        connect[o,s].left = true;// map[i - 1, j - 1].left;
                        connect[o,s].right = true;//map[i - 1, j - 1].right;
                        connect[o,s].isPopulated = false;
                    }
                }
            }
            int co = 0;
            int cs = 0;

             bs = random.Next(1, 13);
             bo = random.Next(1, 13);
            do
            {
                 es = random.Next(1, 13);
                 eo = random.Next(1, 13);
            } while (Math.Abs(bs-es)+Math.Abs(bo-eo)<10);


           
            for (int s = 0; s < 15; s++)
            {
                cs++;
                for (int o = 0; o < 25; o++)
                {
                    

                    co++;
                    cons.groupedMessage(o + " " + s, "TERGEN");
                    if (s==bs&&o==bo)
                    {
                        Console.WriteLine();
                    }
                    bool valid = false;
                    while (!valid)
                    {
                        Cell cell;

                        int fcw, ccw, osb, tsb, tcw, dec = 0;

                        fcw = Vars.config.fc_weight;
                        ccw = fcw + Vars.config.cross_weight;
                        dec = ccw + Vars.config.deadend_weight;
                        tsb = dec + Vars.config.twoside_weight;
                        tcw = tsb + Vars.config.tunnel_weight;
                        osb = tcw + Vars.config.oneside_weight;

                        int randomcellweight = random.Next(0, 101);
                        if (randomcellweight < fcw)
                        {
                            cell = new FullCell(o,s);

                            if (cell.CheckFitting(connect, co, cs, o, s))
                            {
                                valid = true;
                                cons.groupedMessage("fullcell", "TERGEN");
                                map[o, s] = cell;
                            }
                            else
                            {
                                valid = false;
                            }
                        }
                        else if (randomcellweight < ccw)
                        {
                            cell = new CrossCell(o,s);

                            if (cell.CheckFitting(connect, co, cs, o, s))
                            {
                                valid = true;
                                cons.groupedMessage("cross", "TERGEN");
                                map[o,s] = cell;
                            }
                            else
                            {
                                valid = false;
                            }
                        }
                        else if (randomcellweight < dec)
                        {
                            cell = new DeadEndCell(o,s);

                            if (cell.CheckFitting(connect, co, cs, o, s))
                            {
                                valid = true;
                                cons.groupedMessage("deadend", "TERGEN");
                                map[o,s] = cell;
                            }
                            else
                            {
                                valid = false;
                            }
                        }
                        else if (randomcellweight < tsb)
                        {
                            cell = new TwoSideBlocked(o,s);

                            if (cell.CheckFitting(connect, co, cs, o, s))
                            {
                                valid = true;
                                cons.groupedMessage("twoside", "TERGEN");
                                map[o,s] = cell;
                            }
                            else
                            {
                                valid = false;
                            }
                        }
                        else if (randomcellweight < tcw)
                        {
                            cell = new TunnelCell(o,s);

                            if (cell.CheckFitting(connect, co, cs, o, s))
                            {
                                valid = true;
                                cons.groupedMessage("tunnel", "TERGEN");
                                map[o,s] = cell;
                            }
                            else
                            {
                                valid = false;
                            }
                        }
                        else if (randomcellweight < osb)
                        {
                            cell = new OneSideBlocked(o,s);

                            if (cell.CheckFitting(connect, co, cs, o, s))
                            {
                                valid = true;
                                cons.groupedMessage("oneside", "TERGEN");
                                map[o,s] = cell;
                            }
                            else
                            {
                                valid = false;
                            }
                        }

                    }
                    map[o,s].SetBlockingVolumes();

                }
                co = 0;
            }
            cs = 0;
            co = 0;
            map[bo,bs] = new UnitBase(map[bo,bs],"friendly",bo,bs);
            map[eo, es] = new UnitBase(map[eo, es], "enemy",eo,es);

            AStar.Solver<Connection, Object> aStar = new AStar.Solver<Connection, Object>(connect);
           LinkedList<Connection> test= aStar.Search(new System.Drawing.Point(bo,bs),new System.Drawing.Point(eo,es),null);

            foreach (var item in test )
            {
                cons.debugMessage(item.X + " " + item.Y);
            }
            




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


                    Vars.spriteBatch.Draw(TextureFromFile.TextureProcessor.getTexture("transparent"), new SharpDX.Vector2(bs,bo), SharpDX.Color.White);
                    map[o,s].draw();
                  
                }
            }
        }
    }
}
