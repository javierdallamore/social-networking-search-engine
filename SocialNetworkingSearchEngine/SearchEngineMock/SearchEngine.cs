using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SearchEnginesBase.Entities;
using SearchEnginesBase.Interfaces;

namespace SearchEngineMock
{
    public class SearchEngine : ISearchEngine
    {
        public string Name
        {
            get { return "SearchEngineMock"; }
        }

        public SocialNetworkingSearchResult Search(string searchParameters)
        {
            var socialNetworkingSearchResult = new SocialNetworkingSearchResult()
                                                   {
                                                       SocialNetworkingName = "P&P social networking"
                                                   };

            var socialNetworkingItem = new SocialNetworkingItem();
            socialNetworkingSearchResult.SocialNetworkingItems.Add(socialNetworkingItem);

            var socialNetworkingItem2 = new SocialNetworkingItem();
            socialNetworkingSearchResult.SocialNetworkingItems.Add(socialNetworkingItem2);

            return socialNetworkingSearchResult;
        }
    }
}
