using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFinder.Toolkit;
using PathFinder.Debug;
using PathFinder;

namespace TotL
{
    class Program
    {
        static void Main(string[] args)
        {

#if DEBUG
            cons.onDebug = true;
            cons.debugMessage("debug enabled", "info");
#endif
            foreach (string ar in args)
            {

                if (ar == "-console")
                {
                    cons.onDebug = true;
                    cons.debugMessage("debug enabled","info");

                }
                if (ar=="-path-debug-draw")
                {
                    Vars.path_debug_Draw = true;
                }
                if (ar=="-no-text")
                {
                    Vars.noTextMode = true;
                }
            }
            using (TotL game = new TotL())
            {
                game.Run();
            }

          
        }
       
    }
}
