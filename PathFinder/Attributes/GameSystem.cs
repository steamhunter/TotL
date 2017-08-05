using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.Attributes
{
    [System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
   public sealed class GameSystem : Attribute
    {
        public GameSystemType type;
        public GameSystem(GameSystemType type)
        {
            this.type = type;

        }
       


    }

    
}
