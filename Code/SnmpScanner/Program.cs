using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Configuration.Install;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Windows.Forms;
using SnmpScanner.Models;

namespace SnmpScanner
{
    //1.1   
    //  добавлена возможность работы с Exchange
    //  добавлено поле копия при обмене через почту 

    static class Program
    {
        public const string progVersion = "1.2";
        public static bool _threadMustStop;
        public static bool _dbIsBuzy;

        public static bool StartService = false;

        public static Settings AppSettings
        {
            get
            {
                return new Settings();
            }
        }

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //Проверяем наличие файла лицензии

            //args = new[] { "frm" };

            _threadMustStop = false;
            _dbIsBuzy = false;

            //if (System.Environment.UserInteractive)
            //{
            //    Application.EnableVisualStyles();
            //    Application.SetCompatibleTextRenderingDefault(false);
            //    Application.Run(new Form1());


            //}
            //else
            //{

            try
            {

                if (args.Any())
                {
                    switch (args[0])
                    {
                        default:
                            StartWinService();
                            break;
                        case "install":
                            StartService = true;
                            MessageBox.Show(SelfInstaller.InstallMe());
                            break;
                        case "installnostart":
                            StartService = false;
                            MessageBox.Show(SelfInstaller.InstallMe());
                            break;
                        case "uninstall":
                            MessageBox.Show(SelfInstaller.UninstallMe());
                            break;
                        case "frm":
                            Application.EnableVisualStyles();
                            Application.SetCompatibleTextRenderingDefault(false);
                            Application.Run( new Form1());
                            break;
                    }
                }
                else
                {
                    StartWinService();
                }
            }
            catch (FileNotFoundException ex)
            {
                //throw new ApplicationException("Отсутсвует файл с настройками!");
                MessageBox.Show("Отсутствует файл лицензии!");
                Application.Exit();
                Environment.Exit(-1);
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(String.Format("Что-то пошло не так, сообщите разработчикам информацию представленную ниже!/r/nMessage: {0}/r/n/Source: {1}/r/n/StackTrace: {2}", ex.Message, ex.Source, ex.StackTrace));
                Application.Exit();
                Environment.Exit(-1);
                //throw new ApplicationException("Некорректный файл с настройками!");
            }
            //}
        }

        public static void StartWinService()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] { new CounterService() };
            ServiceBase.Run(ServicesToRun);
        }

        public static int GetExchangeDelayValue()
        {
            int exchangeDelayVlaue = AppSettings.MinExchangeDelay;

            int currExchangeDelayVlaue;
            int.TryParse(ConfigurationManager.AppSettings["exchangeDelay"], out currExchangeDelayVlaue);

            if (exchangeDelayVlaue > currExchangeDelayVlaue)
            {
                return exchangeDelayVlaue;

            }
            else
            {
                return currExchangeDelayVlaue;
            }
        }
    }

    public static class SelfInstaller
    {
        private static readonly string _exePath =
            Assembly.GetExecutingAssembly().Location;
        public static string InstallMe()
        {
            try
            {
                ManagedInstallerClass.InstallHelper(
                    new string[] { _exePath });
            }
            catch (Exception exception)
            {
                return exception.Message + "\r\n" + exception.InnerException;
            }
            return "Служба UN1TCounter успешно установлена.";
        }

        public static string UninstallMe()
        {
            try
            {
                ManagedInstallerClass.InstallHelper(
                    new string[] { "/u", _exePath });
            }
            catch (Exception exception)
            {
                return exception.Message + "\r\n" + exception.InnerException;
            }
            return "Служба UN1TCounter успешно удалена.";
        }
    }


}
