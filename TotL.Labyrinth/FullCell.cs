using PathFinder;
using PathFinder._2D;
using PathFinder.Components;
using PathFinder.Scene;
using SharpDX;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotL.Labyrinth
{
    public class FullCell:TerrainTile
    {
        public FullCell(int x,int y):base(x,y)
        {
           GetComponent<Drawer>().Texture = TextureLoader.getTexture("FullCell");
            
            SetRotation(0f);
            closedsides = 4;
        }

        public override void SetRotation(float rotation)
        {
            base.Rotation = 0;
           
                up = false;
                left = false;
                down = false;
                right = false;
           

        }

        public override void Update(GameTime gameTime)
        {

        }
        public override void Draw(GameTime gameTime)
        {

            float unitSize = (Vars.ScreenWidth * 0.83f) / 25f;
              Vars.spriteBatch.Draw(GetComponent<Drawer>().Texture, new RectangleF(LocationX, LocationY, unitSize, unitSize), null, Color.White, Rotation, new Vector2(0, 0), SpriteEffects.None, 0f);

        }
        public override void SetBlockingVolumes()
        {
            _blockedvolumes.Add(GetComponent<Drawer>().Rectangle);
            //base.SetBlockingVolumes();
        }
        public override bool CheckFitting(Connection[,] connect, int co, int cs, int o, int s)
        {
            if (closedsides >= Connection.getClosedSides(connect[co, cs-1], connect[co+1, cs], connect[co, cs+1], connect[co-1, cs]))
            {
                if (Connection.isFiting(this, connect[co, cs - 1], connect[co + 1, cs], connect[co, cs + 1], connect[co - 1, cs]))
                {
                    LocationX = 20 + ((o) * unitSize);
                    LocationY = 20 + ((s) * unitSize);
                    connect[co,cs].up = up;
                    connect[co,cs].down = down;
                    connect[co,cs].left = left;
                    connect[co,cs].right = right;
                    return true;

                }
                else
                {
                    return false;
                }
                
                   
            }
            else
            {
                return false;
            }


        }
        public override string ToString()
        {
            return "FullCell";
        }

        public override void LoadContent()
        {
            throw new NotImplementedException();
        }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public override void UnloadContent()
        {
            throw new NotImplementedException();
        }
    }
}

