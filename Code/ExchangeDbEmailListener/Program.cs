using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Exchange.WebServices.Data;

namespace ExchangeDbEmailListener
{
    class Program
    {
        static void Main(string[] args)
        {
            if (IsProcessOpen("SnmpScanner"))
            {
                throw new ApplicationException("Экземпляр программы уже запущен!");
                Environment.Exit(-1);
            }
            else
            {
                Work();    
            }
        }

        public static bool IsProcessOpen(string name)
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName.Contains(name))
                {
                    return true;
                }
            }
            return false;
        }


        private static void SendErrorNote(string message)
        {
            MailAddress mailTo = new MailAddress(ConfigurationManager.AppSettings["errorMail"]);

            SendMail("Ошибка в приложении ДСУ SNMP обмен", message, mailTo);
        }

        private static bool RedirectionUrlValidationCallback(string redirectionUrl)
        {
            // The default for the validation callback is to reject the URL.
            bool result = false;

            Uri redirectionUri = new Uri(redirectionUrl);

            // Validate the contents of the redirection URL. In this simple validation
            // callback, the redirection URL is considered valid if it is using HTTPS
            // to encrypt the authentication credentials. 
            if (redirectionUri.Scheme == "https")
            {
                result = true;
            }
            return result;
        }

        static ExchangeService service = new ExchangeService();

        /// <summary>
        /// Читаем письма в специальном ящике Exchenge Server, затем сохраняем в БД и удалеям псиьмо
        /// </summary>
        public static void Work()
        {
            //Место куда валятся письма с результатами сбора данных с КМТ
            string login = ConfigurationManager.AppSettings["login"];
            string pass = ConfigurationManager.AppSettings["pass"];
            string mail = ConfigurationManager.AppSettings["mail"];

            service.Credentials = new WebCredentials(mail, pass);

            service.UseDefaultCredentials = false;

            service.AutodiscoverUrl(mail, RedirectionUrlValidationCallback);

            //List<SearchFilter> searchFilterCollection = new List<SearchFilter>();
            //searchFilterCollection.Add(new SearchFilter.ContainsSubstring(ItemSchema.Subject, "test"));
            //searchFilterCollection.Add(new SearchFilter.ContainsSubstring(ItemSchema.Body, "Marketing"));
            //SearchFilter searchFilter = new SearchFilter.SearchFilterCollection(LogicalOperator.Or, searchFilterCollection.ToArray());

            ItemView view = new ItemView(1000000000);//Чтобы ничего не пропустить берем миллиард записей сразу ))

            FindItemsResults<Item> findResults = service.FindItems(WellKnownFolderName.Inbox, view);
            int totalCount = findResults.TotalCount;

            if (findResults.Count() > 0)
            {
                int stepCount = 10;
                int loadCount = 0;
                while (true)
                {
                    var items = findResults.Skip(loadCount).Take(stepCount);
                        service.LoadPropertiesForItems(items, PropertySet.FirstClassProperties);
                    //service.LoadPropertiesForItems(findResults, PropertySet.FirstClassProperties);

                    foreach (Item myItem in items)
                    {
                        try
                        {
                            //Сохраняем письмо в БД
                            string message = myItem.Body.Text;

                            string startTag = "<DeviceRequestRoot>";
                            string endTag = "</DeviceRequestRoot>";

                            //Обрезаем лишнее
                            if (message.Contains(startTag) && message.Contains(endTag))
                            {
                                message = message.Substring(message.IndexOf(startTag),
                                    message.IndexOf(endTag) + endTag.Count());
                            }

                            if (!message.Contains("Проверка отправки сообщения на сервер ГК ЮНИТ"))
                            {
                                Db.Db.Snmp.SaveExchangeItem(message);
                            }

                            //Удаление письма
                            //DeleteMode.SoftDelete - The item or folder will be moved to the dumpster. Items and folders in the dumpster can be recovered.

                        myItem.Delete(DeleteMode.SoftDelete);

                        }
                        catch (DbException ex)
                        {
                            SendErrorNote(ex.Message);
                            continue;
                        }
                    }
                    loadCount += stepCount;
                    if (loadCount> totalCount)break;
                }
            }

            //EmailMessage email = new EmailMessage(service);

            //email.ToRecipients.Add(login);

            //email.Subject = "HelloWorld";
            //email.Body = new MessageBody("This is the first email I've sent by using the EWS Managed API");

            //email.Send();
        }

        public static void SendMail(string subject, string body, MailAddress mailTo)
        {
            string user = ConfigurationManager.AppSettings["mail"];
            string pass = ConfigurationManager.AppSettings["pass"];
            MailAddress addressFrom = new MailAddress(ConfigurationManager.AppSettings["mailFrom"]);
            MailAddress addressTo = mailTo;
            MailMessage mail = new MailMessage(addressFrom, addressTo);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = false;

            SmtpClient client = new SmtpClient(user, Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"]));
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential(user, pass);
            client.EnableSsl = true;
            //if (!String.IsNullOrEmpty(settings.Login))
            //{
            //    client.Credentials = new NetworkCredential(settings.Login, settings.Password);
            //}

            client.Send(mail);
        }
    }
}
