using PathFinder;
using PathFinder._2D;
using PathFinder.Components;
using PathFinder.Scene;
using SharpDX;
using SharpDX.Toolkit.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder
{
    

   public abstract class TerrainTile :GameObject, IConnections,IProcedualyGenerateable
    {
        #region Globals
        protected static float unitSize = (Vars.ScreenWidth * 0.83f) / 25f;
        protected List<RectangleF> _blockedVolumes = new List<RectangleF>();
        #endregion

        public TerrainTile(int x,int y)
        {
            Transform = GetComponent<Transform>();
            AddComponent(new Drawer(Parent));
            Transform.setRotation = SetRotation;
            Transform.X = x;
            Transform.Y = y;
        }
        #region Propertys
        public bool Down
        {
            get;
            set;
        }
        public bool Left
        {
            get;
            set;
        }
        public bool Right
        {
            get;
            set;
        }
        public bool Up
        {
            get;
            set;
        }
      
         public float Rotation
        {
            get;

            set;
        }

        public float LocationX {
            get;
            set;
        }
        public float LocationY
        {
            get;
            set;
        }
        public int ClosedSides
        {
            get;

            set;
        }

        public bool IsPopulated
        {
            get;

            set;
        }
        public float LocationXoffset { get; set; }
        public float LocationYoffset { get; set; }
        public Transform Transform { get; set; }


        #endregion



        /// <summary>
        /// létrehozza a falat jelentö akadályt
        /// KÖTELEZÖ FELÜLIRNI
        /// nincs megvalósítás
        /// throws InvalidCallException()
        /// </summary>
        public virtual void SetBlockingVolumes()
        {
            throw new InvalidCallException("hívás a Cell alap fügvényre" + GetType().ToString());
        }

        /// <summary>
        /// elenörzi hogy a kordináta falban van e?
        /// </summary>
        /// <param name="location">az ellenörizendő koordináta</param>
        /// <returns>true: benne van false: nincs</returns>
        public virtual bool IsIntersectsWith(RectangleF location)
        {
            
            bool colision = false;
            for (int i = 0; i < _blockedVolumes.Count && !colision; i++)
            {
                colision = _blockedVolumes[i].Intersects(location);
            }
            return colision;
        }

        /// <summary>
        /// elenörzi hogy a cella illeszkedik e a Kapcsolat mátrixba
        /// </summary>
        /// <param name="connect"> a kapcsolatokat tartalmazó mátrix</param>
        /// <param name="co">oszlop koordináta a connect mátrix szerint</param>
        /// <param name="cs">sor kordináta a connect mátrix szerint</param>
        /// <param name="o">oszlop a Cell mátrix szerint</param>
        /// <param name="s">sor a Cell mátrix szerint</param>
        /// <returns> true ha illesztkedik</returns>
        public virtual bool CheckFitting( Connection[,] connect ,int co,int cs,int o,int s)
        {
            throw new PathFinder.InvalidCallException("hívás a Cell alap fügvényre"+this.GetType().ToString());
        }

        public virtual void SetRotation(float rotation)
        {
            throw new PathFinder.InvalidCallException("hívás a Cell alap fügvényre" + this.GetType().ToString());
        }
    }
}
