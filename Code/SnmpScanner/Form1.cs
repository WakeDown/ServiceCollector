using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Configuration.Install;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Exchange.WebServices.Data;
using SnmpScanner.Models;

namespace SnmpScanner
{
    public partial class Form1 : Form
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

        private NotifyIcon TrayIcon;
        private ContextMenuStrip TrayIconContextMenu;
        private ToolStripMenuItem CloseMenuItem;
        //private bool _background;

        public Form1()
        {


            Process[] localByName = Process.GetProcessesByName("UN1TCounter");

            if (localByName.Length > 1)
            {
                ////throw new Exception("Экземпляр программы UN1T Счетчик уже запущен!");
                MessageBox.Show("Экземпляр программы UN1T Счетчик уже запущен!");
                Application.Exit();
                Environment.Exit(-1);
            }

            ////<Запуск из командной строки
            //if (background)
            //{
            //    _background = background;
            //    AppSettings = new Settings();
            //    StartWork();
            //    this.Hide();
            //    return;
            //}//<\запуск

            InitializeComponent();

            this.Text += String.Format("    v{0}",Program.progVersion);

            try
            {
                InitializeTrayIcon();
                TrayIcon.Visible = true;

                //AppSettings = new Settings();
                FillIpRangeList();
                if (lstIpRanges.Items.Count > 0) lstIpRanges.SelectedIndex = 0;
                FillDefaults();
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
                MessageBox.Show("Некорректный файл лицензии!");
                Application.Exit();
                Environment.Exit(-1);
                //throw new ApplicationException("Некорректный файл с настройками!");
            }
        }

        private void InitializeTrayIcon()
        {
            TrayIcon = new NotifyIcon();

            TrayIcon.BalloonTipIcon = ToolTipIcon.Info;
            //TrayIcon.BalloonTipText = "I noticed that you double-clicked me! What can I do for you?";
            //TrayIcon.BalloonTipTitle = "You called Master?";
            TrayIcon.Text = "UN1T Счетчик";


            TrayIcon.Icon = Properties.Resources.TrayIcon;

            TrayIcon.DoubleClick += TrayIcon_DoubleClick;

            TrayIconContextMenu = new ContextMenuStrip();
            CloseMenuItem = new ToolStripMenuItem();
            TrayIconContextMenu.SuspendLayout();

            // 
            // TrayIconContextMenu
            // 
            this.TrayIconContextMenu.Items.AddRange(new ToolStripItem[] {
            this.CloseMenuItem});
            this.TrayIconContextMenu.Name = "TrayIconContextMenu";
            this.TrayIconContextMenu.Size = new Size(153, 70);
            // 
            // CloseMenuItem
            // 
            this.CloseMenuItem.Name = "CloseMenuItem";
            this.CloseMenuItem.Size = new Size(152, 22);
            this.CloseMenuItem.Text = "Закрыть программу";
            this.CloseMenuItem.Click += new EventHandler(this.CloseMenuItem_Click);

            TrayIconContextMenu.ResumeLayout(false);
            TrayIcon.ContextMenuStrip = TrayIconContextMenu;
        }

        private void TrayIcon_DoubleClick(object sender, EventArgs e)
        {
            //TrayIcon.ShowBalloonTip(10000);
            this.Show();
            WindowState = FormWindowState.Normal;
        }

        private void CloseMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены что хотите закрыть программу UN1T Счетчик?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Process[] prcs = Process.GetProcessesByName("SnmpScanner");

                foreach (var process in prcs)
                {
                    process.Close();
                }

                TrayIcon.Icon = null;

                Application.Exit();
                Environment.Exit(-1);
            }
        }

        

        private void FillDefaults()
        {
            exchangeDelay.Minimum = AppSettings.MinExchangeDelay;
            exchangeDelay.Maximum = AppSettings.MaxExchangeDelay;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //if (chkLocal.Checked)
            //{


            //    return;
            //}
            
            var settings = new EmailSettings(true);

            DialogResult result;

            if (ValidateSmtpSettings(settings))
            {
                result = DialogResult.Yes;
            }
            else
            {
                result = MessageBox.Show(
                    "Настройки обмена не позволяют обмениваться с центральной базой. Вы хотите продолжить?", "Уведомление",
                    MessageBoxButtons.YesNo);
            }

            if (result == DialogResult.Yes)
            {
                FormDisable();
                StartWork();
            }
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
                this.Invoke(new Action<string>((s) => lblLastExchange.Text = s), DateTime.Now.ToString("g"));

                new ExchangeDb().DoExchangeDb();

            }
        }

        private void SrartScanner()
        {
            while (!_threadMustStop)
            {
                this.Invoke(new Action<string>((s) => lblCheckTime.Text = s), DateTime.Now.ToString("g"));

                new Scanner().SnmpGet();
                //SnmpGet();

            }
        }

        public delegate void RefreshDelegate();

        

        

        private void FillIpRangeList()
        {
            lstIpRanges.Items.Clear();

            IpRange[] lst = IpRange.GetList();

            lstIpRanges.DisplayMember = "Name";

            foreach (IpRange range in lst)
            {
                lstIpRanges.Items.Add(range);

                //string item = String.Format("{0} - {1}", range.IpFrom, range.IpTo);
                //lstIpRanges.Items.Add(item);
            }
        }

        private void IpRangeSave()
        {
            string ipFrom = txtIpFrom.Text;
            string ipTo = txtIpTo.Text;

            IpRange ipRange = new IpRange(ipFrom, ipTo);
            ipRange.Save();

            txtIpFrom.Text = String.Empty;
            txtIpTo.Text = String.Empty;
        }

        private void btnAddIpRange_Click(object sender, EventArgs e)
        {
            IpRangeSave();
            FillIpRangeList();
        }

        private void btnExchange_Click(object sender, EventArgs e)
        {
            exchangeSuccess = false;

            try
            {
                exchangeSuccess = new ExchangeDb().DoExchangeDb(true);
            }
            catch (Exception exception)
            {
                exchangeSuccess = false;
            }

            string note = String.Empty;

            if (exchangeSuccess)
            {
                note = "Обмен прошел успешно!";
            }
            else
            {
                note = "Ошибка обмена!";
            }

            MessageBox.Show(note);
            //lblExchangeNote.Text = note;
        }

        private bool exchangeSuccess;

        

        private void btnStop_Click(object sender, EventArgs e)
        {
            FormDisable(false);
            lock (gate)
            {
                _threadMustStop = true;
            }
        }

        private void FormDisable(bool disable = true)
        {
            btnStart.Enabled = /*btnGetSettings.Enabled =*/ tabDevices.Enabled = tabExchange.Enabled = !disable;
            btnStop.Enabled = disable;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void btnSmtpSave_Click(object sender, EventArgs e)
        {
            //SaveSmtpSettings();
            //DisplayExchangeTabElements();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private EmailSettings currSettings = new EmailSettings(false);

        public void FillCurrSettings()
        {
            currSettings.Host = txtSmtpHost.Text;

            if (!String.IsNullOrEmpty(txtSmtpPort.Text))
            {
                currSettings.Port = Convert.ToInt32(txtSmtpPort.Text);
            }

            currSettings.Login = txtSmtpLogin.Text;
            currSettings.Password = txtSmtpPass.Text;
            string ssl = chkSslEnable.Checked.ToString();
            currSettings.EnableSsl = !String.IsNullOrEmpty(ssl) && Convert.ToBoolean(ssl);

            string save = chkSave2Sent.Checked.ToString();
            currSettings.Save2SentFolder = !String.IsNullOrEmpty(save) && Convert.ToBoolean(save);

            currSettings.SentHost = txtSentHost.Text;

            if (!String.IsNullOrEmpty(txtSentPort.Text))
            {
                currSettings.SentPort = Convert.ToInt32(txtSentPort.Text);
            }

            currSettings.SentLogin = txtSentLogin.Text;
            currSettings.SentPassword = txtSentPassword.Text;
            string sentSsl = chkSentSsl.Checked.ToString();
            currSettings.SentEnableSsl = !String.IsNullOrEmpty(sentSsl) && Convert.ToBoolean(sentSsl);

            currSettings.ServerType = cmbServerTypes.SelectedItem.ToString();

            currSettings.MsExchangeVersion = cmbMsExchangeServerVersions.SelectedItem == null ? String.Empty :cmbMsExchangeServerVersions.SelectedItem.ToString();
            currSettings.MsExchangeLogin = txtMsExchLogin.Text;
            currSettings.MsExchangePass = txtMsExchPass.Text;

            currSettings.MailCopyTo = txtMailCopyTo.Text;
        }

        private void SaveSmtpSettings(object sender, EventArgs e)
        {
            currSettings.Host = txtSmtpHost.Text;

            if (!String.IsNullOrEmpty(txtSmtpPort.Text))
            {
                currSettings.Port = Convert.ToInt32(txtSmtpPort.Text);
            }

            currSettings.Login = txtSmtpLogin.Text;
            currSettings.Password = txtSmtpPass.Text;
            string ssl = chkSslEnable.Checked.ToString();
            currSettings.EnableSsl = !String.IsNullOrEmpty(ssl) && Convert.ToBoolean(ssl);

            string save = chkSave2Sent.Checked.ToString();
            currSettings.Save2SentFolder = !String.IsNullOrEmpty(save) && Convert.ToBoolean(save);

            currSettings.ServerType = cmbServerTypes.SelectedItem.ToString();

            currSettings.MsExchangeVersion = cmbMsExchangeServerVersions.SelectedItem == null ? String.Empty : cmbMsExchangeServerVersions.SelectedItem.ToString();
            currSettings.MsExchangeLogin = txtMsExchLogin.Text;
            currSettings.MsExchangePass = txtMsExchPass.Text;

            currSettings.MailCopyTo = txtMailCopyTo.Text;
        }

        private void SaveSentSettings(object sender, EventArgs e)
        {
            currSettings.SentHost = txtSentHost.Text;

            if (!String.IsNullOrEmpty(txtSentPort.Text))
            {
                currSettings.SentPort = Convert.ToInt32(txtSentPort.Text);
            }

            currSettings.SentLogin = txtSentLogin.Text;
            currSettings.SentPassword = txtSentPassword.Text;
            string sentSsl = chkSentSsl.Checked.ToString();
            currSettings.SentEnableSsl = !String.IsNullOrEmpty(sentSsl) && Convert.ToBoolean(sentSsl);

            currSettings.ServerType = cmbServerTypes.SelectedItem.ToString();

            currSettings.MsExchangeVersion = cmbMsExchangeServerVersions.SelectedItem == null ? String.Empty : cmbMsExchangeServerVersions.SelectedItem.ToString();
            currSettings.MsExchangeLogin = txtMsExchLogin.Text;
            currSettings.MsExchangePass = txtMsExchPass.Text;

            currSettings.MailCopyTo = txtMailCopyTo.Text;
        }

        private void SaveSettings2Config()
        {
            SetAppConfigValue("serverType", cmbServerTypes.SelectedItem.ToString());

            SetAppConfigValue("smtpHost", txtSmtpHost.Text);
            SetAppConfigValue("smtpPort", txtSmtpPort.Text);
            SetAppConfigValue("smtpLogin", txtSmtpLogin.Text);

            string pass = txtSmtpPass.Text;
            SetAppConfigValue("smtpPassword", String.IsNullOrEmpty(pass) ? pass : Cryptor.Encrypt(pass, "Un1tGroup"));

            SetAppConfigValue("smtpEnableSsl", chkSslEnable.Checked.ToString());
            SetAppConfigValue("save2SentFolder", chkSave2Sent.Checked.ToString());
            SetAppConfigValue("sentHost", txtSentHost.Text);
            SetAppConfigValue("sentPort", txtSentPort.Text);
            SetAppConfigValue("sentLogin", txtSentLogin.Text);

            string sentPass = txtSentPassword.Text;
            SetAppConfigValue("sentPassword", String.IsNullOrEmpty(sentPass) ? sentPass : Cryptor.Encrypt(sentPass, "Un1tGroup"));

            SetAppConfigValue("sentEnableSsl", chkSentSsl.Checked.ToString());

            //MS Exchange
            SetAppConfigValue("msExchVers", cmbMsExchangeServerVersions.SelectedItem == null ? String.Empty : cmbMsExchangeServerVersions.SelectedItem.ToString());
            SetAppConfigValue("msExchLogin", txtMsExchLogin.Text);
            string msExchPass = txtMsExchPass.Text;
            SetAppConfigValue("msExchPass", String.IsNullOrEmpty(msExchPass) ? msExchPass : Cryptor.Encrypt(msExchPass, "Un1tGroup"));

            SetAppConfigValue("mailCopyTo", txtMailCopyTo.Text);
        }

        

        private bool ValidateSmtpSettings(EmailSettings settings = null)
        {
            bool valid = false;
            try
            {
            EmailSettings emailSettings = settings ?? new EmailSettings(true);
            ExchangeDb.SendMail("Тестовое сообщение", "Проверка отправки сообщения на сервер ГК ЮНИТ", emailSettings);//isTest

            valid = true;
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message);
                valid = false;
            }
            return valid;
        }

        private void SetAppConfigValue(string key, string value)
        {
            //XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            //foreach (XmlElement element in xmlDoc.DocumentElement.GetElementsByTagName("appSettings"))
            //{
            //    if (element.Name.Equals("appSettings"))
            //    {
            //        foreach (XmlNode node in element.ChildNodes)
            //        {
            //            if (node.Attributes[0].Value.Equals(key))
            //            {
            //                node.Attributes[1].Value = value;
            //            }
            //        }
            //    }
            //}

            //xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            //ConfigurationManager.RefreshSection("appSettings");

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Remove(key);
            config.AppSettings.Settings.Add(key, value);

            config.Save(ConfigurationSaveMode.Modified, true);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void FillSmtpSettings()
        {
            EmailSettings settings = new EmailSettings(true);

            cmbServerTypes.SelectedItem = settings.ServerType;

            txtSmtpHost.Text = settings.Host;//ConfigurationManager.AppSettings["smtpHost"];
            txtSmtpPort.Text = settings.Port.ToString();//ConfigurationManager.AppSettings["smtpPort"];
            txtSmtpLogin.Text = settings.Login;//ConfigurationManager.AppSettings["smtpLogin"];
            txtSmtpPass.Text = settings.Password;//ConfigurationManager.AppSettings["smtpPassword"];
            //string ssl = ConfigurationManager.AppSettings["smtpEnableSsl"];
            chkSslEnable.Checked = settings.EnableSsl;//!String.IsNullOrEmpty(ssl) && Convert.ToBoolean(ssl);
            //string method = ConfigurationManager.AppSettings["smtpMailMethod"];

            //string save = ConfigurationManager.AppSettings["save2SentFolder"];
            chkSave2Sent.Checked = settings.Save2SentFolder;//!String.IsNullOrEmpty(save) && Convert.ToBoolean(save);

            txtSentHost.Text = settings.SentHost;//ConfigurationManager.AppSettings["sentHost"];
            txtSentPort.Text = settings.SentPort.ToString();//ConfigurationManager.AppSettings["sentPort"];
            txtSentLogin.Text = settings.SentLogin;//ConfigurationManager.AppSettings["sentLogin"];
            txtSentPassword.Text = settings.SentPassword;//ConfigurationManager.AppSettings["sentPassword"];
            //string sentSsl = ConfigurationManager.AppSettings["sentEnableSsl"];
            chkSentSsl.Checked = settings.SentEnableSsl; //!String.IsNullOrEmpty(sentSsl) && Convert.ToBoolean(sentSsl);

            //if (String.IsNullOrEmpty(method))
            //{
            //    cmbMailMethod.SelectedIndex = -1;
            //}
            //else
            //{
            //    try
            //    {
            //        cmbMailMethod.SelectedIndex = cmbMailMethod.Items.IndexOf(method);
            //    }
            //    catch (Exception ex)
            //    {
            //        cmbMailMethod.SelectedIndex = -1;
            //    }
            //}


            //MS Exchange
            cmbMsExchangeServerVersions.SelectedItem = settings.MsExchangeVersion;
            txtMsExchLogin.Text = settings.MsExchangeLogin;
            txtMsExchPass.Text = settings.MsExchangePass;

            txtMailCopyTo.Text = settings.MailCopyTo;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabExchange)
            {
               
                FillLists();

                FillSmtpSettings();
                exchangeDelay.Value = Program.GetExchangeDelayValue();
                DisplayTabImapPop3();

                //DisplayExchangeTabElements();
            }
        }

        private void FillLists()
        {
            //Тип сервера
            FillServerTypes(cmbServerTypes);

            //Заполняем версии MS Exchange
            cmbMsExchangeServerVersions.DataSource = Enum.GetNames(typeof (ExchangeVersion));
        }

        private void FillServerTypes(ComboBox cmb)
        {
            List<string> ds = new List<string>();
            ds.Add("--тип сервера--");
            ds.Add("MS Exchange");
            ds.Add("Другой");

            cmb.DataSource = ds;
        }

        private void DisplayExchangeTabElements()
        {
            var settings = new EmailSettings(true);//GetFormSettings();
            btnExchange.Enabled = ValidateSmtpSettings(settings);
        }

        private void btnSmtpTest_Click(object sender, EventArgs e)
        {
            try
            {
                FillCurrSettings();

                var settings = currSettings;//GetFormSettings();

                if (ValidateSmtpSettings(settings))
                {
                    MessageBox.Show("Письмо успешно отправлено!");
                    SaveSettings2Config();
                    //SaveSmtpSettings(null, null);
                    //SaveSentSettings(null, null);
                }
                else
                {
                    MessageBox.Show("Не удается отправить письмо с данными настройками почтового сервера!");
                }

                //DisplayExchangeTabElements();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Некорректные настройки!");
            }
        }

        //private EmailSettings GetFormSettings()
        //{
        //    var settings = new EmailSettings(false);
        //    settings.Host = txtSmtpHost.Text;
        //    if (!String.IsNullOrEmpty(txtSmtpPort.Text)) settings.Port = Convert.ToInt32(txtSmtpPort.Text);
        //    settings.Login = txtSmtpLogin.Text;
        //    settings.Password = txtSmtpPass.Text;

        //    string ssl = chkSslEnable.Checked.ToString();
        //    settings.EnableSsl = !String.IsNullOrEmpty(ssl) && Convert.ToBoolean(ssl);

        //    //MailMethod method;
        //    //MailMethod.TryParse(cmbMailMethod.SelectedText, out method);
        //    //settings.Method = method;

        //    string save = chkSave2Sent.Checked.ToString();
        //    settings.Save2SentFolder = !String.IsNullOrEmpty(save) && Convert.ToBoolean(save);

        //    settings.ImapHost = txtSentHost.Text;
        //    if (!String.IsNullOrEmpty(txtSentPort.Text)) settings.ImapPort = Convert.ToInt32(txtSentPort.Text);

        //    return settings;
        //}

        private void btnSmtpCancel_Click(object sender, EventArgs e)
        {
            FillSmtpSettings();
        }

        private void exchangeDelay_ValueChanged(object sender, EventArgs e)
        {
            SetAppConfigValue("exchangeDelay", exchangeDelay.Value.ToString());
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            WindowState = FormWindowState.Minimized;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                //TrayIcon.Visible = true;
                //TrayIcon.ShowBalloonTip(500);
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                this.Show();
                //TrayIcon.Visible = false;
            }
        }

        private void btnIpRangeDelete_Click(object sender, EventArgs e)
        {
            var selIpRange = lstIpRanges.SelectedItem as IpRange;
            IpRange.Delete(selIpRange.Id);

            FillIpRangeList();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //foreach (MailMethod mm in Enum.GetValues(typeof(MailMethod)))
            //{
            //    cmbMailMethod.Items.Add(mm.ToString());
            //}
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkSave2Sent_CheckedChanged(object sender, EventArgs e)
        {
            SaveSmtpSettings(sender, e);
            DisplayTabImapPop3();
            //lblImapHost.Visible = txtSentHost.Visible = lblSentPort.Visible = txtSentPort.Visible = save;
        }

        private void DisplayTabImapPop3()
        {
            bool save = chkSave2Sent.Checked;
            tcMail.TabPages["tabImap"].Enabled = save;
            //tcMail.TabPages["tabMsExchange"].Enabled = save;
        }

        private void tabExchange_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveSettings2Config();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FillSmtpSettings();
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            //ManagedInstallerClass.InstallHelper(new string[] { Assembly.GetExecutingAssembly().Location });
            MessageBox.Show(SelfInstaller.InstallMe().ToString());
            //MessageBox.Show(ServiceInstaller.InstallService().ToString());
        }

        private void btnUninstall_Click(object sender, EventArgs e)
        {
            //ManagedInstallerClass.InstallHelper(new string[] { "/u", Assembly.GetExecutingAssembly().Location });
            MessageBox.Show(SelfInstaller.UninstallMe().ToString());
            //MessageBox.Show(ServiceInstaller.UninstallService().ToString());
        }

        private void tcMail_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cmbServerTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ds.Add("--тип сервера--");
            //ds.Add("MS Exchange");
            //ds.Add("Другой");

            //Дективируем все вкладки
            foreach (TabPage tabPage in tcMail.TabPages)
                    {
                        tabPage.Enabled = false;
                    }
            bool displayBtns = false;

            if (cmbServerTypes.SelectedItem == null)
            {
                cmbServerTypes.SelectedItem = "--тип сервера--";
            }

            switch (cmbServerTypes.SelectedItem.ToString())
            {
                case "--тип сервера--":
                    displayBtns = false;
                    break;
                case "MS Exchange":
                    tcMail.TabPages["tabMsExchange"].Enabled = true;
                    displayBtns = true;
                    break;
                case "Другой":
                   tcMail.TabPages["tabSmtp"].Enabled = tcMail.TabPages["tabImap"].Enabled = true;
                   displayBtns = true;
                    break;
            }


            chkSave2Sent.Enabled = btnSmtpTest.Enabled = btnSave.Enabled = btnCancel.Enabled = btnExchange.Enabled = exchangeDelay.Enabled = txtMailCopyTo .Enabled= displayBtns;
        }
    }
}
