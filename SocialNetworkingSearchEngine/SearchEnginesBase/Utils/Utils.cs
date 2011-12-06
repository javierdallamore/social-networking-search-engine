using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Net;

namespace SearchEnginesBase.Utils
{
    public static class Utils
    {
        private static string _page;

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
        public static string BuildSearchQuery(string query, string url, int page)
        {
            _page = page == 1 ?string.Empty : "&page=" + page;
            var getUrl = WebRequest.Create(url + query + _page);
            var objStream = getUrl.GetResponse().GetResponseStream();
            var objReader = new StreamReader(objStream);

            var json = string.Empty;

            while (!objReader.EndOfStream)
            {
                json += objReader.ReadLine();
            }

            return json;
        }
    }
}
