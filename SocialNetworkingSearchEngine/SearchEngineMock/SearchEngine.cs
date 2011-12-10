using SearchEnginesBase.Entities;
using SearchEnginesBase.Interfaces;

namespace SearchEngineMock
{
    public class SearchEngine : ISearchEngine
    {
        public string Name
        {
            get { return "P&P social networking"; }
        }

        public SocialNetworkingSearchResult Search(string searchParameters, int page)
        {
            var socialNetworkingSearchResult = new SocialNetworkingSearchResult()
                                                   {
                                                       SocialNetworkingName = "P&P social networking"
                                                   };

            NHSessionManager.Instance.BeginTransaction();
                        
            var list = new DAO.DaoSocialNetworkingItem().GetAll();

            socialNetworkingSearchResult.SocialNetworkingItems.AddRange(list);
            NHSessionManager.Instance.CommitTransaction();

            return socialNetworkingSearchResult;
        }
    }
}
