using System;
using System.IO;
using System.Net.Mail;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Net;

namespace SearchEnginesBase.Utils
{
    public static class Utils
    {
        /// <summary>
        /// Este metodo es un generico que deserealiza un JSON en un objeto T
        /// </summary>
        /// <typeparam name="T">Objeto destino</typeparam>
        /// <param name="jsonSerializado">JSON serializado</param>
        /// <returns></returns>
        public static T DeserializarJsonTo<T>(string jsonSerializado)
        {
            try
            {
                T obj = Activator.CreateInstance<T>();
                var ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonSerializado));
                var serializer = new DataContractJsonSerializer(obj.GetType());
                obj = (T)serializer.ReadObject(ms);

                ms.Close();
                ms.Dispose();

                return obj;
            }
            catch
            {
                return default(T);
            }
        }

        //Este metodo arma la consulta y devuelve un string con el JSON resultante de ejecutar la misma
        public static string BuildSearchQuery(string url, string query, string parameters)
        {
            var getUrl = WebRequest.Create(url + query + parameters);
            var objStream = getUrl.GetResponse().GetResponseStream();
            var objReader = new StreamReader(objStream);

            var json = string.Empty;

            while (!objReader.EndOfStream)
            {
                json += objReader.ReadLine();
            }

            return json;
        }

        public static void SendMail(string to, string address, string displayName, string subject, string body, string userName, string password, int port, string host)
        {
            var msg = new MailMessage();

            msg.To.Add(to);
            msg.From = new MailAddress(address, displayName, Encoding.UTF8);
            msg.Subject = subject;
            msg.SubjectEncoding = Encoding.UTF8;
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
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}
