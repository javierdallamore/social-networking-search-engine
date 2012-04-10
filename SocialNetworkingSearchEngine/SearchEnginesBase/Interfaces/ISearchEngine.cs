using System.Collections.Generic;
using SearchEnginesBase.Entities;

namespace SearchEnginesBase.Interfaces
{
    public interface ISearchEngine
    {
        string Name { get;}
        SocialNetworkingSearchResult Search(string searchParameters, int page);
        List<string> CountriesToFilterISOCodes { get; set; }
    }
}
