using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFinder;

namespace TotL.labyrinthcells
{
    class Connection : IConnections
    {
        private bool _up,_down,_left,_right;
        private float _rotation = 0f;
        public bool down
        {
            get
            {
                return _down;
            }

            set
            {
                _down = value;
            }
        }

        public bool left
        {
            get
            {
                return _left;
            }

            set
            {
                _left = value;
            }
        }

        public bool right
        {
            get
            {
                return _right;
            }

            set
            {
                _right = value;
            }
        }

        public float rotation
        {
            get
            {
                return _rotation;
            }

            set
            {
                _rotation = value;
            }
        }

        public bool up
        {
            get
            {
                return _up;
            }

            set
            {
                _up = value;
            }
        }

        public float rotate(float rotate)
        {
            throw new InvalidCallException("rotate nem hívható a connection osztályon");
        }
    }
}
