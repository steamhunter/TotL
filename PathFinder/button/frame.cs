using SharpDX;
using SharpDX.Toolkit.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PathFinder._2D;
using SharpDX.Toolkit;
using SharpDX.Direct3D11;
using SharpDX.Toolkit.Input;

namespace PathFinder.Button
{
    //class managment 
    //{
    //    static button[] butons=new button[200];
    //    public static int db = 0;
    //    public static  void registerButton(button Button) 
    //    {
    //        if (db<=200)
    //        {
    //            butons[db] = Button;
    //            db++;
    //        }
    //        else
    //        {
    //           Error.error.basicError(100);
    //        }
 
    //    }
    //}
    public class Button
    {
         protected Vector2 pos;
        protected Vector2 size;
        protected Rectangle buttonRec;
        protected bool initialized=false;
        protected int screenX;
        protected int screenY;
        public Game Game { get; set; }

        protected SharpDX.Toolkit.Graphics.Texture2D texture;
         public string Text = "";
        protected MouseManager mouseManager;
         public virtual void Update()
         {

            
             
         }
         public virtual void Init(int x,int y,int w,int h,MouseManager mouseManager)
         {
            initialized = true;
            this.mouseManager = mouseManager;
            screenX = Game.GraphicsDevice.BackBuffer.Width;
            screenY = Game.GraphicsDevice.BackBuffer.Height;
            pos = new Vector2(Convert.ToSingle(x), Convert.ToSingle(y));
             size = new Vector2(Convert.ToSingle(w), Convert.ToSingle(h));
             buttonRec = new Rectangle(x, y, w, h);
             //managment.registerButton(this);
         }
        public virtual void Draw()
        {
            Vars.spriteBatch.Draw(Components.Drawer.GetTexture("grass",Game) /*ShaderResourceView.FromFile(device, "accel_world_007.jpg")*/, pos,Color.Beige);
        }
       

    }
}
