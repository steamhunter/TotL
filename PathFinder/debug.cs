using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PathFinder.Debug
{
    public class cons
    {

        private static bool vanconsole = false;
        public static bool onDebug = false;
        private static StreamWriter logger = new StreamWriter("logs/debug.log");
        private static StreamWriter genlogger = new StreamWriter("logs/generator.log");
        public static void debugMessage(string msg,string subgroup)
        {
            cons.groupedMessage(msg,"DEBUG",subgroup);
            

        }

        public static void Endlog()
        {
            groupedMessage("stoping logger", "log", "exit_event");
            logger.Close();
            genlogger.Close();
        }
        public static void groupedMessage(string msg,string group,string subgroup)
        {
            if (onDebug)
            {
              
                if (vanconsole == false)
                {
                    if (NativeMethods.AllocConsole())
                    {
                        string compmsg = $"[{group.ToUpper()}] [{subgroup.ToUpper()}] " + msg;
                        if (group.ToUpper() == "GENERATOR")
                            genlogger.WriteLine(compmsg);
                        else
                            logger.WriteLine(compmsg);
                        Console.WriteLine(compmsg);
                        vanconsole = true;
                    }
                }
                else
                {
                    string compmsg = $"[{group.ToUpper()}] [{subgroup.ToUpper()}] " + msg;
                    if (group.ToUpper() == "GENERATOR")
                        genlogger.WriteLine(compmsg);
                    else
                        logger.WriteLine(compmsg);
                    Console.WriteLine($"[{group.ToUpper()}] [{subgroup.ToUpper()}] " + msg);
                }
            }
            

        }



        public static void infoMessage(string msg,string subgroup)
        {
            cons.groupedMessage(msg,"INFO",subgroup);
            

        }

        private partial class NativeMethods
        {
            public static Int32 STD_OUTPUT_HANDLE = -11;

            /// Return Type: HANDLE->void*
            ///nStdHandle: DWORD->unsigned int
            [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint = "GetStdHandle")]
            public static extern System.IntPtr GetStdHandle(Int32 nStdHandle);

            /// Return Type: BOOL->int
            [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint = "AllocConsole")]
            [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
            public static extern bool AllocConsole();
        }
    }
}