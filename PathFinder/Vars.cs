using PathFinder.Toolkit.Graphics;
using SharpDX.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder
{
    public class Vars
    {
        public static gamestates state = gamestates.world;
        public static SpriteBatch spriteBatch;
        public static SharpDX.Direct3D11.Device device;
        public static int ScreenWidth;
        public static int ScreenHeight;
        public static Game game;
        public static int seed;
        public static Random random;


        
    }
    public enum gamestates
    {
        
        initializing,
        initialized_notLoaded,
        intitalized_loaded,
        onreset,
        world


    }
}
