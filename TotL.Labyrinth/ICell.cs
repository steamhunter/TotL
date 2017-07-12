using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotL.Labyrinth
{
    interface ICell
    {
        bool isPopulated
        {
            get; set;
        }
        float rotation
        {
            get; set;
        }
        void setRotation(float rotation);
    }
}
