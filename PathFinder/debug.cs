using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PathFinder.Debug
{
    public class cons
    {

        private static bool vanconsole = false;
        public static void debugMessage(string msg)
        {
            if (vanconsole == false)
            {
                if (NativeMethods.AllocConsole())
                {
                    Console.WriteLine("[DEBUG] " + msg);


                    vanconsole = true;
                }
            }
            else
            {

                Console.WriteLine("[DEBUG] " + msg);
            }

        }

        public static void groupedMessage(string msg,string group)
        {
            
            if (vanconsole == false)
            {
                if (NativeMethods.AllocConsole())
                {
                   Console.WriteLine("["+group+"] " + msg);
                    vanconsole = true;
                }
            }
            else
            {

               Console.WriteLine("["+group+"] " + msg);
            }

        }

        public static void infoMessage(string msg)
        {
            if (vanconsole == false)
            {
                if (NativeMethods.AllocConsole())
                {
                  Console.WriteLine("[INFO] " + msg);


                    vanconsole = true;
                }
            }
            else
            {

              Console.WriteLine("[INFO] " + msg);
            }

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