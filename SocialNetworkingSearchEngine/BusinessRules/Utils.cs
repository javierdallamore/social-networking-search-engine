using System.Net;
using System.Net.Mail;
using System.Text;

namespace BusinessRules
{
    public class Utils
    {
        public static void SendMail(string to, string address, string displayName, string subject, string body, string userName, string password, int port, string host)
        {
            var msg = new MailMessage();

            msg.To.Add(to);
            msg.From = new MailAddress(address, displayName, Encoding.UTF8);
            msg.Subject = subject;
            msg.SubjectEncoding = Encoding.UTF8;
            msg.IsBodyHtml = true;
            msg.Body = body;            
            msg.BodyEncoding = Encoding.UTF8;
            msg.IsBodyHtml = false;

            var client = new SmtpClient();

            client.Credentials = new NetworkCredential(userName, password);
            client.Port = port;
            client.Host = host;
            client.EnableSsl = true;

            try
            {
                client.Send(msg);
            }
            catch (SmtpException ex)
            {
            }
        }
    }
}
