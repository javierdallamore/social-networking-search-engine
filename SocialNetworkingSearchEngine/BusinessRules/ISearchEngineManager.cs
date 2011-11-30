using System.Collections.Generic;
using SearchEnginesBase.Entities;

namespace BusinessRules
{
    public interface ISearchEngineManager
    {
        List<SocialNetworkingSearchResult> Search(string searchParameters, List<string> searchEnginesName);
    }
}