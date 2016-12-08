using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFinder.Toolkit;
using PathFinder.Debug;

namespace TotL
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (string ar in args)
            {
                if (ar == "-console")
                {
                    cons.debugMessage("debug enabled");

                }
                else
                {
                    
                }
            }
            using (TotL game = new TotL())
            {
                game.Run();
            }

          
        }
       
    }
}
