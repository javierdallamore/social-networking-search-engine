using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SearchEnginesBase.Utils;
using SearchEnginesBase.Entities;

namespace GooglePlusSearchEngine
{
    public class GooglePlusSearchEngine : SearchEnginesBase.Interfaces.ISearchEngine
    {
        const string _lenguaje = "es-419";
        const string _apiKey = "AIzaSyAoWGAxAZ8LkAX8B5FG0wVBGBBUg6GXgVo";

        public string Name
        {
            get { return "Google +"; }
        }

        public SocialNetworkingSearchResult Search(string searchParameters, int page)
        {
            var engineURL = GetEngineUrl();
            var parameters = GetParameters(_lenguaje, 0, string.Empty, string.Empty, 0, _apiKey);
            var jsonResults = Utils.BuildSearchQuery(engineURL, searchParameters, parameters);
            var entity = Utils.DeserializarJsonTo<SearchResultsGooglePlus>(jsonResults);            
            var list = SocialNetworkingItemList(entity);

            return new SocialNetworkingSearchResult() { SocialNetworkingItems = list, SocialNetworkingName = Name };
        }

        public List<string> CountriesToFilterISOCodes
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        private string GetEngineUrl()
        {
            return "https://www.googleapis.com/plus/v1/activities?query=";
        }

        /// <summary>
        /// Metodo que arma los parametros para la consulta a la API REST de Google Plus.
        /// </summary>
        /// <param name="lenguaje">Especifique el codigo del idioma de preferencia para la búsqueda. Para Español, Latino America el codigo es [es-419].</param>
        /// <param name="maximosResultados">Cantidad maxima de resultados por Response, sirve para paginar (valor entre 1 y 20).</param>
        /// <param name="ordenarPor">Especifica como ordenar los resultados de la busqueda. Los valores pueden ser "best" o "recent".</param>
        /// <param name="pageToken">Para obtener la siguiente pagina de resultados. El valor es "nextPageToken".</param>
        /// <param name="pp">No se que es, pero siempre es igual a 1</param>
        /// <param name="apiKey">Clave de la aplicacion para poder usar Google Plus</param>
        /// <returns></returns>
        private string GetParameters(string lenguaje, int maximosResultados, string ordenarPor, string pageToken, int pp, string apiKey)
        {
            var _language = String.IsNullOrEmpty(lenguaje) ? String.Empty : "&language=" + lenguaje;
            var _maxResults = maximosResultados == 0 ? String.Empty : "&maxResults=" + maximosResultados;
            var _orderBy = String.IsNullOrEmpty(ordenarPor) ? String.Empty : "&orderBy=" + ordenarPor;
            var _pageToken = String.IsNullOrEmpty(pageToken) ? String.Empty : "&pageToken=" + pageToken;
            var _pp = pp == 0 ? "1" : "&pp=" + pp;
            var _key = "&key=" + apiKey;

            return _language + _maxResults + _orderBy + _pageToken + _pp + _key;
        }

        //Este metodo itera los resultados y crea las entidades de dominio
        private List<SocialNetworkingItem> SocialNetworkingItemList(SearchResultsGooglePlus entity)
        {
            if (entity == null)
                return null;

            List<SocialNetworkingItem> users = (from u in entity.Results
                                                select new SocialNetworkingItem
                                                {
                                                    SocialNetworkName = Name,
                                                    UserName = u.Actor.FromUser,
                                                    ProfileImage = u.Actor.Imagen.ProfileImageUrl,
                                                    Content = u.Objeto.Text,
                                                    UrlPost = u.Objeto.UrlPost,
                                                    UrlProfile = u.Actor.UserProfile,
                                                    CreatedAt = DateTimeOffset.Parse(u.CreatedAt, System.Globalization.CultureInfo.GetCultureInfo("en-US")).UtcDateTime,
                                                    Source = string.Empty
                                                }).ToList();
            return users;
        }
    }
}
