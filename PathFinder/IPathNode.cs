using PathFinder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder
{
    public interface IPathNode<TUserContext>:ICoordinate
    {
      
        Boolean IsWalkable(TUserContext inContext);
        Boolean IsWalkable(TUserContext inContext, IPathNode<TUserContext> centernode);
        object UserContext();
    }
}
