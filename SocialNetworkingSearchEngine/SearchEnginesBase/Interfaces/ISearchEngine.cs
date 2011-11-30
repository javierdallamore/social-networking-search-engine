using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SearchEnginesBase.Entities;

namespace SearchEnginesBase.Interfaces
{
    public interface ISearchEngine
    {
        string Name { get;}
        SocialNetworkingSearchResult Search(string searchParameters);
    }
}
