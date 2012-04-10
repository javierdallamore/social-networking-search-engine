using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessRules;
using Core.Domain;

namespace QueryExcecutionEngine
{
    public class DBQueryExecutionImpl: IExcecutionEngineService
    {
        //private readonly string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SearchEngineMockDB"].ConnectionString;
        private List<QueryDef> _querysToSearch;
        private IServicesManager _serviceManager;
        private SearchEngineManager _searchEngineManager;

        #region Implementation of IExcecutionEngineService

        public void Initialize()
        {
            _serviceManager = new ServicesManager();
            _querysToSearch = _serviceManager.GetTopActiveQuerys(10);
            _searchEngineManager = new SearchEngineManager();
            
            SearchEngineManager.ConfigureAddins();
        }

        public void Start()
        {
            try
            {
                foreach (var query in _querysToSearch)
                {
                    var posts = _searchEngineManager.Search(query.Query, query.SearchEnginesNamesList);
                    foreach (var post in posts)
                    {
                        _serviceManager.SavePost(post);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Stop()
        {
            
        }

        public void Pause()
        {
            
        }

        public void Resume()
        {
            
        }

        #endregion
    }
}
