using System.Collections.Generic;
using Core.Domain;

namespace BusinessRules
{
    public interface ISearchEngineManager
    {
        List<Post> Search(string searchParameters, List<string> searchEnginesName);
    }
}