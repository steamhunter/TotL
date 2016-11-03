using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PathFinder._2D;

namespace PathFinder.Character.Managment 
{
    public class management
    {
        static List<Base> list = new List<Base>();
        public static void register(Base charact)
        {
            list.Add( charact);
        }
        public static void colision(Unit2D player)
        {
            foreach (var item in list)
            {
                if (item.unit.rectangle.Intersects(player.rectangle) )
                {
                    item.ifcolide(1);
                }
            }
           
        }
    }

}
