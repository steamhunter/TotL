using PathFinder;
using PathFinder.Debug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotL.Labyrinth.Map
{
    public class MapBuilder
    {
        private static class CellSelector
        {
            public static bool CheckCell(Cell cell, Connection[,] connect, Cell[,] map, int co, int cs, int o, int s)
            {
                if (cell.CheckFitting(connect, co, cs, o, s))
                {

                    cons.groupedMessage(cell.ToString(), "generator","terrain");
                    map[o, s] = cell;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        Connection[,] connect;
        Cell[,] map;

        public MapBuilder(Connection[,] connect, Cell[,] map)
        {
            this.connect = connect;
            this.map = map;
        }

        public void Build(out int eo, out int es, out int bo, out int bs)
        {
            GenerateBorder(connect);

            bs = Vars.random.Next(1, 13);
            bo = Vars.random.Next(1, 13);
            do
            {
                es = Vars.random.Next(1, 13);
                eo = Vars.random.Next(1, 13);
            } while (Math.Abs(bs - es) + Math.Abs(bo - eo) < 10);

            int co = 0;
            int cs = 0;
            for (int s = 0; s < 15; s++)
            {
                cs++;
                for (int o = 0; o < 25; o++)
                {


                    co++;
                    cons.groupedMessage(o + " " + s, "generator","terrain");
                    #region valid cell gen
                    bool valid = false;
                    while (!valid)
                    {

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
                        int randomcellweight = Vars.random.Next(0, 101);
                        if (randomcellweight < fcw)
                        {
                            valid = CellSelector.CheckCell(new FullCell(o, s), connect, map, co, cs, o, s);
                        }
                        else if (randomcellweight < ccw)
                        {
                            valid = CellSelector.CheckCell(new CrossCell(o, s), connect, map, co, cs, o, s);

                        }
                        else if (randomcellweight < dec)
                        {

                            valid = CellSelector.CheckCell(new DeadEndCell(o, s), connect, map, co, cs, o, s);
                        }
                        else if (randomcellweight < tsb)
                        {

                            valid = CellSelector.CheckCell(new TwoSideBlocked(o, s), connect, map, co, cs, o, s);
                        }
                        else if (randomcellweight < tcw)
                        {

                            valid = CellSelector.CheckCell(new TunnelCell(o, s), connect, map, co, cs, o, s);
                        }
                        else if (randomcellweight < osb)
                        {

                            valid = CellSelector.CheckCell(new OneSideBlocked(o, s), connect, map, co, cs, o, s);
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
        }

        private void GenerateBorder(Connection[,] connect)
        {
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
        }
    }
}
