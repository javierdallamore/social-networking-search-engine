using System;
using System.Collections.Generic;
using System.Linq;
using SearchEnginesBase.Entities;
using SearchEnginesBase.Utils;

namespace TwitterSearchEngine
{
    public class TwitterSearchEngine : SearchEnginesBase.Interfaces.ISearchEngine
    {
        public string Name
        {
            get { return "Twitter search engine"; }
        }

        public SocialNetworkingSearchResult Search(string searchParameters, int page)
        {
            var engineURL = GetEngineUrl();
            var parameters = GetParameters(page, 100);
            var jsonResults = Utils.BuildSearchQuery(engineURL, searchParameters, parameters);
            var entity = Utils.DeserializarJsonTo<SearchResultsTwitter>(jsonResults);
            var list = SocialNetworkingItemList(entity);
            return new SocialNetworkingSearchResult() { SocialNetworkingItems = list, SocialNetworkingName = "Twitter using 'Twitter search engine'" };
        }

        //Este metodo itera los resultados y crea las entidades de dominio
        private List<SocialNetworkingItem> SocialNetworkingItemList(SearchResultsTwitter entity)
        {
            List<SocialNetworkingItem> users = (from u in entity.Results
                                                select new SocialNetworkingItem
                                      {
                                          UserName = u.FromUser,
                                          ProfileImage = u.ProfileImageUrl,
                                          Content = u.Text,
                                          StatusDate = DateTimeOffset.Parse(u.CreatedAt).UtcDateTime,
                                          UrlPost = GetPostUrl(u.FromUser, u.IdString),
                                          UrlProfile = GetProfileUrl(u.FromUser)
                                      }).ToList();
            return users;
        }

        private string GetEngineUrl()
        {
            return "http://search.twitter.com/search.json?q=";
        }

        private string GetPostUrl(string fromUser, string idString)
        {
            return "http://twitter.com/#!/" + fromUser + "/statuses/" + idString;
        }

        private string GetProfileUrl(string fromUser)
        {
            return "http://twitter.com/#!/" + fromUser;
        }

        private string GetParameters(int page, int rpp)
        {            
            var _page = page == 1 ? string.Empty : "&page=" + page;
            var _rpp = "&rpp=" + rpp;

            return _page + _rpp;
        }
    }
}
