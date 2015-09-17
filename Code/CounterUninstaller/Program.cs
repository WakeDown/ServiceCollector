using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CounterUninstaller
{
    class Program
    {
        static void Main(string[] args)
        {
            string fldr = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            string path = Path.Combine(fldr, @"Program\UN1TCounter.exe");

            var psi = new ProcessStartInfo(String.Format("\"{0}\"", path), "uninstall")
            {
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (var process = Process.Start(psi))
            {
                //process.WaitForExit();
            }
        }
    }
}
