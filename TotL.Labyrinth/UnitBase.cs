using PathFinder;
using PathFinder._2D;
using PathFinder.Debug;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.Toolkit.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotL.Labyrinth
{
    public class UnitBase : Cell
    {
        private Cell realcell;
        public int hp = 10000;
        ShaderResourceView baseTexture;
       public bool isdestroyed = false;
        public string Type { get; set; }

        public UnitBase(Cell realcell, string type,int x,int y):base(x,y)
        {
            baseTexture = TextureFromFile.TextureProcessor.getTexture("UnitBase");
            closedsides = realcell.closedsides;
            this.realcell = realcell;
            locationX = realcell.locationX/*+realcell.LocationXoffset*/;
            locationY = realcell.locationY/*+realcell.LocationYoffset*/;
            X = x;
            Y = y;
            this.Type = type;
        }


        public override void setRotation(float rotation)
        {
            realcell.setRotation(rotation);

        }

        public override void draw()
        {

            realcell.draw();
            if (!isdestroyed)
            {
                if (Type == "enemy")
                {
                    Vars.spriteBatch.Draw(baseTexture, new RectangleF(locationX, locationY, unitSize, unitSize), null, Color.Red, rotation, new Vector2(0, 0), SpriteEffects.None, 0f);
                }
                else
                {
                    Vars.spriteBatch.Draw(baseTexture, new RectangleF(locationX, locationY, unitSize, unitSize), null, Color.Blue, rotation, new Vector2(0, 0), SpriteEffects.None, 0f);
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
    }
}

