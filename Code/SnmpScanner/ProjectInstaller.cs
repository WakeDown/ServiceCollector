using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace SnmpScanner
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();

            //EventLogInstaller installer = FindInstaller(this.Installers);
            //if (installer != null)
            //{
            //    installer.Log = "UN1TCounterServiceLog";
            //}
        }

        //private EventLogInstaller FindInstaller(InstallerCollection installers)
        //{
        //    foreach (Installer installer in installers)
        //    {
        //        if (installer is EventLogInstaller)
        //        {
        //            return (EventLogInstaller)installer;
        //        }

        //        EventLogInstaller eventLogInstaller = FindInstaller(installer.Installers);
        //        if (eventLogInstaller != null)
        //        {
        //            return eventLogInstaller;
        //        }
        //    }
        //    return null;
        //}

        private void serviceInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {
            if (Program.StartService)
            {
                using (ServiceController sc = new ServiceController(serviceInstaller1.ServiceName))
                {
                    sc.Start();
                }
            }
        }

        private void serviceInstaller1_BeforeUninstall(object sender, InstallEventArgs e)
        {
            //using (ServiceController sc = new ServiceController(serviceInstaller1.ServiceName))
            //{
            //    sc.Stop();
            //}
        }

        private void serviceProcessInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {

        }
    }
}
