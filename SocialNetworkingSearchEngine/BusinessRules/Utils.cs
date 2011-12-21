using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using Core.Domain;

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
    
        public static string GenerateBodyHtml(Post post)
        {
            var title = "|*TITLE*|";
            var urlImgSocialNetwork = "|*URL_IMG_SOCIAL_NETWORK*|";
            var urlPost = "|*URL_POST*|";
            var date = "|*DATE*|";
            var content = "|*CONTENT*|";
            var urlImgUserProfile = "|*URL_IMG_USER_PROFILE*|";
            var urlProfile = "|*URL_PROFILE*|";
            var userName = "|*USER_NAME*|";
            // Read the file as one string.
            var path = HttpContext.Current.Server.MapPath("../Content/template.html");
            var templateFile = new StreamReader(path);
            var bodyHtml = templateFile.ReadToEnd();
            
            templateFile.Close();

            bodyHtml = bodyHtml.Replace(title, "TITULO");
            bodyHtml = bodyHtml.Replace(urlImgSocialNetwork, post.SocialNetworkName);
            bodyHtml = bodyHtml.Replace(urlPost, post.UrlPost);
            bodyHtml = bodyHtml.Replace(date, post.CreatedAtShort);
            bodyHtml = bodyHtml.Replace(urlImgUserProfile, post.ProfileImage);
            bodyHtml = bodyHtml.Replace(urlProfile, post.UrlProfile);
            bodyHtml = bodyHtml.Replace(userName, post.UserName);
            bodyHtml = bodyHtml.Replace(content, post.Content);

            return bodyHtml;
        }
    }
}
