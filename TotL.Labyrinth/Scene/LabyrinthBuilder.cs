using PathFinder;
using PathFinder.Debug;
using PathFinder.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotL.Labyrinth.Scene
{
    public class LabyrinthBuilder:MapBuilder
    {
        public int bs, bo;
        public int es, eo;
        private static class CellSelector
        {
            public static bool CheckCell(TerrainTile cell, Connection[,] connect, TerrainTile[,] map, int co, int cs, int o, int s)
            {
                if (cell.CheckFitting(connect, co, cs, o, s))
                {

                    cons.GroupedMessage(cell.ToString(), "generator","terrain");
                    map[o, s] = cell;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        

        public LabyrinthBuilder(Connection[,] connect, TerrainTile[,] map):base(connect,map)
        {
            this.Connect = connect;
            this.Map = map;
        }

        public override void Build()
        {
            GenerateBorder(Connect);

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
                    cons.GroupedMessage(o + " " + s, "generator","terrain");
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
                            valid = CellSelector.CheckCell(new FullCell(o, s), Connect, Map, co, cs, o, s);
                        }
                        else if (randomcellweight < ccw)
                        {
                            valid = CellSelector.CheckCell(new CrossCell(o, s), Connect, Map, co, cs, o, s);

                        }
                        else if (randomcellweight < dec)
                        {

                            valid = CellSelector.CheckCell(new DeadEndCell(o, s), Connect, Map, co, cs, o, s);
                        }
                        else if (randomcellweight < tsb)
                        {

                            valid = CellSelector.CheckCell(new TwoSideBlocked(o, s), Connect, Map, co, cs, o, s);
                        }
                        else if (randomcellweight < tcw)
                        {

                            valid = CellSelector.CheckCell(new TunnelCell(o, s), Connect, Map, co, cs, o, s);
                        }
                        else if (randomcellweight < osb)
                        {

                            valid = CellSelector.CheckCell(new OneSideBlocked(o, s), Connect, Map, co, cs, o, s);
                        }
                        #endregion

                    }
                    Map[o, s].SetBlockingVolumes();

                    #endregion
                }
                co = 0;
            }
            cs = 0;
            co = 0;

            Map[bo, bs] = new UnitBase(Map[bo, bs], "friendly", bo, bs);
            Map[eo, es] = new UnitBase(Map[eo, es], "enemy", eo, es);
        }

       
    }
}
