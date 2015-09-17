using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SnmpScanner.Models;

namespace SnmpScanner
{
    public class EmailSettings
    {
        public MailAddress MailFrom;
        public MailAddress MailTo;

        public string Host;
        public int Port;
        public string Login;
        public string Password;
        public bool EnableSsl;

        public bool Save2SentFolder;

        public string SentMethod;
        public string SentHost;
        public int SentPort;
        public string SentLogin;
        public string SentPassword;
        public bool SentEnableSsl;

        public string MsExchangeVersion;
        public string MsExchangeLogin;
        public string MsExchangePass;

        public string ServerType;

        public string MailCopyTo;

        public EmailSettings(bool defaultSettings)
        {
            var settings = new Settings();
            MailTo = new MailAddress(settings.MailTo);
            MailFrom = new MailAddress(settings.MailFrom);

            if (defaultSettings) LoadDefault();
        }

        private void LoadDefault()
        {
            ServerType = ConfigurationManager.AppSettings["serverType"];

            Host = ConfigurationManager.AppSettings["smtpHost"];

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["smtpPort"]))
            {
                Port = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"]);
            }

            Login = ConfigurationManager.AppSettings["smtpLogin"];

            string pass = ConfigurationManager.AppSettings["smtpPassword"];
            Password = String.IsNullOrEmpty(pass) ? pass : Cryptor.Decrypt(pass, "Un1tGroup");

            string ssl = ConfigurationManager.AppSettings["smtpEnableSsl"];
            EnableSsl = !String.IsNullOrEmpty(ssl) && Convert.ToBoolean(ssl);

            string save = ConfigurationManager.AppSettings["save2SentFolder"];
            Save2SentFolder = !String.IsNullOrEmpty(save) && Convert.ToBoolean(save);

            SentHost = ConfigurationManager.AppSettings["sentHost"];

            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["sentPort"]))
            {
                SentPort = Convert.ToInt32(ConfigurationManager.AppSettings["sentPort"]);
            }

            SentLogin = ConfigurationManager.AppSettings["sentLogin"];

            string sentPass = ConfigurationManager.AppSettings["sentPassword"];
            SentPassword = String.IsNullOrEmpty(sentPass) ? sentPass : Cryptor.Decrypt(sentPass, "Un1tGroup");

            string sentSsl = ConfigurationManager.AppSettings["sentEnableSsl"];
            SentEnableSsl = !String.IsNullOrEmpty(sentSsl) && Convert.ToBoolean(sentSsl);

            //SentMethod = ConfigurationManager.AppSettings["sentMethod"];

            MsExchangeVersion = ConfigurationManager.AppSettings["msExchVers"];
            MsExchangeLogin = ConfigurationManager.AppSettings["msExchLogin"];
            string msExchPass = ConfigurationManager.AppSettings["msExchPass"];
            MsExchangePass = String.IsNullOrEmpty(msExchPass) ? msExchPass : Cryptor.Decrypt(msExchPass, "Un1tGroup");

            MailCopyTo = ConfigurationManager.AppSettings["mailCopyTo"];
        }

       
    }
}
