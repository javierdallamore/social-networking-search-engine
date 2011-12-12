using System.Collections.Generic;
using Core.Domain;
using SearchEnginesBase.Entities;

namespace BusinessRules
{
    public interface ISearchEngineManager
    {
        List<Post> Search(string searchParameters, List<string> searchEnginesName);
    }
}