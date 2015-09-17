using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace SnmpScanner.Models
{
    public class Settings
    {
        /// <summary>
        /// В минутах
        /// </summary>
        public int ScanDelay;
        public List<string> SerialNumOidList;
        public List<string> TotlaCounterOidList;
        public List<Device> DeviceList;
        public string MailTo;
        public string MailFrom;
        public string MailSubject;
        public int ContractorId;
        /// <summary>
        /// В часах
        /// </summary>
        public int MinExchangeDelay;
        /// <summary>
        /// В часах
        /// </summary>
        public int MaxExchangeDelay;

        public Settings()
        {
            Load();
        }

        public void Load()
        {
            string path = Path.Combine(Application.StartupPath,ConfigurationManager.AppSettings["settingsPath"]);

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Отсутсвует файл лицензии!");
            }

            string content = File.ReadAllText(path);

            if (Path.GetExtension(path).Contains("un1t"))
            {
                content = Cryptor.Decrypt(content, "Un1tGroup");
            }

            var doc = XDocument.Parse(content);

            ContractorId = (from xml in doc.Descendants("Common")
                            select Convert.ToInt32(xml.Attribute("contractorId").Value)).FirstOrDefault();

            MailFrom = (from xml in doc.Descendants("Common")
                        select xml.Attribute("mailFrom").Value).FirstOrDefault();

            MailTo = (from xml in doc.Descendants("Common")
                      select xml.Attribute("mailTo").Value).FirstOrDefault();
            MailSubject = (from xml in doc.Descendants("Common")
                           select xml.Attribute("mailSubject").Value).FirstOrDefault();

            ScanDelay = (from xml in doc.Descendants("Schedule")
                         select Convert.ToInt32(xml.Attribute("scanDelay").Value)).FirstOrDefault();

            MaxExchangeDelay = (from xml in doc.Descendants("Schedule")
                                select Convert.ToInt32(xml.Attribute("maxExchangeDelay").Value)).FirstOrDefault();

            MinExchangeDelay = (from xml in doc.Descendants("Schedule")
                             select Convert.ToInt32(xml.Attribute("minExchangeDelay").Value)).FirstOrDefault();

            SerialNumOidList = (from xml in doc.Descendants("OidList").Elements("SerialNum")
                select xml.Value).ToList();

            TotlaCounterOidList = (from xml in doc.Descendants("OidList").Elements("TotalCounter")
                                select xml.Value).ToList();

            DeviceList = (from xml in doc.Descendants("DeviceList").Elements("Device")
                          select new Device() {SerialNum = xml.Attribute("serialNum").Value}).ToList();
        }
    }
}
