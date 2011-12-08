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
            var jsonResults = Utils.BuildSearchQuery(searchParameters, engineURL, page);
            var entity = Utils.DeserializarJsonTo<SearchResultsTwitter>(jsonResults);
            var list = SocialNetworkingItemList(entity);
            return new SocialNetworkingSearchResult() { SocialNetworkingItems = list, SocialNetworkingName = "Twitter using 'Twitter search engine'", UrlIcon = GetUrlIcon() };
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
                                          CreatedAt = DateTimeOffset.Parse(u.CreatedAt).UtcDateTime,
                                          Source = u.Source
                                      }).ToList();
            return users;
        }

        private string GetEngineUrl()
        {
            return System.Configuration.ConfigurationManager.AppSettings["TwitterSearchEngine"];
        }

        private string GetUrlIcon()
        {
            return System.Configuration.ConfigurationManager.AppSettings["UrlIcon"];
        }
    }
}
