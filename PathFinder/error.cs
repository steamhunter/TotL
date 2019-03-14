using SharpDX.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.Error
{
    public class Error
    {
        public static void DialogFileNotFound(string fileName)
        {

            Console.WriteLine("dialógús fájl nem található");
            // Application.Exit();

        }

        public static Game Game { get; set; }

        public static void ShutdownGame()
        {
            Game.Exit();
        }
        public static string BasicError(int errorID)
        {
            Console.WriteLine("hopá valami baj van" + errorID + "hiba");
            return "error";
        }
    }
}
