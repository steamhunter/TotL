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
using TotL.Labyrinth;
using PathFinder.Debug;
using PathFinder.AStar;
using SharpDX;
using SharpDX.Toolkit.Input;

namespace TotL
{
    class mapArea : AreaBase
    {
        #region Globals
        int bs, bo;
        int es, eo;
        Cell[,] map = new Cell[25, 15];
        Connection[,] connect = new Connection[27, 18];
        List<Units.Unit> unitlist = new List<Units.Unit>();
        #endregion

        private int GetCoordinateFromLocation(int location)
        {
            int unitsize = Convert.ToInt32(Vars.unitSize);
            return (20+(location*unitsize))+(unitsize/2);
        }

        public override void Initialize()
        {
            #region Game system init
            float unitSize = (Vars.ScreenWidth * 0.83f) / 25f;
            int now = DateTime.Now.Millisecond * DateTime.Now.Second;
            cons.debugMessage(now.ToString());
            Random random = new Random(now);
            Vars.seed = random.Next(10000000, 99999999);
            cons.debugMessage(Vars.seed.ToString());
            random = new Random(Vars.seed);
            Vars.config = configjson.getConfig();
            #endregion

            #region Generator Border
            for (int s = 0; s < 18; s++)
            {
                for (int o = 0; o < 27; o++)
                {
                    if (s == 0 || o == 0 || o == 26 || s == 16)
                    {
                        connect[o, s] = new Connection(o, s);
                        connect[o, s].up = false;
                        connect[o, s].down = false;
                        connect[o, s].left = false;
                        connect[o, s].right = false;
                        connect[o, s].closedsides = 4;
                    }
                    else
                    {
                        connect[o, s] = new Connection(o, s);
                        connect[o, s].closedsides = 0;
                        connect[o, s].up = true; //map[i - 1, j - 1].up;
                        connect[o, s].down = true; //map[i - 1, j - 1].down;
                        connect[o, s].left = true;// map[i - 1, j - 1].left;
                        connect[o, s].right = true;//map[i - 1, j - 1].right;
                        connect[o, s].isPopulated = false;
                    }
                }
            }
            #endregion

            #region generate start base 
            bs = random.Next(1, 13);
            bo = random.Next(1, 13);
            do
            {
                es = random.Next(1, 13);
                eo = random.Next(1, 13);
            } while (Math.Abs(bs - es) + Math.Abs(bo - eo) < 10);
            #endregion

            int co = 0;
            int cs = 0;
            for (int s = 0; s < 15; s++)
            {
                cs++;
                for (int o = 0; o < 25; o++)
                {


                    co++;
                    cons.groupedMessage(o + " " + s, "TERGEN");
                    if (s == bs && o == bo)
                    {
                        Console.WriteLine();
                    }
                    #region valid cell gen
                    bool valid = false;
                    while (!valid)
                    {
                        Cell cell;

                        #region cell weights
                        int fcw, ccw, osb, tsb, tcw, dec = 0;
                        fcw = Vars.config.fc_weight;
                        ccw = fcw + Vars.config.cross_weight;
                        dec = ccw + Vars.config.deadend_weight;
                        tsb = dec + Vars.config.twoside_weight;
                        tcw = tsb + Vars.config.tunnel_weight;
                        osb = tcw + Vars.config.oneside_weight;
                        #endregion

                        #region pick random cell
                        int randomcellweight = random.Next(0, 101);
                        if (randomcellweight < fcw)
                        {
                            cell = new FullCell(o, s);

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
                            cell = new CrossCell(o, s);

                            if (cell.CheckFitting(connect, co, cs, o, s))
                            {
                                valid = true;
                                cons.groupedMessage("cross", "TERGEN");
                                map[o, s] = cell;
                            }
                            else
                            {
                                valid = false;
                            }
                        }
                        else if (randomcellweight < dec)
                        {
                            cell = new DeadEndCell(o, s);

                            if (cell.CheckFitting(connect, co, cs, o, s))
                            {
                                valid = true;
                                cons.groupedMessage("deadend", "TERGEN");
                                map[o, s] = cell;
                            }
                            else
                            {
                                valid = false;
                            }
                        }
                        else if (randomcellweight < tsb)
                        {
                            cell = new TwoSideBlocked(o, s);

                            if (cell.CheckFitting(connect, co, cs, o, s))
                            {
                                valid = true;
                                cons.groupedMessage("twoside", "TERGEN");
                                map[o, s] = cell;
                            }
                            else
                            {
                                valid = false;
                            }
                        }
                        else if (randomcellweight < tcw)
                        {
                            cell = new TunnelCell(o, s);

                            if (cell.CheckFitting(connect, co, cs, o, s))
                            {
                                valid = true;
                                cons.groupedMessage("tunnel", "TERGEN");
                                map[o, s] = cell;
                            }
                            else
                            {
                                valid = false;
                            }
                        }
                        else if (randomcellweight < osb)
                        {
                            cell = new OneSideBlocked(o, s);

                            if (cell.CheckFitting(connect, co, cs, o, s))
                            {
                                valid = true;
                                cons.groupedMessage("oneside", "TERGEN");
                                map[o, s] = cell;
                            }
                            else
                            {
                                valid = false;
                            }
                        }
                        #endregion

                    }
                    map[o, s].SetBlockingVolumes();

                    #endregion
                }
                co = 0;
            }
            cs = 0;
            co = 0;

            map[bo, bs] = new UnitBase(map[bo, bs], "friendly", bo, bs);
            map[eo, es] = new UnitBase(map[eo, es], "enemy", eo, es);

            AStar.AstarSolver = new AStar.Solver<Connection, Object>(connect);
            test = AStar.AstarSolver.Search(new System.Drawing.Point(bo + 1, bs + 1), new System.Drawing.Point(eo + 1, es + 1), null);

            if (test == null)
            {
                Initialize();
            }
            cons.debugMessage("base" + bo + " " + bs);
            cons.debugMessage("enemy" + eo + " " + es);
            foreach (var item in test)
            {
                cons.debugMessage(item.X + " " + item.Y);
            }





        }
        LinkedList<Connection> test;
        public override void LoadContent()
        {

        }
        public override void Update(GameTime gameTime)
        {
            if (Vars.mykeyboardmanager.GetState().IsKeyPressed(Keys.A))
            {
                Units.PlayerUnit newunit = new Units.PlayerUnit(GetCoordinateFromLocation(bo), GetCoordinateFromLocation(bs), new Vector2(GetCoordinateFromLocation(bo), GetCoordinateFromLocation(bs) + 5));
                unitlist.Add(newunit);
            }
            foreach (var item in unitlist)
            {
                item.update(map);
            }
        }
        public override void Draw(GameTime gameTime)
        {
            for (int s = 0; s < 15; s++)
            {
                for (int o = 0; o < 25; o++)
                {
                    if (Vars.path_debug_Draw && test != null)
                    {
                        foreach (var item in test)
                        {
                            // Vars.spriteBatch.Draw(TextureFromFile.TextureProcessor.getTexture("transparent"), new SharpDX.Vector2(20 + (0+Vars.unitSize/2), 20 + (0)), Color.White);
                            Vars.spriteBatch.Draw(TextureFromFile.TextureProcessor.getTexture("transparent"), new RectangleF((20 + ((item.X - 1) * Vars.unitSize + (Vars.unitSize / 2))) - 5, (20 + ((item.Y - 1) * Vars.unitSize + (Vars.unitSize / 2))) - 5, 10, 10), null, Color.White, 0f, new Vector2(0, 0), SpriteEffects.None, 0f);

                        }
                    }


                    map[o, s].draw();

                }
            }

            foreach (var item in unitlist)
            {
                item.draw();
            }
        }
    }
}
