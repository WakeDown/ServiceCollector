using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SnmpScanner.Models;

namespace SnmpScanner
{
    public class Scanner
    {
        private Thread _snmpThread;
        private Thread _exchangeThread;

        private bool _threadMustStop
        {
            get
            {
                return Program._threadMustStop;
            }
            set
            {
                Program._threadMustStop = value;
            }
        }

        private bool _dbIsBuzy
        {
            get
            {
                return Program._dbIsBuzy;
            }
            set
            {
                Program._dbIsBuzy = value;
            }
        }

        static object gate = new object();

        private Settings AppSettings { get { return Program.AppSettings; } }

        public void SnmpGet()
        {
            //Stopwatch sw = Stopwatch.StartNew();

            if (AppSettings != null && AppSettings.DeviceList.Count > 0)
            {

            }
            else
            {
                throw new Exception("Некорректный файл настроек программы. Не найден список устройств!");
            }

            string[] arrHostList = GetIpList();//Список адресов по которым будем производить сканирование

            if (!arrHostList.Any())
            {
                MessageBox.Show("Укажите диапазон ip адресов для сканирования на вкладке Аппараты");
                lock (gate)
                {
                    _threadMustStop = true;
                }
                return;
            }

            string[] counterOidList = AppSettings.TotlaCounterOidList.ToArray();
            string[] snumOidList = AppSettings.SerialNumOidList.ToArray();

            //if (!_background)
            //{
            
            //}

            foreach (string host in arrHostList)
            {
                lock (gate)
                {
                    if (_threadMustStop) break;
                }

                //if (!_background)
                //{
                //this.Invoke(new Action<string>((s) => lblCurrHost.Text = s), host);
                //this.Invoke(new Action(this.Refresh));
                //}

                //Проверяем есть ли кто на том конце провода
                Ping p = new Ping();
                PingReply r = p.Send(host);
                if (r != null && r.Status != IPStatus.Success) continue;

                foreach (string snumOid in snumOidList)
                {
                    string serialNum = GetSnmpValue(host, snumOid.Trim());

                    //Если серийный номер не существует в списке настроек, то продолжаем сканирование
                    if (String.IsNullOrEmpty(serialNum.Trim()) || AppSettings.DeviceList.All(x => !serialNum.ToUpper().Contains(x.SerialNum.Trim().ToUpper()))) continue;

                    //Request requestSerialNum = new Request() { Oid = snumOid, Value = serialNum, OidType = "SerialNum", DateRequest = DateTime.Now, Host = host };
                    //requestSerialNum.Save();

                    foreach (string counterOid in counterOidList)
                    {
                        string totlaCounter = GetSnmpValue(host, counterOid);
                        Request requestTotalCounter = new Request() { SerialNum = serialNum, Oid = counterOid, Value = totlaCounter, OidType = "TotalCounter", DateRequest = DateTime.Now, Host = host };

                        lock (gate)
                        {
                            while (_dbIsBuzy) { Thread.Sleep(10); }
                        }

                        lock (gate)
                        {
                            _dbIsBuzy = true;
                        }

                        try
                        {
                            requestTotalCounter.Save();
                        }
                        finally
                        {
                            lock (gate)
                            {
                                _dbIsBuzy = false;
                            }
                        }
                    }
                }

                //string serialNum = GetSnmpValue(host, snumOid);
            }

            //if (!_background)
            //{
            //this.Invoke(new Action<string>((s) => lblCurrHost.Text = s), "завершено");
            //this.Invoke(new Action(this.Refresh));
            //}

            if (!_threadMustStop) Thread.Sleep(AppSettings.ScanDelay * 60 * 1000);//Перерыв в работе
        }

        private static string GetSnmpValue(string ipHost, string oid)
        {
            if (String.IsNullOrEmpty(ipHost) || String.IsNullOrEmpty(oid))
            {
                return String.Empty;
            }

            string output;
            output = Snmp.RunProgram(ipHost, oid);
            return output;
        }

        private static string[] GetIpList()
        {
            IpRange[] lst = IpRange.GetList();

            List<string> lstIpList = new List<string>();

            foreach (IpRange range in lst)
            {
                string[] arrIpList = Helpers.GetIpAdressFromRange(range.IpFrom, range.IpTo).ToArray();
                lstIpList.AddRange(arrIpList);
            }

            return lstIpList.ToArray();
        }
    }
}
