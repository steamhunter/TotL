using SharpDX.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.Error
{
    public class error
    {
        public static void DialogFileNotFound(string filename)
        {

            Console.WriteLine("dialógús fájl nem található");
            // Application.Exit();

        }
        public static Game game;
        
        public static void shutgame()
        {
            game.Exit();
        }
        public static string basicError(int eid)
        {
            Console.WriteLine("hopá valami baj van" + eid + "hiba");
            return "error";
        }
    }
}
