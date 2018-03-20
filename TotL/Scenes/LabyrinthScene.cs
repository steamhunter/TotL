using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFinder.Scene;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
using PathFinder._2D;
using PathFinder;
using TotL.Labyrinth;
using PathFinder.Debug;
using PathFinder.AStar;
using SharpDX;
using SharpDX.Toolkit.Input;

namespace TotL.Scenes
{
   
    class LabyrinthScene : Scene
    {
        #region Globals
        Labyrinth.Scene.LabyrinthBuilder mapbuilder;
        List<Units.Unit> clusterA = new List<Units.Unit>();
        List<Units.Unit> clusterB = new List<Units.Unit>();
        List<Units.Unit> EnemyCluster = new List<Units.Unit>();
        UI.ClusterStatus AClusterStatus;
        UI.ClusterStatus BClusterStatus;
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
            int unitsize = Convert.ToInt32(Vars.cellSize);
            return (20 + (location * unitsize)) + (unitsize / 2);
        }
        private int GetLocationFromCoordinate(int Coordinate)
        {
            return (Coordinate - 20) / (int)Vars.cellSize;
        }

        public override void Initialize()
        {
            terrain = new Labyrinth.Scene.LabyrinthTerrain(new TerrainTile[25, 15], new Connection[27, 18]);
            Vars.mapstate = internalstates.map_initializing;
            #region Game system init
            float unitSize = (Vars.ScreenWidth * 0.83f) / 25f;
            int now = DateTime.Now.Millisecond * DateTime.Now.Second;
            cons.debugMessage($"base time: {now.ToString()}","generator");
            Random random = new Random(now);
            Vars.seed = random.Next(10000000, 99999999);
            cons.debugMessage($"SEED: {Vars.seed.ToString()}","generator");
            random = new Random(Vars.seed);
            Vars.random = random;
            AClusterStatus = new UI.ClusterStatus(Vars.ScreenWidth - 200, 50,128,128,"A_cluster");
            BClusterStatus = new UI.ClusterStatus(Vars.ScreenWidth - 200, 50 + 128 + 30, 128, 128,"B_cluster");
            #endregion

            mapbuilder = ((Labyrinth.Scene.LabyrinthBuilder)((Labyrinth.Scene.LabyrinthTerrain)terrain).mapBuilder);
            
            mapbuilder.Build();
            
            AStar.RelationalAstarSolver = new AStar.RelationalSolver<Connection, Object>(((Labyrinth.Scene.LabyrinthTerrain)terrain).connect);
            LinkedList<Connection> test = AStar.RelationalAstarSolver.Search(new System.Drawing.Point(mapbuilder.bo + 1, mapbuilder.bs + 1), new System.Drawing.Point(mapbuilder.eo + 1, mapbuilder.es + 1), null);

            if (test == null)
            {
                Initialize();
            }
            cons.debugMessage($"base {mapbuilder.bo} {mapbuilder.bs}","generator");
            cons.debugMessage($"enemy {mapbuilder.eo} {mapbuilder.es}","generator");

            Vars.mapstate = internalstates.map_ready;
        }

        


        public override void LoadContent()
        {
            AClusterStatus.LoadContent();
            BClusterStatus.LoadContent();
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
                
                if (spawnclusterA == false)
                {
                    spawnclusterA = true;
                    selectedCluster = 1;
                    AClusterStatus.Select();
                    BClusterStatus.DeSelect();

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
                        
                        Units.PlayerUnit newunit = new Units.PlayerUnit(new Vector2(GetCoordinateFromLocation(mapbuilder.bo), GetCoordinateFromLocation(mapbuilder.bs)));
                        newunit.navcoordinate = new Vector2(GetCoordinateFromLocation(mapbuilder.bo), GetCoordinateFromLocation(mapbuilder.bs));
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
                    if (ClusterAtargetIndex < clusterA.Count)
                    {


                        clusterA[ClusterAtargetIndex].target = new Vector2(GetLocationFromCoordinate((int)ClusterAX), GetLocationFromCoordinate((int)ClusterAY));
                        if (ClusterAtargetIndex < clusterA.Count)
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

                item.Update(mapbuilder.map,clusterA);
            }
            #endregion

            #region ClusterB
            clusterBtick++;
            if (Vars.mykeyboardmanager.GetState().IsKeyPressed(Keys.C))
            {
                if (spawnclusterB == false)
                {
                    spawnclusterB = true;
                    selectedCluster = 2;
                    BClusterStatus.Select();
                    AClusterStatus.DeSelect();
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
                        
                        Units.PlayerUnit newunit = new Units.PlayerUnit(new Vector2(GetCoordinateFromLocation(mapbuilder.bo), GetCoordinateFromLocation(mapbuilder.bs)));
                        newunit.navcoordinate = new Vector2(GetCoordinateFromLocation(mapbuilder.bo), GetCoordinateFromLocation(mapbuilder.bs));
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
                    BClusterStatus.Select();
                    AClusterStatus.DeSelect();
                }





            }
            if (clusterBhastarget)
            {
                if (clusterBtargetIndex < clusterB.Count)
                {


                    clusterBtargettick++;
                    if (clusterBtargettick >= 30)
                    {
                        clusterB[clusterBtargetIndex].target = new Vector2(GetLocationFromCoordinate((int)ClusterBX), GetLocationFromCoordinate((int)ClusterBY));
                        if (clusterBtargetIndex < clusterB.Count)
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

                item.Update(mapbuilder.map,clusterB);
            }
            #endregion

            #region enemyCluster

            EnemyClustertick++;
            if (gameTime.TotalGameTime.Seconds > 15 || Vars.mykeyboardmanager.GetState().IsKeyPressed(Keys.T))
            {

                spawnEnemy = true;




            }
            if (spawnEnemy)
            {
                if (EnemySize != 20)
                {
                    if (EnemyClustertick >= 20)
                    {
                        Units.EnemyUnit newunit = new Units.EnemyUnit(new Vector2(GetCoordinateFromLocation(mapbuilder.eo), GetCoordinateFromLocation(mapbuilder.es)));
                        newunit.navcoordinate = new Vector2(GetCoordinateFromLocation(mapbuilder.eo), GetCoordinateFromLocation(mapbuilder.es));
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
                EnemyClusterX = GetCoordinateFromLocation(mapbuilder.bo);
                EnemyClusterY = GetCoordinateFromLocation(mapbuilder.bs);
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
                EnemyCluster[i].Update(mapbuilder.map,EnemyCluster);
            }

            #endregion

            if ((clusterA.Count>0||clusterB.Count>0)&&EnemyCluster.Count>0&&!wasmovement)
            {
                wasmovement = true;
            }

            for (int ec = 0; ec < EnemyCluster.Count; ec++)
            {
                int eLocationX;
                int eLocationY;
                if (EnemyCluster.Count>0)
                {
                    eLocationX = GetLocationFromCoordinate((int)EnemyCluster[ec].Coordinate.X);
                    eLocationY = GetLocationFromCoordinate((int)EnemyCluster[ec].Coordinate.Y);
                }
                else
                {
                    break;
                }
                

                for (int ca = 0; ca < clusterA.Count; ca++)
                {
                    if (clusterA.Count > 0&&EnemyCluster.Count>0)
                    {
                        int fLocationX;
                        int fLocationY;
                        if (clusterA.Count > 0&&EnemyCluster.Count>0)
                        {


                            fLocationX = GetLocationFromCoordinate((int)clusterA[ca].Coordinate.X);
                            fLocationY = GetLocationFromCoordinate((int)clusterA[ca].Coordinate.Y);

                            if (eLocationX == fLocationX && eLocationY == fLocationY)
                            {
                                EnemyCluster[ec].DamageUnit(1);
                                clusterA[ca].DamageUnit(1);
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
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                ;
                for (int cb = 0; cb < clusterB.Count; cb++)
                {
                    int fLocationX = -1;
                    int fLocationY = -1;
                    if (clusterB.Count > 0&&EnemyCluster.Count>0)
                    {
                        fLocationX = GetLocationFromCoordinate((int)clusterB[cb].Coordinate.X);
                        fLocationY = GetLocationFromCoordinate((int)clusterB[cb].Coordinate.Y);
                    }
                    else
                    {
                        break;
                    }


                    if (eLocationX == fLocationX && eLocationY == fLocationY)
                    {
                        if (clusterB.Count > 0&&EnemyCluster.Count>0)
                        {

                            if (ec < EnemyCluster.Count)
                            {
                                EnemyCluster[ec].DamageUnit(1);
                                clusterB[cb].DamageUnit(1);


                                if (EnemyCluster[ec].HP <= 0)
                                {
                                    EnemyCluster.Remove(EnemyCluster[ec]);
                                }

                                if (clusterB[cb].HP <= 0)
                                {
                                    clusterB.RemoveAt(cb);
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }

            }
        }

        public override void Draw(GameTime gameTime)
        {
            Vars.mapstate = internalstates.on_map;

            if (clusterA.Count == 0 && clusterB.Count == 0 && EnemyCluster.Count == 0 && wasmovement)
            {
                if (!Vars.noTextMode)
                {
                    Vars.spriteBatch.DrawString(Vars.font, $"DÖNTETLEN {Environment.NewLine} minkét csapat egységei megsemisültek (nyomj e-t a kilépéshez)", new Vector2(Vars.ScreenWidth / 2 - 100, Vars.ScreenHeight / 2), Color.Black);

                }
            }
            else if ((mapbuilder.map[mapbuilder.eo, mapbuilder.es] as UnitBase).isdestroyed||EnemyCluster.Count==0&&wasmovement)
            {
                if (!Vars.noTextMode)
                {
                    Vars.spriteBatch.DrawString(Vars.font, "GYŐZTÉL (nyomj e-t a kilépéshez)", new Vector2(Vars.ScreenWidth / 2 - 100, Vars.ScreenHeight / 2), Color.Black);
                }
            }
            else if ((mapbuilder.map[mapbuilder.bo, mapbuilder.bs] as UnitBase).isdestroyed|| clusterA.Count == 0 && clusterB.Count == 0&&wasmovement)
            {
                if (!Vars.noTextMode)
                {
                    Vars.spriteBatch.DrawString(Vars.font, "VESZTETTÉL (nyomj e-t a kilépéshez)", new Vector2(Vars.ScreenWidth / 2 - 100, Vars.ScreenHeight / 2), Color.Black);

                }
            }
            else
            {
                if (!Vars.noTextMode)
                {
                    Vars.spriteBatch.DrawString(Vars.font, "Saját bázis élete :" + "10000/" + (mapbuilder.map[mapbuilder.bo, mapbuilder.bs] as UnitBase).hp, new Vector2(20, Vars.ScreenHeight - 50), Color.Black);
                    Vars.spriteBatch.DrawString(Vars.font, "ellenséges bázis élete :" + "10000/" + (mapbuilder.map[mapbuilder.eo, mapbuilder.es] as UnitBase).hp, new Vector2(Vars.ScreenHeight - 50, Vars.ScreenHeight - 50), Color.Black);
                }
                for (int s = 0; s < 15; s++)
                {
                    for (int o = 0; o < 25; o++)
                    {
                        mapbuilder.map[o, s].Draw(gameTime);

                    }
                }

                foreach (var item in clusterA)
                {
                    item.Draw(gameTime);
                }
                foreach (var item in clusterB)
                {
                    item.Draw(gameTime);
                }
                foreach (var item in EnemyCluster)
                {
                    item.Draw(gameTime);
                }
                if (AClusterStatus != null)
                {
                    AClusterStatus.Draw(gameTime);
                }
                if (BClusterStatus != null)
                {
                    BClusterStatus.Draw(gameTime);
                }

            }
        }

        public override void UnloadContent()
        {
            
        }
    }
}
