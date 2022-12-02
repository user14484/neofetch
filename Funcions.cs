using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Management;

namespace neofetch
{
    class Funcions
    {
        public Stopwatch stopwatch = new Stopwatch();
        public void ProgramStart()
        {
            stopwatch.Start();
        }

        public void ProgramEnd()
        {
            stopwatch.Stop();
            Console.WriteLine("Время выполнения: " + stopwatch.ElapsedMilliseconds + " милисекунд");
            Console.WriteLine("Нажмите любую клавишу что бы продолжить...");
            Console.ReadKey();
        }

        public Dictionary<int, string> WriteFileConsole(string path)
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            int lines = 0;
            using (TextReader reader = File.OpenText(path))
            {
                using (TextReader reader2 = File.OpenText(path))
                {
                    while (reader2.ReadLine() != null) {
                        result.Add(lines, "");
                        lines++;
                    }
                }
                for(int i = 0; i <= lines; i++)
                {
                    result[i] = reader.ReadLine();
                }
            }
            return result;
        }

        public string getOSInfo()
        {
            OperatingSystem os = Environment.OSVersion;
            Version vs = os.Version;

            string operatingSystem = "";

            if (os.Platform == PlatformID.Win32Windows)
            {
                switch (vs.Minor)
                {
                    case 0:
                        operatingSystem = "95";
                        break;
                    case 10:
                        if (vs.Revision.ToString() == "2222A")
                            operatingSystem = "98SE";
                        else
                            operatingSystem = "98";
                        break;
                    case 90:
                        operatingSystem = "Me";
                        break;
                    default:
                        break;
                }
            }
            else if (os.Platform == PlatformID.Win32NT)
            {
                switch (vs.Major)
                {
                    case 3:
                        operatingSystem = "NT 3.51";
                        break;
                    case 4:
                        operatingSystem = "NT 4.0";
                        break;
                    case 5:
                        if (vs.Minor == 0)
                            operatingSystem = "2000";
                        else
                            operatingSystem = "XP";
                        break;
                    case 6:
                        if (vs.Minor == 0)
                            operatingSystem = "Vista";
                        else if (vs.Minor == 1)
                            operatingSystem = "7";
                        else if (vs.Minor == 2)
                            operatingSystem = "8";
                        else
                            operatingSystem = "8.1";
                        break;
                    case 10:
                        operatingSystem = "10";
                        break;
                    default:
                        break;
                }
            }
            if (operatingSystem != "")
            {
                operatingSystem = "Windows " + operatingSystem;
                if (os.ServicePack != "")
                {
                    operatingSystem += " " + os.ServicePack;
                }
            }
            return operatingSystem;
        }
        public List<string> GetHardwareInfo(string WIN32_Class, string ClassItemField)
        {
            List<string> result = new List<string>();

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM " + WIN32_Class);

            try
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    result.Add(obj[ClassItemField].ToString().Trim());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public string GetGPU() {
            string result = "NULL";
            foreach(string str in this.GetHardwareInfo("Win32_VideoController", "Name"))
            {
                if(str.Contains("NVIDIA"))
                    result = str;
                else if(str.Contains("AMD"))
                    result = str;
            }
            return result;
        }

        public ulong GetRAM()
        {
            string Query = "SELECT Capacity FROM Win32_PhysicalMemory";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(Query);

            UInt64 Capacity = 0;
            foreach (ManagementObject WniPART in searcher.Get())
            {
                Capacity += Convert.ToUInt64(WniPART.Properties["Capacity"].Value);
            }

            return Capacity;
        }

        /*public TimeSpan GetUptime1()
        {
            ManagementObject mo = new ManagementObject(@"\\.\root\cimv2:Win32_OperatingSystem=@");
            DateTime lastBootUp = ManagementDateTimeConverter.ToDateTime(mo["LastBootUpTime"].ToString());
            return DateTime.Now.ToUniversalTime() - lastBootUp.ToUniversalTime();
        }*/

        public TimeSpan GetUptime
        {
            get
            {
                using (var uptime = new PerformanceCounter("System", "System Up Time"))
                {
                    uptime.NextValue();       //Call this an extra time before reading its value
                    return TimeSpan.FromSeconds(uptime.NextValue());
                }
            }
        }

        public string ToReadableString(TimeSpan span)
        {
            string formatted = string.Format("{0}{1}{2}",
                span.Duration().Days > 0 ? string.Format("{0:0} day{1}, ", span.Days, span.Days == 1 ? string.Empty : "s") : string.Empty,
                span.Duration().Hours > 0 ? string.Format("{0:0} hour{1}, ", span.Hours, span.Hours == 1 ? string.Empty : "s") : string.Empty,
                span.Duration().Minutes > 0 ? string.Format("{0:0} minute{1}, ", span.Minutes, span.Minutes == 1 ? string.Empty : "s") : string.Empty,
                span.Duration().Seconds > 0 ? string.Format("{0:0} second{1}", span.Seconds, span.Seconds == 1 ? string.Empty : "s") : string.Empty);

            if (formatted.EndsWith(", ")) formatted = formatted.Substring(0, formatted.Length - 2);

            if (string.IsNullOrEmpty(formatted)) formatted = "0 seconds";

            return formatted;
        }

        public void Clear()
        {
            Console.Write("\x1B[2J\x1B[H");
        }
    }
}
