using System.Collections.Generic;
using SearchEnginesBase.Entities;
using SearchEnginesBase.Interfaces;

namespace SearchEngineMock
{
    public class SearchEngine : ISearchEngine
    {
        public string Name
        {
            get { return "Local DB"; }
        }

        public SocialNetworkingSearchResult Search(string searchParameters, int page)
        {
            var socialNetworkingSearchResult = new SocialNetworkingSearchResult() { };

            NHSessionManager.Instance.BeginTransaction();

            var list = new DAO.DaoSocialNetworkingItem().GetAll();

            socialNetworkingSearchResult.SocialNetworkingItems.AddRange(list);
            NHSessionManager.Instance.CommitTransaction();

            return socialNetworkingSearchResult;
        }

        public List<string> CountriesToFilterISOCodes
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }
    }
}
