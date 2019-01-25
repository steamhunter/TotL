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
        Cluster clusterA = new Cluster();
        Cluster clusterB = new Cluster();
        Cluster EnemyCluster = new Cluster();
        // List<Units.Unit> EnemyCluster = new List<Units.Unit>();
        // bool spawnEnemy = false;
        // short EnemySize = 0;
        bool wasmovement = false;
        short selectedCluster = 0;
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
            cons.debugMessage($"base time: {now.ToString()}", "generator");
            Random random = new Random(now);
            Vars.seed = random.Next(10000000, 99999999);
            cons.debugMessage($"SEED: {Vars.seed.ToString()}", "generator");
            random = new Random(Vars.seed);
            Vars.random = random;
            clusterA.Status = new UI.ClusterStatus(Vars.ScreenWidth - 200, 50, 128, 128, "A_cluster");
            clusterB.Status = new UI.ClusterStatus(Vars.ScreenWidth - 200, 50 + 128 + 30, 128, 128, "B_cluster");
            #endregion

            mapbuilder = ((Labyrinth.Scene.LabyrinthBuilder)((Labyrinth.Scene.LabyrinthTerrain)terrain).mapBuilder);

            mapbuilder.Build();

            AStar.RelationalAstarSolver = new AStar.RelationalSolver<Connection, Object>(((Labyrinth.Scene.LabyrinthTerrain)terrain).connect);
            LinkedList<Connection> test = AStar.RelationalAstarSolver.Search(new System.Drawing.Point(mapbuilder.bo + 1, mapbuilder.bs + 1), new System.Drawing.Point(mapbuilder.eo + 1, mapbuilder.es + 1), null);

            if (test == null)
            {
                Initialize();
            }
            cons.debugMessage($"base {mapbuilder.bo} {mapbuilder.bs}", "generator");
            cons.debugMessage($"enemy {mapbuilder.eo} {mapbuilder.es}", "generator");

            Vars.mapstate = internalstates.map_ready;
        }




        public override void LoadContent()
        {
            clusterA.Status.LoadContent();
            clusterB.Status.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {

            #region clusterA
            clusterA.Tick++;
            if (Vars.mykeyboardmanager.GetState().IsKeyPressed(Keys.X))
            {

                if (clusterA.Summon == false)
                {
                    clusterA.Summon = true;
                    selectedCluster = 1;
                    clusterA.Status.Select();
                    clusterB.Status.DeSelect();

                }
                else
                {
                    selectedCluster = 1;
                }


            }

            if (clusterA.Summon)
            {
                if (clusterA.Size != 10)
                {
                    if (clusterA.Tick >= 30)
                    {

                        Units.PlayerUnit newunit = new Units.PlayerUnit(new Vector2(GetCoordinateFromLocation(mapbuilder.bo), GetCoordinateFromLocation(mapbuilder.bs)));
                        newunit.navcoordinate = new Vector2(GetCoordinateFromLocation(mapbuilder.bo), GetCoordinateFromLocation(mapbuilder.bs));
                        clusterA.Add(newunit);
                        clusterA.Size++;
                        clusterA.Tick = 0;
                    }

                }
                else
                {
                    clusterA.Summon = false;
                }
            }
            if (Vars.mymousemanager.GetState().LeftButton.Pressed)
            {
                if (selectedCluster == 1)
                {
                    clusterA.X = Vars.mymousemanager.GetState().X * Vars.ScreenWidth;
                    clusterA.Y = Vars.mymousemanager.GetState().Y * Vars.ScreenHeight;
                    clusterA.HasTarget = true;
                    clusterA.TargetIndex = 0;

                }




            }
            if (clusterA.HasTarget)
            {
                clusterA.TargetTick++;
                if (clusterA.TargetTick >= 30)
                {
                    if (clusterA.TargetIndex < clusterA.Count)
                    {


                        clusterA[clusterA.TargetIndex].target = new Vector2(GetLocationFromCoordinate((int)clusterA.X), GetLocationFromCoordinate((int)clusterA.Y));
                        if (clusterA.TargetIndex < clusterA.Count)
                        {
                            clusterA.TargetIndex++;
                        }
                        else if (clusterA.Count == 30)
                        {
                            clusterA.HasTarget = false;
                            clusterA.TargetIndex = 0;
                        }
                        clusterA.TargetTick = 0;
                    }
                    else
                    {
                        clusterA.TargetIndex = 0;
                    }
                }


            }
            foreach (var item in clusterA)
            {

                item.Update(mapbuilder.map, clusterA);
            }
            #endregion

            #region ClusterB
             clusterB.Tick++;
            if (Vars.mykeyboardmanager.GetState().IsKeyPressed(Keys.C))
            {
                if (clusterB.Summon == false)
                {
                    clusterB.Summon = true;
                    selectedCluster = 2;
                    clusterB.Status.Select();
                    clusterA.Status.DeSelect();
                }
                else
                {
                    selectedCluster = 2;
                }

            }

            if (clusterB.Summon)
            {
                if (clusterB.Size != 10)
                {
                    if (clusterB.Tick >= 30)
                    {

                        Units.PlayerUnit newunit = new Units.PlayerUnit(new Vector2(GetCoordinateFromLocation(mapbuilder.bo), GetCoordinateFromLocation(mapbuilder.bs)));
                        newunit.navcoordinate = new Vector2(GetCoordinateFromLocation(mapbuilder.bo), GetCoordinateFromLocation(mapbuilder.bs));
                        clusterB.Add(newunit);
                        clusterB.Size++;
                        clusterB.Tick = 0;
                    }

                }
                else
                {
                    clusterB.Summon = false;
                }
            }
            if (Vars.mymousemanager.GetState().LeftButton.Pressed)
            {
                if (selectedCluster == 2)
                {
                    clusterB.X = Vars.mymousemanager.GetState().X * Vars.ScreenWidth;
                    clusterB.Y = Vars.mymousemanager.GetState().Y * Vars.ScreenHeight;
                    clusterB.HasTarget = true;
                    clusterB.TargetIndex = 0;
                    clusterB.Status.Select();
                    clusterA.Status.DeSelect();
                }





            }
            if (clusterB.HasTarget)
            {
                if (clusterB.TargetIndex < clusterB.Count)
                {


                    clusterB.TargetTick++;
                    if (clusterB.TargetTick >= 30)
                    {
                        clusterB[clusterB.TargetIndex].target = new Vector2(GetLocationFromCoordinate((int)clusterB.X), GetLocationFromCoordinate((int)clusterB.Y));
                        if (clusterB.TargetIndex < clusterB.Count)
                        {
                            clusterB.TargetIndex++;
                        }
                        else if (clusterB.Count == 30)
                        {
                            clusterB.HasTarget = false;
                            clusterB.TargetIndex = 0;
                        }
                        clusterB.TargetTick = 0;
                    }
                }
                else
                {
                    clusterB.TargetIndex = 0;
                }

            }
            foreach (var item in clusterB)
            {

                item.Update(mapbuilder.map, clusterB);
            }
            #endregion

            #region enemyCluster

            EnemyCluster.Tick++;
            if (gameTime.TotalGameTime.Seconds > 15 || Vars.mykeyboardmanager.GetState().IsKeyPressed(Keys.T))
            {

                EnemyCluster.Summon = true;




            }
            if (EnemyCluster.Summon)
            {
                if (EnemyCluster.Size != 20)
                {
                    if (EnemyCluster.Tick >= 20)
                    {
                        Units.EnemyUnit newunit = new Units.EnemyUnit(new Vector2(GetCoordinateFromLocation(mapbuilder.eo), GetCoordinateFromLocation(mapbuilder.es)));
                        newunit.navcoordinate = new Vector2(GetCoordinateFromLocation(mapbuilder.eo), GetCoordinateFromLocation(mapbuilder.es));
                        EnemyCluster.Add(newunit);
                        EnemyCluster.Size++;
                        EnemyCluster.Tick = 0;
                    }

                }
                else
                {
                    EnemyCluster.Summon = false;
                }
            }

            if (EnemyCluster.Count == 20)
            {
                EnemyCluster.X = GetCoordinateFromLocation(mapbuilder.bo);
                EnemyCluster.Y = GetCoordinateFromLocation(mapbuilder.bs);
                EnemyCluster.HasTarget = true;

            }


            if (EnemyCluster.HasTarget && EnemyCluster.Count == 20)
            {
                EnemyCluster.TargetTick++;
                if (EnemyCluster.TargetTick >= 30)
                {
                    EnemyCluster[EnemyCluster.TargetIndex].target = new Vector2(GetLocationFromCoordinate((int)EnemyCluster.X), GetLocationFromCoordinate((int)EnemyCluster.Y));
                    if (EnemyCluster.TargetIndex < EnemyCluster.Count - 1)
                    {
                        EnemyCluster.TargetIndex++;
                    }
                    else if (EnemyCluster.Count == 30)
                    {
                        EnemyCluster.HasTarget = false;
                        EnemyCluster.TargetIndex = 0;
                    }
                    EnemyCluster.TargetTick = 0;
                }


            }

            for (int i = 0; i < EnemyCluster.Count; i++)
            {
                EnemyCluster[i].Update(mapbuilder.map, EnemyCluster);
            }

            #endregion

            if ((clusterA.Count > 0 || clusterB.Count > 0) && EnemyCluster.Count > 0 && !wasmovement)
            {
                wasmovement = true;
            }

            for (int ec = 0; ec < EnemyCluster.Count; ec++)
            {
                int eLocationX;
                int eLocationY;
                if (EnemyCluster.Count > 0)
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
                    if (clusterA.Count > 0 && EnemyCluster.Count > 0)
                    {
                        int fLocationX;
                        int fLocationY;
                        if (clusterA.Count > 0 && EnemyCluster.Count > 0)
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
                    if (clusterB.Count > 0 && EnemyCluster.Count > 0)
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
                        if (clusterB.Count > 0 && EnemyCluster.Count > 0)
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
            else if ((mapbuilder.map[mapbuilder.eo, mapbuilder.es] as UnitBase).isdestroyed || EnemyCluster.Count == 0 && wasmovement)
            {
                if (!Vars.noTextMode)
                {
                    Vars.spriteBatch.DrawString(Vars.font, "GYŐZTÉL (nyomj e-t a kilépéshez)", new Vector2(Vars.ScreenWidth / 2 - 100, Vars.ScreenHeight / 2), Color.Black);
                }
            }
            else if ((mapbuilder.map[mapbuilder.bo, mapbuilder.bs] as UnitBase).isdestroyed || clusterA.Count == 0 && clusterB.Count == 0 && wasmovement)
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
                if (clusterA.Status != null)
                {
                    clusterA.Status.Draw(gameTime);
                }
                if (clusterB.Status != null)
                {
                    clusterB.Status.Draw(gameTime);
                }

            }
        }

        public override void UnloadContent()
        {

        }
    }
}
