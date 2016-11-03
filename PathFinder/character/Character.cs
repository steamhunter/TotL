using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PathFinder._2D;
using PathFinder.Character.stuff;
using PathFinder.Character.Managment;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
using SharpDX;

namespace PathFinder.Character
{
    
    public class Base
    {
        public drawing drawing = new drawing();
        public Unit2D unit;
        public bool isdead = false;
        private Game igame;
        protected bool ready = false;
        public Game game
        {
            get
            {
                return igame;
            }
            set
            {
                igame = value;
                ready = true;
            }
        }
        public Base()
        {
            
        }
        virtual public void ifcolide(int count)
        { Console.WriteLine("colide"); }
        public void init(string entity) 
        {
            if (ready)
            {

                unit = new Unit2D(entity,game);
               management.register(this);
            }
            else
            {
               Error.error.basicError(4);
            }
            
        }
        virtual public void update()
        {
            if (ready)
            {

            }
            else
            {
               Error.error.basicError(4);
            }
        }
        public void draw(SpriteBatch spritebatch)
        {
            if (ready)
            {
                if (!isdead)
                {
                    unit.draw(spritebatch);
                }
               
            }
            else
            {
              Error.error.basicError(4);
            }
                
            
           
        }
        
        public static dmg damage = new dmg();
        public int health=100;
        public static string ai;
        
        
        
    }
    public struct drawing
    {
        private Texture2D _texture;
        public Texture2D texture
        {
            get 
            {
                if (_texture!=null)
                {
                    return _texture;
                }
               Error.error.basicError(2);
                return null;
            }
            set 
            {
                _texture = value;
            }
        }
        public Rectangle rectangle;
        public Vector2 location;
    }
    public class CharacterBase:Base
    {
        public CharacterBase ()
        {
            
            
        }   
    }
    public class CharacterEnemyBase:Base
    {
      public CharacterEnemyBase ()
        {
            if (ready)
            {
               // caracter = new caracter("enemy", game);
                unit = new Unit2D("enemy", game);
            }
          
            
        }
        new string ai = "baseattack";
    }
}

