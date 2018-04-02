using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder
{
    class LinkedList<t>:System.Collections.Generic.LinkedList<t>
    {
        public void Add(t element)
        {
            AddLast(element);
        }
    }
}
