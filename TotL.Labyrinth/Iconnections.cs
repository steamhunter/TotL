﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotL.Labyrinth
{

   public interface IConnections
    {
        int X { get; set; }
        int Y { get; set; }
        bool isPopulated
        {
            get;set;
        }
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
