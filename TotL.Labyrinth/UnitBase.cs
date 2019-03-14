﻿using PathFinder;
using PathFinder._2D;
using PathFinder.Components;
using PathFinder.Debug;
using PathFinder.Scene;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotL.Labyrinth
{
    public class UnitBase : TerrainTile
    {
        private TerrainTile realcell;
        public int hp = 10000;
        ShaderResourceView baseTexture;
       public bool isdestroyed = false;
        public string Type { get; set; }

        public UnitBase(TerrainTile realcell, string type,int x,int y):base(x,y)
        {
            baseTexture =TextureLoader.GetTexture("UnitBase");
            ClosedSides = realcell.ClosedSides;
            this.realcell = realcell;
            LocationX = realcell.LocationX/*+realcell.LocationXoffset*/;
            LocationY = realcell.LocationY/*+realcell.LocationYoffset*/;
            this.Type = type;
        }


        public override void SetRotation(float rotation)
        {
            realcell.SetRotation(rotation);

        }

        public override void Draw(GameTime gameTime)
        {

            realcell.Draw(gameTime);
            if (!isdestroyed)
            {
                if (Type == "enemy")
                {
                    Vars.spriteBatch.Draw(baseTexture, new RectangleF(LocationX, LocationY, unitSize, unitSize), null, Color.Red, Rotation, new Vector2(0, 0), SpriteEffects.None, 0f);
                }
                else
                {
                    Vars.spriteBatch.Draw(baseTexture, new RectangleF(LocationX, LocationY, unitSize, unitSize), null, Color.Blue, Rotation, new Vector2(0, 0), SpriteEffects.None, 0f);
                }
            }
           



        }
        public void Damagebuilding(int enemydmg,string attackertype)
        {
            if (attackertype!=Type&&!isdestroyed)
            {
                
                hp -= enemydmg ;

                if (hp <= 0)
                {
                    isdestroyed = true;
                }
            }
            
        }

        public override void SetBlockingVolumes()
        {

            realcell.SetBlockingVolumes();
        }

        public override bool CheckFitting(Connection[,] connect, int co, int cs, int o, int s)
        {
           return realcell.CheckFitting(connect, co, cs, o, s);

        }

        public override void Update(GameTime gameTime)
        {
           
        }

        public override void LoadContent()
        {
            
        }

        public override void Initialize()
        {
            
        }

        public override void UnloadContent()
        {
            
        }
    }
}

