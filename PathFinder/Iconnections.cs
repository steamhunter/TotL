using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder
{

   public interface IConnections
    {
        
       
        int ClosedSides
        {
            get;
            set;
        }
        bool Up
        {
            get;
            set;

        }
        bool Down
        {
            get;
            set;

        }
        bool Left
        {
            get;
            set;
        }

        bool Right
        {
            get;
            set;
        }
        
        
        
    }
}
