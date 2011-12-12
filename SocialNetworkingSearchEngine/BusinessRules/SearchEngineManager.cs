using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Core.Domain;
using DataAccess.DAO;
using SearchEnginesBase.Entities;
using SearchEnginesBase.Interfaces;

namespace BusinessRules
{
    public class SearchEngineManager : ISearchEngineManager
    {
        private static List<SearchEngineConfiguration> _searchEngines;

        public List<Post> Search(string searchParameters, List<string> searchEnginesName)
        {
            var results = new List<Post>();

            // Si indica que busque post guardados, primero agrego esos posts.
            if (searchEnginesName.Any(y => y == "SavedPosts"))
                results.AddRange(SearchSavedPosts(searchParameters));

            foreach (var searchEngine in _searchEngines.Where(x => searchEnginesName.Any(y=>y ==x.Name)))
            {
                try
                {
                    if (searchEngine.Instance != null)
                    {
                        var socialNetworkingSearchResult = searchEngine.Instance.Search(searchParameters, 1);
                        if (socialNetworkingSearchResult != null)
                        {
                            results.AddRange(ConvertFromSocialNetworkingItemToPosts(socialNetworkingSearchResult.SocialNetworkingItems));
                        }
                    }
                }
                catch (Exception ex)
                {
                    //LogError(ex);
                }
            }

            return results;
        }

        private IEnumerable<Post> SearchSavedPosts(string searchParameters)
        {
            var postRepository = new PostRepository();
            return postRepository.GetByQuery(searchParameters);
        }

        private List<Post> ConvertFromSocialNetworkingItemToPosts(List<SocialNetworkingItem> entityList)
        {
            List<Post> posts = (from u in entityList
                                    select new Post
                                                {
                                                    UserName = u.UserName,
                                                    ProfileImage = u.ProfileImage,
                                                    Content = u.Content,
                                                    UrlPost = u.UrlPost,
                                                    UrlProfile = u.UrlProfile,
                                                    CreatedAt = u.CreatedAt,
                                                    Source = u.Source
                                                }).ToList();
            return posts;
        }

        public static void ConfigureAddins()
        {
            _searchEngines = new List<SearchEngineConfiguration>();
            var dsConfig = new System.Data.DataSet();
            var path = AppDomain.CurrentDomain.BaseDirectory + "\\SearchEngines.config";
            if(!File.Exists(path))
                path = AppDomain.CurrentDomain.BaseDirectory + "\\bin\\SearchEngines.config";

            dsConfig.ReadXml(path);
            foreach (DataRow row in dsConfig.Tables["searchEngine"].Rows)
            {
                try
                {
                    var engineConfiguration = new SearchEngineConfiguration
                                                  {
                                                      DisplayName = row["displayName"].ToString(),
                                                      Name = row["name"].ToString()
                                                  };
                    var engineType = Type.GetType(row["type"].ToString());
                    if (!typeof(ISearchEngine).IsAssignableFrom(engineType))
                    {
                        continue;
                    }
                    engineConfiguration.Type = engineType;
                    engineConfiguration.Instance = engineType.GetConstructor(System.Type.EmptyTypes).Invoke(null) as ISearchEngine;
                    _searchEngines.Add(engineConfiguration);
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
        }
    }
}
