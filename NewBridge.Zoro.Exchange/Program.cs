using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace NewBridge.Zoro.Exchange
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                MailMessage msg = new MailMessage(ConfigurationManager.AppSettings["From"], ConfigurationManager.AppSettings["To"]);
                msg.Subject = "易泰电影列表，日期" + System.DateTime.Now.ToShortDateString();
                Attachment attachment = new Attachment(ConfigurationManager.AppSettings["Attachment"], MediaTypeNames.Application.Octet);
                msg.Attachments.Add(attachment);

                SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["SmtpServer"], Int32.Parse(ConfigurationManager.AppSettings["SmtpPort"]));
                client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["UserName"], ConfigurationManager.AppSettings["Password"]);
                client.EnableSsl = bool.Parse(ConfigurationManager.AppSettings["EnableSsl"]);

                client.Send(msg);
                Console.WriteLine("发送成功！{0}", msg.Subject);
            }
            catch (Exception ex)
            {
                Console.WriteLine("发送失败！{0}", ex.Message);
            }
            Console.ReadLine();
        }
    }
}
