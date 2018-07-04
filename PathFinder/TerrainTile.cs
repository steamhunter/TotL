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
    

   public abstract class TerrainTile :GameObject, IConnections,IProcGeneralable
    {
        #region Globals
        protected static float unitSize = (Vars.ScreenWidth * 0.83f) / 25f;
        protected List<RectangleF> _blockedvolumes = new List<RectangleF>();
        public float LocationXoffset;
        public float LocationYoffset;
        public Transform transform;
        #endregion

        public TerrainTile(int x,int y)
        {
            transform = GetComponent<Transform>();
            AddComponent(new Drawer(parent));
            transform.setRotation = SetRotation;
            transform.X = x;
            transform.Y = y;
        }
        #region Propertys
        public bool down
        {
            get;
            set;
        }
        public bool left
        {
            get;
            set;
        }
        public bool right
        {
            get;
            set;
        }
        public bool up
        {
            get;
            set;
        }
      
         public float rotation
        {
            get;

            set;
        }

        public float locationX {
            get;
            set;
        }
        public float locationY
        {
            get;
            set;
        }
        public int closedsides
        {
            get;

            set;
        }

        public bool IsPopulated
        {
            get;

            set;
        }

       
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
            for (int i = 0; i < _blockedvolumes.Count && !colision; i++)
            {
                colision = _blockedvolumes[i].Intersects(location);
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
