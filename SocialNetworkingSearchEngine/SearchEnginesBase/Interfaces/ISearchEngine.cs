using System.Collections.Generic;
using SearchEnginesBase.Entities;

namespace SearchEnginesBase.Interfaces
{
    public interface ISearchEngine
    {
        string Name { get;}
        SocialNetworkingSearchResult Search(string searchParameters, int page);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchParameters"></param>
        /// <param name="page"></param>
        /// <param name="location">Codigo ISO 3166-1 del pais</param>
        /// <param name="language">Codigo ISO 639-1 del idioma</param>
        /// <returns></returns>
        SocialNetworkingSearchResult Search(string searchParameters, int page, string location, string language = null);
        List<string> CountriesToFilterISOCodes { get; set; }
    }
}
