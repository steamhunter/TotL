﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotL.Labyrinth
{
    public interface IPathNode<TUserContext>
    {
        Boolean IsWalkable(TUserContext inContext);
        Boolean IsWalkable(TUserContext inContext, IPathNode<TUserContext> centernode);
        object UserContext();
        int X { get; set; }
        int Y { get; set; }
    }
}
