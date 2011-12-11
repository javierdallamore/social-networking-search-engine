using System.Linq;
using Core.Domain;
using DataAccess.DAO;
using SearchEnginesBase.Entities;
using SearchEnginesBase.Interfaces;
using System.Collections.Generic;

namespace BusinessRules
{
    public class PichersSearchEngine : ISearchEngine
    {
        public string Name
        {
            get { return "P&P search engine"; }
        }

        public SocialNetworkingSearchResult Search(string searchParameters, int page)
        {
            var dao = new EntityRepository();
            var entitiesList = dao.GetByQuery(searchParameters);
            var list = SocialNetworkingItemList(entitiesList);

            return new SocialNetworkingSearchResult() { SocialNetworkingItems = list, SocialNetworkingName = Name };
        }

        private List<SocialNetworkingItem> SocialNetworkingItemList(List<Entity> entityList)
        {
            List<SocialNetworkingItem> users = (from u in entityList
                                                select new SocialNetworkingItem
                                                {
                                                    UserName = u.UserName,
                                                    ProfileImage = u.ProfileImage,
                                                    Content = u.Content,
                                                    UrlPost = u.UrlPost,
                                                    UrlProfile = u.UrlProfile,
                                                    CreatedAt = u.CreatedAt,
                                                    Source = u.Source
                                                }).ToList();
            return users;
        }
    }
}