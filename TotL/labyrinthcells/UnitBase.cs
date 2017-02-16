using PathFinder;
using PathFinder._2D;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.Toolkit.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotL.labyrinthcells
{
    class UnitBase :Cell
    {
        private Cell realcell;
        ShaderResourceView baseTexture;
       
        public UnitBase(Cell realcell)
        {
            baseTexture = TextureFromFile.TextureProcessor.getTexture("UnitBase");
            closedsides = realcell.closedsides;
            this.realcell = realcell;
            locationX = realcell.locationX;
            locationY = realcell.locationY;
        }


        public override void setRotation(float rotation)
        {
            realcell.setRotation(rotation);

        }

        public override void draw()
        {
           
                realcell.draw();
                Vars.spriteBatch.Draw(baseTexture, new RectangleF(locationX, locationY, unitSize, unitSize), null, Color.White, rotation, new Vector2(0, 0), SpriteEffects.None, 0f);
           
           
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

