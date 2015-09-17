using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Exchange.WebServices.Data;

namespace SnmpScanner.Models
{
    public class Log
    {
        public static void Write(string msg)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "log.txt");
            using (StreamWriter sw = (File.Exists(path)) ? File.AppendText(path) : File.CreateText(path))
            {
                sw.WriteLine(String.Format("{0:dd.MM.yyyy hh:MM:ss}:\r\n{1}", DateTime.Now, msg));
            }    
        }
    }
}
