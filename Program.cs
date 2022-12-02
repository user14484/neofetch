//#define DEBUG
using System;
using System.Collections.Generic;

namespace neofetch
{
    class Program
    {
        static Funcions program = new Funcions();
        static Dictionary<int, string> logo = new Dictionary<int, string>()
        {
            { 1,  "                                             G9999  " },
            { 2,  "                                 B9999999999999999  " },
            { 3,  "                     # 999999999999999999999999999  " },
            { 4,  "          M99999999999 999999999999999999999999999  " },
            { 5,  " 999999999999999999999 999999999999999999999999999  " },
            { 6,  " 999999999999999999999 999999999999999999999999999  " },
            { 7,  " 999999999999999999999 999999999999999999999999999  " },
            { 8,  " 999999999999999999999 999999999999999999999999999  " },
            { 9,  " 999999999999999999999 999999999999999999999999999  " },
            { 10, " 999999999999999999999 999999999999999999999999999  " },
            { 11, " 999999999999999999999 999999999999999999999999999  " },
            { 12, " 999999999999999999999 999999999999999999999999999  " },
            { 13, "                                                   " },
            { 14, " ##################### ###########################  " },
            { 15, " 999999999999999999999 999999999999999999999999999  " },
            { 16, " 999999999999999999999 999999999999999999999999999  " },
            { 17, " 999999999999999999999 999999999999999999999999999  " },
            { 18, " 999999999999999999999 999999999999999999999999999  " },
            { 19, " 999999999999999999999 999999999999999999999999999  " },
            { 20, " 999999999999999999999 999999999999999999999999999  " },
            { 21, " 999999999999999999999 999999999999999999999999999  " },
            { 22, " h99999999999999999999 999999999999999999999999999  " },
            { 23, "            B999999999 999999999999999999999999999  " },
            { 24, "                       H99999999999999999999999999  " },
            { 25, "                                  H999999999999999  " },
            { 26, "                                             #h999  " }
        };


        static Dictionary<int, string> Info = new Dictionary<int, string>()
        {
            { 0, string.Format("\x1B[93mOS:\x1B[0m  {0} ({1})", program.getOSInfo(), Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE", EnvironmentVariableTarget.Machine)) },
            { 1, string.Format("\x1B[93mCPU:\x1B[0m {0}", program.GetHardwareInfo("Win32_Processor", "Name")[0]) },
            { 2, string.Format("\x1B[93mGPU:\x1B[0m {0}", program.GetGPU())},
            { 3, string.Format("\x1B[93mUser:\x1B[0m {0}", Environment.UserName) },
            { 4, string.Format("\x1B[93mRAM:\x1B[0m {0}Gb", program.GetRAM()/1024/1024/1024) },
            { 5, string.Format("\x1B[93mUptime:\x1B[0m {0}", program.ToReadableString(program.GetUptime)) }
        };

        static void begin()
        {
            int i = 0;
            foreach (string line in logo.Values)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(line);
                Console.ForegroundColor = ConsoleColor.White;

                if (i < Info.Values.Count)
                {
                    Console.WriteLine(Info[i]);
                }
                else
                    Console.WriteLine();

                i++;
            }
        }


        static void Main(string[] args)
        {
#if (DEBUG)
            program.ProgramStart();
#endif
            program.Clear();
            begin();
#if (DEBUG)
            program.ProgramEnd();
#endif
        }
    }
}
