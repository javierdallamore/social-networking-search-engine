using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using SearchEnginesBase.Entities;
using SearchEnginesBase.Utils;

namespace TwitterSearchEngine
{
    public class TwitterSearchEngine : SearchEnginesBase.Interfaces.ISearchEngine
    {
        private StringBuilder _queryParams;

        public string Name
        {
            get { return "Twitter"; }
        }

        public SocialNetworkingSearchResult Search(string searchParameters, int page)
        {
            try
            {
                var engineURL = GetEngineUrl();
                _queryParams.Append(GetPageParameter(page, 100));
                var jsonResults = Utils.BuildSearchQuery(engineURL, searchParameters, _queryParams.ToString());
                var entity = Utils.DeserializarJsonTo<SearchResultsTwitter>(jsonResults);
                var list = SocialNetworkingItemList(entity);

                return new SocialNetworkingSearchResult() { SocialNetworkingItems = list, SocialNetworkingName = Name };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public SocialNetworkingSearchResult Search(string searchParameters, int page, string location, string language = null)
        {
            _queryParams = new StringBuilder();
            _queryParams.Append(GetLocationParameter(location));
            if (!string.IsNullOrEmpty(language)) _queryParams.Append(GetLangParameter(language));

            return Search(searchParameters, page);
        }

        public List<string> CountriesToFilterISOCodes
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        //Este metodo itera los resultados y crea las entidades de dominio
        private List<SocialNetworkingItem> SocialNetworkingItemList(SearchResultsTwitter entity)
        {
            if (entity == null)
                return null;

            List<SocialNetworkingItem> users = (from u in entity.Results
                                                select new SocialNetworkingItem
                                                           {
                                                               SocialNetworkName = Name,
                                                               UserName = u.FromUser,
                                                               ProfileImage = u.ProfileImageUrl,
                                                               Content = u.Text,
                                                               UrlPost = GetPostUrl(u.FromUser, u.IdString),
                                                               UrlProfile = GetProfileUrl(u.FromUser),
                                                               CreatedAt = DateTimeOffset.Parse(u.CreatedAt, System.Globalization.CultureInfo.GetCultureInfo("en-US")).UtcDateTime,
                                                               Source = u.Source
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

        private string GetPageParameter(int page, int rpp)
        {
            var _page = page == 1 ? string.Empty : "&page=" + page;
            var _rpp = "&rpp=" + rpp;

            return _page + _rpp;
        }

        private string GetLangParameter(string lang)
        {
            if (string.IsNullOrWhiteSpace(lang) || lang.Length > 2)
            {
                throw new ArgumentException("lang no es un ISO 639-1 valido");
            }
            return "&lang=" + lang;
        }

        private string GetLocationParameter(string location)
        {
            if (location == null || location.Length != 2)
            {
                throw new ArgumentException("lang no es un ISO 3166-1 valido");
            }
            location = GetGeoLocationByISOCountryName(location);
            if (!string.IsNullOrEmpty(location))
                return "&geocode=" + location;

            return string.Empty;
        }

        private string GetGeoLocationByISOCountryName(string isoCountryName)
        {
            if (isoCountryName == null || isoCountryName.Length != 2)
            {
                throw new ArgumentException("isoCountryName no es un ISO 3166-1 valido");
            }
            switch (isoCountryName)
            {
                case "AR":
                    return "-35,-64,2000km";
            }
            return string.Empty;
        }
    }
}
