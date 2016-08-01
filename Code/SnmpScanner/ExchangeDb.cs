using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Exchange.WebServices.Data;
using S22.Imap;
using SnmpScanner.Models;

namespace SnmpScanner
{
    public class ExchangeDb
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

        public static void SendMail(string subject, string body, EmailSettings settings)
        {
            //switch (settings.Method)
            //{
            //        case MailMethod.SMTP:
            //        SendMailSmtp(subject, body, settings);
            //        break;
            //        case MailMethod.IMAP:
            //        break;
            //}

            //IMail email = Mail.Text(body).To(settings.MailTo.ToString()).From(settings.EnableSsl ? settings.Login : settings.MailFrom.ToString()).Subject(subject).Create();

            if (settings.ServerType == "Другой")//Если сервер Другой то шлем по SMTP
            {
                SendMailSmtp(subject, body, settings);
                try
                {
                    if (settings.Save2SentFolder)
                    {
                        SaveMail2SentFolder(subject, body, settings);
                    }
                }
                catch
                {
                    //throw new Exception(String.Format("Письмо было отправлено, но не удалось сохранить письмо в папку Отправленные.\r\nПричина: {0}", ex.Message));
                }
                return;
            }
            else if (settings.ServerType == "MS Exchange")//Если это Exchange
            {
                SendMailExchange2007Plus(subject, body, settings);

                return;
            }

            throw new Exception("Не указан тип сервера! Письмо не было отправлено");
        }

        private static void SendMailExchange2007Plus(string subject, string body, EmailSettings settings)
        {
            ExchangeService _service;

            //Мычаем выбранную версию Exchange
            if (!string.IsNullOrEmpty(settings.MsExchangeVersion))
            {
                try
                {
                    ExchangeVersion ver;
                    ExchangeVersion.TryParse(settings.MsExchangeVersion, out ver);
                    _service = new ExchangeService(ver);
                }
                catch(Exception ex)
                {
                    Log.Write(ex.Message);
                    _service = new ExchangeService();
                }
            }
            else
            {
                _service = new ExchangeService();    
            }
            
            if (!String.IsNullOrEmpty(settings.MsExchangeLogin))
            {
                string login = settings.MsExchangeLogin.Remove(settings.MsExchangeLogin.IndexOf('@'));

                _service.Credentials = new WebCredentials(login, settings.MsExchangePass);
            }

            _service.AutodiscoverUrl(settings.MsExchangeLogin);

            EmailMessage mail = new EmailMessage(_service);
            
            mail.ToRecipients.Add(settings.MailTo.ToString());

            _service.HttpHeaders.Remove("");
            //Шлем копию письма если надо
            if (!String.IsNullOrEmpty(settings.MailCopyTo))
            {
                mail.CcRecipients.Add(settings.MailCopyTo);
            }
            else
            {
                mail.CcRecipients.Clear();
            }

            mail.Subject = subject;

            //Подготавливаем тело письма так как может не отображаться если клиент не узнает эти тэги
            //body = String.Format(@"<plaintext>{0}</plaintext>", body);

            mail.Body = new MessageBody(BodyType.Text, body);

            if (settings.Save2SentFolder)
            {
                mail.SendAndSaveCopy();
            }
            else
            {
                mail.Send();
            }
        }

        public static void SaveMail2SentFolder(string subject, string body, EmailSettings settings)
        {
            using (ImapClient imap = new ImapClient(settings.SentHost, settings.SentPort, settings.SentLogin, settings.SentPassword, AuthMethod.Auto, settings.SentEnableSsl))
            {
                MailMessage message = new MailMessage();

                if (!String.IsNullOrEmpty(settings.SentLogin))
                {
                    message.From = new MailAddress(settings.SentLogin);
                }
                else
                {
                    message.From = settings.MailFrom;
                }

                message.To.Add(settings.MailTo);

                message.Subject = subject;
                message.Body = body;

                string mailbox = null;

                foreach (var mb in imap.ListMailboxes())
                {
                    if (mb.ToLower().Equals("sent") || mb.ToLower().Equals("отправленные") || mb.ToLower().Contains("sent"))
                    {
                        mailbox = mb;
                        break;
                    }
                }

                uint uid = imap.StoreMessage(message, false, mailbox);
                //imap.SetMessageFlags(uid, null, );
            }
        }

        public static void SendMailSmtp(string subject, string body, EmailSettings settings)
        {
            MailMessage mail = new MailMessage();

            SmtpClient client = new SmtpClient();
            client.Port = settings.Port;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            if (!String.IsNullOrEmpty(settings.Login))
            {
                client.Credentials = new NetworkCredential(settings.Login, settings.Password);
                mail.From = new MailAddress(settings.Login);
            }
            else
            {
                mail.From = settings.MailFrom;
            }

            client.EnableSsl = settings.EnableSsl;

            mail.To.Add(settings.MailTo);

            //Шлем копию письма если надо
            if (!String.IsNullOrEmpty(settings.MailCopyTo))
            {
                mail.CC.Add(new MailAddress(settings.MailCopyTo));
            }
            else
            {
                mail.CC.Clear();
            }

            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = false;

            client.Host = settings.Host;

            client.Send(mail);
        }

        public bool DoExchangeDb(bool exchangeNow = false)
        {
            bool result = false;
            bool canDelete = false;

            try
            {
                SendEmailExchangeDb();
                canDelete = true;
                result = true;
            }
            catch (Exception ex)
            {
                Log.Write(ex.Message);
                canDelete = false;
                result = false;
                //exchangeSuccess = false;
            }

            if (canDelete)
            {
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

                    Request.DeleteAll();
                }
                finally
                {
                    lock (gate)
                    {
                        _dbIsBuzy = false;
                    }
                }
            }

            //if (!_background)
            //{
            //this.Invoke(new Action<string>((s) => lblLastExchange.Text = s), DateTime.Now.ToString("g"));
            //}

            if (!exchangeNow)
            {
                if (!_threadMustStop) Thread.Sleep(Program.GetExchangeDelayValue() * 60 * 60 * 1000);
            }

            return result;
        }

        private void SendEmailExchangeDb()
        {
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
                Request[] lst = Request.GetList();

                StringBuilder content = new StringBuilder();

                //content.Append(@"<?xml version=""1.0"" encoding=""UTF-8"" ?>");

                //////XmlDocument document = new XmlDocument();

                //////XmlNode deviceRequestRoot = document.CreateElement("DeviceRequestRoot");
                //////document.DocumentElement.AppendChild(deviceRequestRoot);

                ////////SysInfo
                //////XmlNode sysInfo = document.CreateElement("SysInfo");
                //////deviceRequestRoot.AppendChild(sysInfo);

                //////XmlAttribute idContractor = document.CreateAttribute("idContractor");
                //////idContractor.Value = AppSettings.ContractorId.ToString();
                //////sysInfo.Attributes.Append(idContractor);
                //////XmlAttribute dateSend = document.CreateAttribute("dateSend");
                //////dateSend.Value = DateTime.Now.ToString("1:dd-MM-yyyy HH:mm:ss zzz");
                //////sysInfo.Attributes.Append(dateSend);
                //////XmlAttribute progVersion = document.CreateAttribute("progVersion");
                //////idContractor.Value = Program.progVersion;
                //////sysInfo.Attributes.Append(progVersion);
                ////////>SysInfo

                //////XmlNode deviceRequestList = document.CreateElement("DeviceRequestList");
                //////deviceRequestRoot.AppendChild(deviceRequestList);

                //////foreach (Request request in lst)
                //////{
                //////    XmlNode deviceRequest = document.CreateElement("DeviceRequest");
                //////    deviceRequestList.AppendChild(deviceRequest);

                //////    XmlAttribute serialNum = document.CreateAttribute("serialNum");
                //////    serialNum.Value = request.SerialNum.Replace("\"", "");
                //////    deviceRequest.Attributes.Append(serialNum);

                //////    XmlAttribute date = document.CreateAttribute("date");
                //////    date.Value = request.DateRequest.ToString();
                //////    deviceRequest.Attributes.Append(date);

                //////    XmlAttribute host = document.CreateAttribute("host");
                //////    host.Value = request.Host;
                //////    deviceRequest.Attributes.Append(host);

                //////    XmlAttribute oid = document.CreateAttribute("oid");
                //////    oid.Value = request.Oid;
                //////    deviceRequest.Attributes.Append(oid);

                //////    XmlAttribute value = document.CreateAttribute("value");
                //////    value.Value = request.Value.Replace("\"", "");
                //////    deviceRequest.Attributes.Append(value);

                //////    XmlAttribute oidType = document.CreateAttribute("oidType");
                //////    oidType.Value = request.OidType;
                //////    deviceRequest.Attributes.Append(oidType);
                //////}


                content.Append("<DeviceRequestRoot>");

                content.Append(String.Format(@"<SysInfo idContractor=""{0}"" dateSend=""{1:dd-MM-yyyy HH:mm:ss zzz}"" progVersion=""{2}"" />",
                    AppSettings.ContractorId, DateTime.Now, Program.progVersion));

                content.Append("<DeviceRequestList>");
                foreach (Request request in lst)
                {
                    content.Append(
                        String.Format(
                            @"<DeviceRequest serialNum=""{5}"" date=""{0:dd-MM-yyyy hh:mm:ss}"" host=""{1}"" oid=""{2}"" value=""{3}"" oidType=""{4}"" />",
                            request.DateRequest, request.Host, request.Oid, request.Value.Replace("\"", ""), request.OidType, request.SerialNum.Replace("\"", "")));
                }
                content.Append("</DeviceRequestList>");
                content.Append("</DeviceRequestRoot>");


                var emailSettings = new EmailSettings(true);
                //SendMail(AppSettings.MailSubject, document.ToString(), emailSettings);
                SendMail(AppSettings.MailSubject, content.ToString(), emailSettings);
            }
            finally
            {
                lock (gate)
                {
                    _dbIsBuzy = false;
                }
            }

            //string path = "d:\\devPool.txt";
            //if (File.Exists(path))
            //{
            //    File.Delete(path);
            //}

            //using (FileStream fs = File.Create(path))
            //{
            //    Byte[] info = new UTF8Encoding(true).GetBytes(content.ToString());
            //    fs.Write(info, 0, info.Length);
            //}
        }
    }
}
