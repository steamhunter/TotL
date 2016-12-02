using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotL.labyrinthcells
{

    interface IConnections
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
        float rotation
        {
            get;set;
        }
        void setRotation(float rotation);
        
        
    }
}
