using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder
{

   public interface IConnections: ICoordinate
    {
        
       
        int closedsides
        {
            get;
            set;
        }
        bool up
        {
            get;
            set;

        }
        bool down
        {
            get;
            set;

        }
        bool left
        {
            get;
            set;
        }

        bool right
        {
            get;
            set;
        }
        
        
        
    }
}
