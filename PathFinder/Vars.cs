using PathFinder.Toolkit.Graphics;
using SharpDX.Toolkit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

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
        public static configjson.configstructure config;
        public static float unitSize;



    }
    public class configjson
    {
        public class configstructure
        {
            public int fc_weight { get; set; }
            public int cross_weight { get; set; }
            public int deadend_weight { get; set; }
            public int oneside_weight { get; set; }
            public int twoside_weight { get; set; }
            public int tunnel_weight { get; set; }
        }

        public static configstructure getConfig()
        {
            string jsonstring = File.ReadAllText("config.json");
            JavaScriptSerializer ser = new JavaScriptSerializer();
            configstructure jres = ser.Deserialize<configstructure>(jsonstring);
            return jres;
        }
    }
    public enum gamestates
    {
        
        initializing,
        initialized_notLoaded,
        intitalized_loaded,
        world


    }
}
