using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder
{
    interface IRotatable: IDrawable
    {
        /// <summary>
        /// beálítja a forgatásí irányát
        /// </summary>
        /// <param name="rotation">forgatás iránya a Totl.Labyrith.Rotations szerint</param>
        void SetRotation(float rotation);
        float Rotation
        {
            get; set;
        }
    }
}
