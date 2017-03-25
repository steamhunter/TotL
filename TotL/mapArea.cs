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
        List<Units.Unit> clusterA = new List<Units.Unit>();
        List<Units.Unit> clusterB = new List<Units.Unit>();
        List<Units.Unit> EnemyCluster = new List<Units.Unit>();
        bool spawnclusterA = false;
        short clusterAsize = 0;
        bool spawnclusterB = false;
        short clusterBsize = 0;
        bool spawnEnemy = false;
        short EnemySize = 0;
        bool wasmovement = false;
        #endregion

        private int GetCoordinateFromLocation(int location)
        {
            int unitsize = Convert.ToInt32(Vars.unitSize);
            return (20 + (location * unitsize)) + (unitsize / 2);
        }
        private int GetLocationFromCoordinate(int Coordinate)
        {
            return (Coordinate - 20) / (int)Vars.unitSize;
        }

        public override void Initialize()
        {
            Vars.mapstate = internalstates.map_initializing;
            #region Game system init
            float unitSize = (Vars.ScreenWidth * 0.83f) / 25f;
            int now = DateTime.Now.Millisecond * DateTime.Now.Second;
            cons.debugMessage(now.ToString());
            Random random = new Random(now);
            Vars.seed = random.Next(10000000, 99999999);
            cons.debugMessage(Vars.seed.ToString());
            random = new Random(Vars.seed);
            Vars.random = random;
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

            Vars.mapstate = internalstates.map_ready;
        }
        LinkedList<Connection> test;
        public override void LoadContent()
        {
        }

        #region unit managment globals
        short clusterAtick = 0;
        float ClusterAX;
        float ClusterAY;
        short selectedCluster = 0;
        bool ClusterAhastarget = false;
        short ClusterAtargetIndex = 0;
        short ClusterAtargettick = 0;

        short clusterBtick = 0;
        float ClusterBX;
        float ClusterBY;
        bool clusterBhastarget = false;
        short clusterBtargetIndex = 0;
        short clusterBtargettick = 0;

        short EnemyClustertick = 0;
        float EnemyClusterX;
        float EnemyClusterY;
        bool EnemyClusterhastarget = false;
        short EnemyClustertargetIndex = 0;
        short EnemyClustertargettick = 0;

#endregion

        public override void Update(GameTime gameTime)
        {
            
            #region clusterA
            clusterAtick++;
            if (Vars.mykeyboardmanager.GetState().IsKeyPressed(Keys.X))
            {
                wasmovement = true;
                if (spawnclusterA == false)
                {
                    spawnclusterA = true;
                    selectedCluster = 1;
                }
                else
                {
                    selectedCluster = 1;
                }


            }

            if (spawnclusterA)
            {
                if (clusterAsize != 10)
                {
                    if (clusterAtick >= 30)
                    {
                        Units.PlayerUnit newunit = new Units.PlayerUnit(GetCoordinateFromLocation(bo), GetCoordinateFromLocation(bs));
                        newunit.navcoordinate = new Vector2(GetCoordinateFromLocation(bo), GetCoordinateFromLocation(bs) + 5);
                        clusterA.Add(newunit);
                        clusterAsize++;
                        clusterAtick = 0;
                    }

                }
                else
                {
                    spawnclusterA = false;
                }
            }
            if (Vars.mymousemanager.GetState().LeftButton.Pressed)
            {
                if (selectedCluster == 1)
                {
                    ClusterAX = Vars.mymousemanager.GetState().X * Vars.ScreenWidth;
                    ClusterAY = Vars.mymousemanager.GetState().Y * Vars.ScreenHeight;
                    ClusterAhastarget = true;
                    ClusterAtargetIndex = 0;

                }




            }
            if (ClusterAhastarget)
            {
                ClusterAtargettick++;
                if (ClusterAtargettick >= 30)
                {
                    if (ClusterAtargetIndex < clusterA.Count - 1)
                    {


                        clusterA[ClusterAtargetIndex].target = new Vector2(GetLocationFromCoordinate((int)ClusterAX), GetLocationFromCoordinate((int)ClusterAY));
                        if (ClusterAtargetIndex < clusterA.Count - 1)
                        {
                            ClusterAtargetIndex++;
                        }
                        else if (clusterA.Count == 30)
                        {
                            ClusterAhastarget = false;
                            ClusterAtargetIndex = 0;
                        }
                        ClusterAtargettick = 0;
                    }
                    else
                    {
                        ClusterAtargetIndex = 0;
                    }
                }


            }
            foreach (var item in clusterA)
            {

                item.update(map);
            }
            #endregion

            #region ClusterB
            clusterBtick++;
            if (Vars.mykeyboardmanager.GetState().IsKeyPressed(Keys.C))
            {
                wasmovement = true;
                if (spawnclusterB == false)
                {
                    spawnclusterB = true;
                    selectedCluster = 2;
                }
                else
                {
                    selectedCluster = 2;
                }

            }

            if (spawnclusterB)
            {
                if (clusterBsize != 10)
                {
                    if (clusterBtick >= 30)
                    {
                        Units.PlayerUnit newunit = new Units.PlayerUnit(GetCoordinateFromLocation(bo), GetCoordinateFromLocation(bs));
                        newunit.navcoordinate = new Vector2(GetCoordinateFromLocation(bo), GetCoordinateFromLocation(bs) + 5);
                        clusterB.Add(newunit);
                        clusterBsize++;
                        clusterBtick = 0;
                    }

                }
                else
                {
                    spawnclusterB = false;
                }
            }
            if (Vars.mymousemanager.GetState().LeftButton.Pressed)
            {
                if (selectedCluster == 2)
                {
                    ClusterBX = Vars.mymousemanager.GetState().X * Vars.ScreenWidth;
                    ClusterBY = Vars.mymousemanager.GetState().Y * Vars.ScreenHeight;
                    clusterBhastarget = true;
                    clusterBtargetIndex = 0;
                }





            }
            if (clusterBhastarget)
            {
                if (clusterBtargetIndex < clusterB.Count - 1)
                {


                    clusterBtargettick++;
                    if (clusterBtargettick >= 30)
                    {
                        clusterB[clusterBtargetIndex].target = new Vector2(GetLocationFromCoordinate((int)ClusterBX), GetLocationFromCoordinate((int)ClusterBY));
                        if (clusterBtargetIndex < clusterB.Count - 1)
                        {
                            clusterBtargetIndex++;
                        }
                        else if (clusterB.Count == 30)
                        {
                            clusterBhastarget = false;
                            clusterBtargetIndex = 0;
                        }
                        clusterBtargettick = 0;
                    }
                }
                else
                {
                    clusterBtargetIndex = 0;
                }

            }
            foreach (var item in clusterB)
            {

                item.update(map);
            }
            #endregion

            #region enemyCluster

            EnemyClustertick++;
            if (Vars.mykeyboardmanager.GetState().IsKeyPressed(Keys.T))
            {

                spawnEnemy = true;




            }
            if (spawnEnemy)
            {
                if (EnemySize != 20)
                {
                    if (EnemyClustertick >= 20)
                    {
                        Units.EnemyUnit newunit = new Units.EnemyUnit(GetCoordinateFromLocation(eo), GetCoordinateFromLocation(es));
                        newunit.navcoordinate = new Vector2(GetCoordinateFromLocation(eo), GetCoordinateFromLocation(es) + 5);
                        EnemyCluster.Add(newunit);
                        EnemySize++;
                        EnemyClustertick = 0;
                    }

                }
                else
                {
                    spawnEnemy = false;
                }
            }

            if (EnemyCluster.Count == 20)
            {
                EnemyClusterX = GetCoordinateFromLocation(bo);
                EnemyClusterY = GetCoordinateFromLocation(bs);
                EnemyClusterhastarget = true;

            }


            if (EnemyClusterhastarget && EnemyCluster.Count == 20)
            {
                EnemyClustertargettick++;
                if (EnemyClustertargettick >= 30)
                {
                    EnemyCluster[EnemyClustertargetIndex].target = new Vector2(GetLocationFromCoordinate((int)EnemyClusterX), GetLocationFromCoordinate((int)EnemyClusterY));
                    if (EnemyClustertargetIndex < EnemyCluster.Count - 1)
                    {
                        EnemyClustertargetIndex++;
                    }
                    else if (EnemyCluster.Count == 30)
                    {
                        EnemyClusterhastarget = false;
                        EnemyClustertargetIndex = 0;
                    }
                    EnemyClustertargettick = 0;
                }


            }

            for (int i = 0; i < EnemyCluster.Count; i++)
            {
                EnemyCluster[i].update(map);
            }

            #endregion


            for(int ec=0;ec<EnemyCluster.Count;ec++)
            {
                short eLocationX = (short)GetLocationFromCoordinate(EnemyCluster[ec].CoordinateX);
                short eLocationY = (short)GetLocationFromCoordinate(EnemyCluster[ec].CoordinateY);

                for(int ca=0;ca<clusterA.Count;ca++)
                {
                    short fLocationX=(short)GetLocationFromCoordinate(clusterA[ca].CoordinateX);
                    short fLocationY=(short)GetLocationFromCoordinate(clusterA[ca].CoordinateY);

                    if (eLocationX==fLocationX&&eLocationY==fLocationY)
                    {
                        EnemyCluster[ec].damageUnit(1);
                        clusterA[ca].damageUnit(1);
                        if (EnemyCluster[ec].HP <= 0)
                        {
                            EnemyCluster.Remove(EnemyCluster[ec]);
                        }

                        if (clusterA[ca].HP <= 0)
                        {
                            clusterA.RemoveAt(ca);
                        }
                    }
                }

                for(int cb=0;cb<clusterB.Count;cb++)
                {
                    short fLocationX = (short)GetLocationFromCoordinate(clusterB[cb].CoordinateX);
                    short fLocationY = (short)GetLocationFromCoordinate(clusterB[cb].CoordinateY);

                    if (eLocationX == fLocationX && eLocationY == fLocationY)
                    {
                        EnemyCluster[ec].damageUnit(1);
                        clusterB[cb].damageUnit(1);


                        if (EnemyCluster[ec].HP<=0)
                        {
                            EnemyCluster.Remove(EnemyCluster[ec]);
                        }

                        if (clusterB[cb].HP<=0)
                        {
                            clusterB.RemoveAt(cb);
                        }
                    }
                }

            }
        }

        public override void Draw(GameTime gameTime)
        {
            Vars.mapstate = internalstates.on_map;

            if (clusterA.Count==0&&clusterB.Count==0&&EnemyCluster.Count==0&&wasmovement)
            {
                Vars.spriteBatch.DrawString(Vars.font, $"DÖNTETLEN {Environment.NewLine} minkét csapat egységei megsemisültek (nyomj e-t a kilépéshez)", new Vector2(Vars.ScreenWidth / 2 - 100, Vars.ScreenHeight / 2), Color.Black);
            }
            else if ((map[eo, es] as UnitBase).isdestroyed)
            {
                Vars.spriteBatch.DrawString(Vars.font, "GYŐZTÉL (nyomj e-t a kilépéshez)", new Vector2(Vars.ScreenWidth / 2 - 100, Vars.ScreenHeight / 2), Color.Black);
            }
            else if ((map[bo, bs] as UnitBase).isdestroyed)
            {
                Vars.spriteBatch.DrawString(Vars.font, "VESZTETTÉL (nyomj e-t a kilépéshez)", new Vector2(Vars.ScreenWidth / 2 - 100, Vars.ScreenHeight / 2), Color.Black);
            }
            else
            {
                for (int s = 0; s < 15; s++)
                {
                    for (int o = 0; o < 25; o++)
                    {
                        map[o, s].draw();

                    }
                }

                foreach (var item in clusterA)
                {
                    item.draw();
                }
                foreach (var item in clusterB)
                {
                    item.draw();
                }
                foreach (var item in EnemyCluster)
                {
                    item.draw();
                }
            }
        }
    }
}
