using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using SearchEnginesBase.Entities;
using SearchEnginesBase.Interfaces;

namespace BusinessRules
{
    public class SearchEngineManager : ISearchEngineManager
    {
        private static List<SearchEngineConfiguration> _searchEngines;

        public List<SocialNetworkingSearchResult> Search(string searchParameters, List<string> searchEnginesName)
        {
            var socialNetworkingSearchResults = new List<SocialNetworkingSearchResult>();

            foreach (var searchEngine in _searchEngines.Where(x => searchEnginesName.Any(y=>y ==x.Name)))
            {
                try
                {
                    if (searchEngine.Instance != null)
                    {
                        var socialNetworkingSearchResult = searchEngine.Instance.Search(searchParameters);
                        if (socialNetworkingSearchResult != null)
                            socialNetworkingSearchResults.Add(socialNetworkingSearchResult);
                    }
                }
                catch (Exception ex)
                {
                    //LogError(ex);
                }
            }

            return socialNetworkingSearchResults;
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
