namespace SnmpScanner
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TabPage tabImap;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.chkSentSsl = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtSentPort = new System.Windows.Forms.TextBox();
            this.lblSentPort = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtSentHost = new System.Windows.Forms.TextBox();
            this.txtSentLogin = new System.Windows.Forms.TextBox();
            this.txtSentPassword = new System.Windows.Forms.TextBox();
            this.lblImapHost = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lstIpRanges = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtIpFrom = new System.Windows.Forms.TextBox();
            this.pnlIpRanges = new System.Windows.Forms.GroupBox();
            this.btnIpRangeDelete = new System.Windows.Forms.Button();
            this.btnAddIpRange = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtIpTo = new System.Windows.Forms.TextBox();
            this.btnExchange = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblCheckTime = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.exchangeDelay = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabScan = new System.Windows.Forms.TabPage();
            this.lblLastExchange = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tabDevices = new System.Windows.Forms.TabPage();
            this.tabExchange = new System.Windows.Forms.TabPage();
            this.txtMailCopyTo = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbServerTypes = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tcMail = new System.Windows.Forms.TabControl();
            this.tabMsExchange = new System.Windows.Forms.TabPage();
            this.cmbMsExchangeServerVersions = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtMsExchLogin = new System.Windows.Forms.TextBox();
            this.txtMsExchPass = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.tabSmtp = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSmtpHost = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSmtpPort = new System.Windows.Forms.TextBox();
            this.chkSslEnable = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSmtpLogin = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSmtpPass = new System.Windows.Forms.TextBox();
            this.lblExchangeNote = new System.Windows.Forms.Label();
            this.btnSmtpTest = new System.Windows.Forms.Button();
            this.chkSave2Sent = new System.Windows.Forms.CheckBox();
            tabImap = new System.Windows.Forms.TabPage();
            tabImap.SuspendLayout();
            this.pnlIpRanges.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.exchangeDelay)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabScan.SuspendLayout();
            this.tabDevices.SuspendLayout();
            this.tabExchange.SuspendLayout();
            this.tcMail.SuspendLayout();
            this.tabMsExchange.SuspendLayout();
            this.tabSmtp.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabImap
            // 
            tabImap.Controls.Add(this.chkSentSsl);
            tabImap.Controls.Add(this.label14);
            tabImap.Controls.Add(this.txtSentPort);
            tabImap.Controls.Add(this.lblSentPort);
            tabImap.Controls.Add(this.label15);
            tabImap.Controls.Add(this.txtSentHost);
            tabImap.Controls.Add(this.txtSentLogin);
            tabImap.Controls.Add(this.txtSentPassword);
            tabImap.Controls.Add(this.lblImapHost);
            tabImap.Controls.Add(this.label16);
            tabImap.Location = new System.Drawing.Point(4, 22);
            tabImap.Name = "tabImap";
            tabImap.Padding = new System.Windows.Forms.Padding(3);
            tabImap.Size = new System.Drawing.Size(305, 134);
            tabImap.TabIndex = 1;
            tabImap.Text = "IMAP";
            tabImap.UseVisualStyleBackColor = true;
            // 
            // chkSentSsl
            // 
            this.chkSentSsl.AutoSize = true;
            this.chkSentSsl.Location = new System.Drawing.Point(113, 113);
            this.chkSentSsl.Name = "chkSentSsl";
            this.chkSentSsl.Size = new System.Drawing.Size(15, 14);
            this.chkSentSsl.TabIndex = 18;
            this.chkSentSsl.UseVisualStyleBackColor = true;
            this.chkSentSsl.CheckedChanged += new System.EventHandler(this.SaveSentSettings);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 114);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(79, 13);
            this.label14.TabIndex = 18;
            this.label14.Text = "Включить SSL";
            // 
            // txtSentPort
            // 
            this.txtSentPort.Location = new System.Drawing.Point(113, 35);
            this.txtSentPort.Name = "txtSentPort";
            this.txtSentPort.Size = new System.Drawing.Size(186, 20);
            this.txtSentPort.TabIndex = 15;
            this.txtSentPort.WordWrap = false;
            this.txtSentPort.TextChanged += new System.EventHandler(this.SaveSentSettings);
            // 
            // lblSentPort
            // 
            this.lblSentPort.AutoSize = true;
            this.lblSentPort.Location = new System.Drawing.Point(6, 38);
            this.lblSentPort.Name = "lblSentPort";
            this.lblSentPort.Size = new System.Drawing.Size(32, 13);
            this.lblSentPort.TabIndex = 16;
            this.lblSentPort.Text = "Порт";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 64);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(103, 13);
            this.label15.TabIndex = 13;
            this.label15.Text = "Имя пользователя";
            // 
            // txtSentHost
            // 
            this.txtSentHost.Location = new System.Drawing.Point(113, 9);
            this.txtSentHost.Name = "txtSentHost";
            this.txtSentHost.Size = new System.Drawing.Size(186, 20);
            this.txtSentHost.TabIndex = 14;
            this.txtSentHost.TextChanged += new System.EventHandler(this.SaveSentSettings);
            // 
            // txtSentLogin
            // 
            this.txtSentLogin.Location = new System.Drawing.Point(113, 61);
            this.txtSentLogin.Name = "txtSentLogin";
            this.txtSentLogin.Size = new System.Drawing.Size(186, 20);
            this.txtSentLogin.TabIndex = 16;
            this.txtSentLogin.TextChanged += new System.EventHandler(this.SaveSentSettings);
            // 
            // txtSentPassword
            // 
            this.txtSentPassword.Location = new System.Drawing.Point(113, 87);
            this.txtSentPassword.Name = "txtSentPassword";
            this.txtSentPassword.PasswordChar = '*';
            this.txtSentPassword.Size = new System.Drawing.Size(186, 20);
            this.txtSentPassword.TabIndex = 17;
            this.txtSentPassword.TextChanged += new System.EventHandler(this.SaveSentSettings);
            // 
            // lblImapHost
            // 
            this.lblImapHost.AutoSize = true;
            this.lblImapHost.Location = new System.Drawing.Point(6, 12);
            this.lblImapHost.Name = "lblImapHost";
            this.lblImapHost.Size = new System.Drawing.Size(74, 13);
            this.lblImapHost.TabIndex = 15;
            this.lblImapHost.Text = "Имя сервера";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 90);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(45, 13);
            this.label16.TabIndex = 15;
            this.label16.Text = "Пароль";
            // 
            // lstIpRanges
            // 
            this.lstIpRanges.FormattingEnabled = true;
            this.lstIpRanges.Location = new System.Drawing.Point(6, 45);
            this.lstIpRanges.Name = "lstIpRanges";
            this.lstIpRanges.Size = new System.Drawing.Size(214, 225);
            this.lstIpRanges.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(-1, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 13);
            this.label6.TabIndex = 13;
            // 
            // txtIpFrom
            // 
            this.txtIpFrom.Location = new System.Drawing.Point(6, 19);
            this.txtIpFrom.Name = "txtIpFrom";
            this.txtIpFrom.Size = new System.Drawing.Size(100, 20);
            this.txtIpFrom.TabIndex = 14;
            this.txtIpFrom.Text = "192.168.1.1";
            // 
            // pnlIpRanges
            // 
            this.pnlIpRanges.Controls.Add(this.btnIpRangeDelete);
            this.pnlIpRanges.Controls.Add(this.btnAddIpRange);
            this.pnlIpRanges.Controls.Add(this.label7);
            this.pnlIpRanges.Controls.Add(this.txtIpTo);
            this.pnlIpRanges.Controls.Add(this.txtIpFrom);
            this.pnlIpRanges.Controls.Add(this.lstIpRanges);
            this.pnlIpRanges.Location = new System.Drawing.Point(6, 6);
            this.pnlIpRanges.Name = "pnlIpRanges";
            this.pnlIpRanges.Size = new System.Drawing.Size(307, 271);
            this.pnlIpRanges.TabIndex = 15;
            this.pnlIpRanges.TabStop = false;
            this.pnlIpRanges.Text = "ip диапазон";
            // 
            // btnIpRangeDelete
            // 
            this.btnIpRangeDelete.Location = new System.Drawing.Point(226, 44);
            this.btnIpRangeDelete.Name = "btnIpRangeDelete";
            this.btnIpRangeDelete.Size = new System.Drawing.Size(75, 23);
            this.btnIpRangeDelete.TabIndex = 18;
            this.btnIpRangeDelete.Text = "Удалить";
            this.btnIpRangeDelete.UseVisualStyleBackColor = true;
            this.btnIpRangeDelete.Click += new System.EventHandler(this.btnIpRangeDelete_Click);
            // 
            // btnAddIpRange
            // 
            this.btnAddIpRange.Location = new System.Drawing.Point(226, 18);
            this.btnAddIpRange.Name = "btnAddIpRange";
            this.btnAddIpRange.Size = new System.Drawing.Size(75, 23);
            this.btnAddIpRange.TabIndex = 17;
            this.btnAddIpRange.Text = "Добавить";
            this.btnAddIpRange.UseVisualStyleBackColor = true;
            this.btnAddIpRange.Click += new System.EventHandler(this.btnAddIpRange_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(108, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(10, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "-";
            // 
            // txtIpTo
            // 
            this.txtIpTo.Location = new System.Drawing.Point(120, 19);
            this.txtIpTo.Name = "txtIpTo";
            this.txtIpTo.Size = new System.Drawing.Size(100, 20);
            this.txtIpTo.TabIndex = 15;
            this.txtIpTo.Text = "192.168.1.50";
            // 
            // btnExchange
            // 
            this.btnExchange.Location = new System.Drawing.Point(218, 255);
            this.btnExchange.Name = "btnExchange";
            this.btnExchange.Size = new System.Drawing.Size(94, 22);
            this.btnExchange.TabIndex = 16;
            this.btnExchange.Text = "Сейчас";
            this.btnExchange.UseVisualStyleBackColor = true;
            this.btnExchange.Click += new System.EventHandler(this.btnExchange_Click);
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnStart.Location = new System.Drawing.Point(6, 6);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(310, 74);
            this.btnStart.TabIndex = 18;
            this.btnStart.Text = "Ручной запуск";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnStop.Location = new System.Drawing.Point(6, 184);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(310, 93);
            this.btnStop.TabIndex = 19;
            this.btnStop.Text = "Остановить";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lblCheckTime
            // 
            this.lblCheckTime.AutoSize = true;
            this.lblCheckTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCheckTime.Location = new System.Drawing.Point(157, 100);
            this.lblCheckTime.Name = "lblCheckTime";
            this.lblCheckTime.Size = new System.Drawing.Size(0, 18);
            this.lblCheckTime.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 260);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Синхронизация каждые";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // exchangeDelay
            // 
            this.exchangeDelay.Location = new System.Drawing.Point(141, 258);
            this.exchangeDelay.Maximum = new decimal(new int[] {
            72,
            0,
            0,
            0});
            this.exchangeDelay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.exchangeDelay.Name = "exchangeDelay";
            this.exchangeDelay.Size = new System.Drawing.Size(41, 20);
            this.exchangeDelay.TabIndex = 23;
            this.exchangeDelay.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.exchangeDelay.ValueChanged += new System.EventHandler(this.exchangeDelay_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(188, 260);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "час.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(109, 255);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 13);
            this.label4.TabIndex = 25;
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabScan);
            this.tabControl1.Controls.Add(this.tabDevices);
            this.tabControl1.Controls.Add(this.tabExchange);
            this.tabControl1.Location = new System.Drawing.Point(2, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(327, 309);
            this.tabControl1.TabIndex = 26;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabScan
            // 
            this.tabScan.Controls.Add(this.lblLastExchange);
            this.tabScan.Controls.Add(this.label12);
            this.tabScan.Controls.Add(this.label10);
            this.tabScan.Controls.Add(this.btnStart);
            this.tabScan.Controls.Add(this.btnStop);
            this.tabScan.Controls.Add(this.lblCheckTime);
            this.tabScan.Location = new System.Drawing.Point(4, 22);
            this.tabScan.Name = "tabScan";
            this.tabScan.Padding = new System.Windows.Forms.Padding(3);
            this.tabScan.Size = new System.Drawing.Size(319, 283);
            this.tabScan.TabIndex = 0;
            this.tabScan.Text = "Сканирование";
            this.tabScan.UseVisualStyleBackColor = true;
            // 
            // lblLastExchange
            // 
            this.lblLastExchange.AutoSize = true;
            this.lblLastExchange.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblLastExchange.Location = new System.Drawing.Point(157, 140);
            this.lblLastExchange.Name = "lblLastExchange";
            this.lblLastExchange.Size = new System.Drawing.Size(0, 18);
            this.lblLastExchange.TabIndex = 25;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(16, 140);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(135, 18);
            this.label12.TabIndex = 24;
            this.label12.Text = "Последний обмен";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(37, 100);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(114, 18);
            this.label10.TabIndex = 22;
            this.label10.Text = "Последнй скан";
            // 
            // tabDevices
            // 
            this.tabDevices.Controls.Add(this.pnlIpRanges);
            this.tabDevices.Location = new System.Drawing.Point(4, 22);
            this.tabDevices.Name = "tabDevices";
            this.tabDevices.Padding = new System.Windows.Forms.Padding(3);
            this.tabDevices.Size = new System.Drawing.Size(319, 283);
            this.tabDevices.TabIndex = 1;
            this.tabDevices.Text = "Аппараты";
            this.tabDevices.UseVisualStyleBackColor = true;
            // 
            // tabExchange
            // 
            this.tabExchange.Controls.Add(this.txtMailCopyTo);
            this.tabExchange.Controls.Add(this.label11);
            this.tabExchange.Controls.Add(this.cmbServerTypes);
            this.tabExchange.Controls.Add(this.btnCancel);
            this.tabExchange.Controls.Add(this.btnSave);
            this.tabExchange.Controls.Add(this.tcMail);
            this.tabExchange.Controls.Add(this.lblExchangeNote);
            this.tabExchange.Controls.Add(this.label4);
            this.tabExchange.Controls.Add(this.label1);
            this.tabExchange.Controls.Add(this.btnSmtpTest);
            this.tabExchange.Controls.Add(this.chkSave2Sent);
            this.tabExchange.Controls.Add(this.label3);
            this.tabExchange.Controls.Add(this.btnExchange);
            this.tabExchange.Controls.Add(this.exchangeDelay);
            this.tabExchange.Location = new System.Drawing.Point(4, 22);
            this.tabExchange.Name = "tabExchange";
            this.tabExchange.Padding = new System.Windows.Forms.Padding(3);
            this.tabExchange.Size = new System.Drawing.Size(319, 283);
            this.tabExchange.TabIndex = 2;
            this.tabExchange.Text = "Отправка почты";
            this.tabExchange.UseVisualStyleBackColor = true;
            this.tabExchange.Click += new System.EventHandler(this.tabExchange_Click);
            // 
            // txtMailCopyTo
            // 
            this.txtMailCopyTo.Location = new System.Drawing.Point(120, 196);
            this.txtMailCopyTo.Name = "txtMailCopyTo";
            this.txtMailCopyTo.Size = new System.Drawing.Size(192, 20);
            this.txtMailCopyTo.TabIndex = 32;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 199);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(79, 13);
            this.label11.TabIndex = 31;
            this.label11.Text = "Копия письма";
            // 
            // cmbServerTypes
            // 
            this.cmbServerTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServerTypes.FormattingEnabled = true;
            this.cmbServerTypes.Location = new System.Drawing.Point(3, 6);
            this.cmbServerTypes.Name = "cmbServerTypes";
            this.cmbServerTypes.Size = new System.Drawing.Size(145, 21);
            this.cmbServerTypes.TabIndex = 30;
            this.cmbServerTypes.SelectedIndexChanged += new System.EventHandler(this.cmbServerTypes_SelectedIndexChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(218, 223);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 23);
            this.btnCancel.TabIndex = 29;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(122, 223);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 23);
            this.btnSave.TabIndex = 28;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tcMail
            // 
            this.tcMail.Controls.Add(this.tabMsExchange);
            this.tcMail.Controls.Add(this.tabSmtp);
            this.tcMail.Controls.Add(tabImap);
            this.tcMail.Location = new System.Drawing.Point(3, 29);
            this.tcMail.Name = "tcMail";
            this.tcMail.SelectedIndex = 0;
            this.tcMail.Size = new System.Drawing.Size(313, 160);
            this.tcMail.TabIndex = 27;
            this.tcMail.SelectedIndexChanged += new System.EventHandler(this.tcMail_SelectedIndexChanged);
            // 
            // tabMsExchange
            // 
            this.tabMsExchange.Controls.Add(this.cmbMsExchangeServerVersions);
            this.tabMsExchange.Controls.Add(this.label18);
            this.tabMsExchange.Controls.Add(this.txtMsExchLogin);
            this.tabMsExchange.Controls.Add(this.txtMsExchPass);
            this.tabMsExchange.Controls.Add(this.label19);
            this.tabMsExchange.Controls.Add(this.label20);
            this.tabMsExchange.Location = new System.Drawing.Point(4, 22);
            this.tabMsExchange.Name = "tabMsExchange";
            this.tabMsExchange.Padding = new System.Windows.Forms.Padding(3);
            this.tabMsExchange.Size = new System.Drawing.Size(305, 134);
            this.tabMsExchange.TabIndex = 2;
            this.tabMsExchange.Text = "MS Exchange";
            this.tabMsExchange.UseVisualStyleBackColor = true;
            // 
            // cmbMsExchangeServerVersions
            // 
            this.cmbMsExchangeServerVersions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMsExchangeServerVersions.FormattingEnabled = true;
            this.cmbMsExchangeServerVersions.Location = new System.Drawing.Point(113, 8);
            this.cmbMsExchangeServerVersions.Name = "cmbMsExchangeServerVersions";
            this.cmbMsExchangeServerVersions.Size = new System.Drawing.Size(186, 21);
            this.cmbMsExchangeServerVersions.TabIndex = 29;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 38);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(103, 13);
            this.label18.TabIndex = 19;
            this.label18.Text = "Имя пользователя";
            // 
            // txtMsExchLogin
            // 
            this.txtMsExchLogin.Location = new System.Drawing.Point(113, 35);
            this.txtMsExchLogin.Name = "txtMsExchLogin";
            this.txtMsExchLogin.Size = new System.Drawing.Size(186, 20);
            this.txtMsExchLogin.TabIndex = 25;
            // 
            // txtMsExchPass
            // 
            this.txtMsExchPass.Location = new System.Drawing.Point(113, 61);
            this.txtMsExchPass.Name = "txtMsExchPass";
            this.txtMsExchPass.PasswordChar = '*';
            this.txtMsExchPass.Size = new System.Drawing.Size(186, 20);
            this.txtMsExchPass.TabIndex = 26;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 13);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(44, 13);
            this.label19.TabIndex = 22;
            this.label19.Text = "Версия";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(6, 64);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(45, 13);
            this.label20.TabIndex = 23;
            this.label20.Text = "Пароль";
            // 
            // tabSmtp
            // 
            this.tabSmtp.Controls.Add(this.label2);
            this.tabSmtp.Controls.Add(this.txtSmtpHost);
            this.tabSmtp.Controls.Add(this.label5);
            this.tabSmtp.Controls.Add(this.txtSmtpPort);
            this.tabSmtp.Controls.Add(this.chkSslEnable);
            this.tabSmtp.Controls.Add(this.label13);
            this.tabSmtp.Controls.Add(this.label8);
            this.tabSmtp.Controls.Add(this.txtSmtpLogin);
            this.tabSmtp.Controls.Add(this.label9);
            this.tabSmtp.Controls.Add(this.txtSmtpPass);
            this.tabSmtp.Location = new System.Drawing.Point(4, 22);
            this.tabSmtp.Name = "tabSmtp";
            this.tabSmtp.Padding = new System.Windows.Forms.Padding(3);
            this.tabSmtp.Size = new System.Drawing.Size(305, 134);
            this.tabSmtp.TabIndex = 0;
            this.tabSmtp.Text = "SMTP";
            this.tabSmtp.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Имя сервера";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // txtSmtpHost
            // 
            this.txtSmtpHost.Location = new System.Drawing.Point(113, 9);
            this.txtSmtpHost.Name = "txtSmtpHost";
            this.txtSmtpHost.Size = new System.Drawing.Size(186, 20);
            this.txtSmtpHost.TabIndex = 1;
            this.txtSmtpHost.TextChanged += new System.EventHandler(this.SaveSmtpSettings);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Порт";
            // 
            // txtSmtpPort
            // 
            this.txtSmtpPort.Location = new System.Drawing.Point(113, 35);
            this.txtSmtpPort.Name = "txtSmtpPort";
            this.txtSmtpPort.Size = new System.Drawing.Size(186, 20);
            this.txtSmtpPort.TabIndex = 3;
            this.txtSmtpPort.TextChanged += new System.EventHandler(this.SaveSmtpSettings);
            // 
            // chkSslEnable
            // 
            this.chkSslEnable.AutoSize = true;
            this.chkSslEnable.Location = new System.Drawing.Point(113, 113);
            this.chkSslEnable.Name = "chkSslEnable";
            this.chkSslEnable.Size = new System.Drawing.Size(15, 14);
            this.chkSslEnable.TabIndex = 11;
            this.chkSslEnable.UseVisualStyleBackColor = true;
            this.chkSslEnable.CheckedChanged += new System.EventHandler(this.SaveSmtpSettings);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 114);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(79, 13);
            this.label13.TabIndex = 12;
            this.label13.Text = "Включить SSL";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 64);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Имя пользователя";
            // 
            // txtSmtpLogin
            // 
            this.txtSmtpLogin.Location = new System.Drawing.Point(113, 61);
            this.txtSmtpLogin.Name = "txtSmtpLogin";
            this.txtSmtpLogin.Size = new System.Drawing.Size(186, 20);
            this.txtSmtpLogin.TabIndex = 5;
            this.txtSmtpLogin.TextChanged += new System.EventHandler(this.SaveSmtpSettings);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 90);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "Пароль";
            // 
            // txtSmtpPass
            // 
            this.txtSmtpPass.Location = new System.Drawing.Point(113, 87);
            this.txtSmtpPass.Name = "txtSmtpPass";
            this.txtSmtpPass.PasswordChar = '*';
            this.txtSmtpPass.Size = new System.Drawing.Size(186, 20);
            this.txtSmtpPass.TabIndex = 7;
            this.txtSmtpPass.TextChanged += new System.EventHandler(this.SaveSmtpSettings);
            // 
            // lblExchangeNote
            // 
            this.lblExchangeNote.AutoSize = true;
            this.lblExchangeNote.Location = new System.Drawing.Point(154, 250);
            this.lblExchangeNote.Name = "lblExchangeNote";
            this.lblExchangeNote.Size = new System.Drawing.Size(0, 13);
            this.lblExchangeNote.TabIndex = 26;
            // 
            // btnSmtpTest
            // 
            this.btnSmtpTest.Location = new System.Drawing.Point(3, 223);
            this.btnSmtpTest.Name = "btnSmtpTest";
            this.btnSmtpTest.Size = new System.Drawing.Size(113, 23);
            this.btnSmtpTest.TabIndex = 10;
            this.btnSmtpTest.Text = "Тестовое письмо";
            this.btnSmtpTest.UseVisualStyleBackColor = true;
            this.btnSmtpTest.Click += new System.EventHandler(this.btnSmtpTest_Click);
            // 
            // chkSave2Sent
            // 
            this.chkSave2Sent.AutoSize = true;
            this.chkSave2Sent.Location = new System.Drawing.Point(154, 8);
            this.chkSave2Sent.Name = "chkSave2Sent";
            this.chkSave2Sent.Size = new System.Drawing.Size(162, 17);
            this.chkSave2Sent.TabIndex = 13;
            this.chkSave2Sent.Text = "сохранять в отправленных";
            this.chkSave2Sent.UseVisualStyleBackColor = true;
            this.chkSave2Sent.CheckedChanged += new System.EventHandler(this.chkSave2Sent_CheckedChanged);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(331, 314);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label6);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1000, 1000);
            this.MinimumSize = new System.Drawing.Size(347, 316);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UN1T Счетчик";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            tabImap.ResumeLayout(false);
            tabImap.PerformLayout();
            this.pnlIpRanges.ResumeLayout(false);
            this.pnlIpRanges.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.exchangeDelay)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabScan.ResumeLayout(false);
            this.tabScan.PerformLayout();
            this.tabDevices.ResumeLayout(false);
            this.tabExchange.ResumeLayout(false);
            this.tabExchange.PerformLayout();
            this.tcMail.ResumeLayout(false);
            this.tabMsExchange.ResumeLayout(false);
            this.tabMsExchange.PerformLayout();
            this.tabSmtp.ResumeLayout(false);
            this.tabSmtp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstIpRanges;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtIpFrom;
        private System.Windows.Forms.GroupBox pnlIpRanges;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtIpTo;
        private System.Windows.Forms.Button btnAddIpRange;
        private System.Windows.Forms.Button btnIpRangeDelete;
        private System.Windows.Forms.Button btnExchange;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblCheckTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown exchangeDelay;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabScan;
        private System.Windows.Forms.TabPage tabDevices;
        private System.Windows.Forms.TabPage tabExchange;
        private System.Windows.Forms.TextBox txtSmtpPass;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtSmtpLogin;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSmtpPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSmtpHost;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSmtpTest;
        private System.Windows.Forms.Label lblExchangeNote;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblLastExchange;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox chkSslEnable;
        private System.Windows.Forms.Label lblImapHost;
        private System.Windows.Forms.TextBox txtSentHost;
        private System.Windows.Forms.CheckBox chkSave2Sent;
        private System.Windows.Forms.TextBox txtSentPort;
        private System.Windows.Forms.Label lblSentPort;
        private System.Windows.Forms.TabControl tcMail;
        private System.Windows.Forms.TabPage tabSmtp;
        private System.Windows.Forms.CheckBox chkSentSsl;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtSentLogin;
        private System.Windows.Forms.TextBox txtSentPassword;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabPage tabMsExchange;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtMsExchLogin;
        private System.Windows.Forms.TextBox txtMsExchPass;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox cmbMsExchangeServerVersions;
        private System.Windows.Forms.ComboBox cmbServerTypes;
        private System.Windows.Forms.TextBox txtMailCopyTo;
        private System.Windows.Forms.Label label11;
    }
}

