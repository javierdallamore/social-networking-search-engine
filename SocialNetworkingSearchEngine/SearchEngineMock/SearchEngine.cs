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

        public SocialNetworkingSearchResult Search(string searchParameters, int page)
        {
            var socialNetworkingSearchResult = new SocialNetworkingSearchResult()
                                                   {
                                                       SocialNetworkingName = "P&P social networking"
                                                   };
            NHSessionManager.Instance.BeginTransaction();
            var daoSocialNetworkingItem = new DAO.DaoSocialNetworkingItem();
            socialNetworkingSearchResult.SocialNetworkingItems.AddRange(daoSocialNetworkingItem.GetAll());
            NHSessionManager.Instance.CommitTransaction();

            return socialNetworkingSearchResult;
        }
    }
}
