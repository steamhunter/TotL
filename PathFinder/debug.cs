using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PathFinder.Debug
{
    public class cons
    {

        private static bool hasConsole = false;
        private static StreamWriter logger = new StreamWriter("logs/debug.log");
        private static StreamWriter genlogger = new StreamWriter("logs/generator.log");

        public static bool OnDebug { get; set; } = false;

        public static void DebugMessage(string message,string subGroup)
        {
            cons.GroupedMessage(message,"DEBUG",subGroup);
            

        }

        public static void EndLog()
        {
            GroupedMessage("stoping logger", "log", "exit_event");
            logger.Close();
            genlogger.Close();
        }
        public static void GroupedMessage(string message,string group,string subGroup)
        {
            if (OnDebug)
            {
              
                if (hasConsole == false)
                {
                    if (NativeMethods.AllocConsole())
                    {
                        string combinedMessage = $"[{group.ToUpper()}] [{subGroup.ToUpper()}] " + message;
                        if (group.ToUpper() == "GENERATOR")
                            genlogger.WriteLine(combinedMessage);
                        else
                            logger.WriteLine(combinedMessage);
                        Console.WriteLine(combinedMessage);
                        hasConsole = true;
                    }
                }
                else
                {
                    string combinedMessage = $"[{group.ToUpper()}] [{subGroup.ToUpper()}] " + message;
                    if (group.ToUpper() == "GENERATOR")
                        genlogger.WriteLine(combinedMessage);
                    else
                        logger.WriteLine(combinedMessage);
                    Console.WriteLine($"[{group.ToUpper()}] [{subGroup.ToUpper()}] " + message);
                }
            }
            

        }



        public static void InfoMessage(string message,string subGroup)
        {
            cons.GroupedMessage(message,"INFO",subGroup);
            

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