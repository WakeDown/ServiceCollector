using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ServiceProcess;

namespace SnmpScanner
{
    class CounterService : ServiceBase
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

        EventLog eventLog;

        public CounterService()
        {
            InitializeComponent();

            if (!System.Diagnostics.EventLog.SourceExists("UN1TCounterSource"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "UN1TCounterSource", "UN1TCounterServiceLog");
            }
            eventLog = new EventLog("UN1TCounterServiceLog");
            eventLog.Source = "UN1TCounterSource";
            eventLog.Log = "UN1TCounterServiceLog";
        }

        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            //this.ServiceName = "UN1TCounter";
            //this.CanStop = true;
            //this.CanPauseAndContinue = true;
            this.AutoLog = false;
        }

        protected override void OnStart(string[] args)
        {
            StartWork();
        }

        protected override void OnStop()
        {
            _threadMustStop = true;
        }

        private void StartWork()
        {
            _threadMustStop = false;
            _dbIsBuzy = false;

            _snmpThread = new Thread(SrartScanner);
            _snmpThread.Start();

            _exchangeThread = new Thread(StartExchange);
            _exchangeThread.Start();
        }

        private void StartExchange()
        {
            //Немного откладываем обмен, чтобы обменяться уже с данными
            Thread.Sleep(5 * 60 * 1000);//5 минут

            while (!_threadMustStop)
            {
                eventLog.WriteEntry(String.Format("Последний обмен: {0}", DateTime.Now.ToString("g")));
                new ExchangeDb().DoExchangeDb();

            }
        }

        private void SrartScanner()
        {
            while (!_threadMustStop)
            {
                eventLog.WriteEntry(String.Format("Последний скан: {0}", DateTime.Now.ToString("g")));
                new Scanner().SnmpGet();
            }
        }
    }
}
