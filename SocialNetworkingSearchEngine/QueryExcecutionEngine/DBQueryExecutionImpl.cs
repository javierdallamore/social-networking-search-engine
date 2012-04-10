using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessRules;
using Core.Domain;
using log4net;

namespace QueryExcecutionEngine
{
    public class DBQueryExecutionImpl: IExcecutionEngineService
    {
        private ILog _logger;

        private List<QueryDef> _querysToSearch;
        private IServicesManager _serviceManager;
        private SearchEngineManager _searchEngineManager;

        #region Implementation of IExcecutionEngineService

        public void Initialize()
        {
            log4net.Config.XmlConfigurator.Configure(); 

            _logger = LogManager.GetLogger(GetType());
            _logger.Info("====== Initializing Service ======");
            _serviceManager = new ServicesManager();
            _querysToSearch = _serviceManager.GetTopActiveQuerys(10);
            _searchEngineManager = new SearchEngineManager();
            
            SearchEngineManager.ConfigureAddins();
            _logger.Info("====== Service Initialized ======");
        }

        public void Start()
        {
            try
            {
                _logger.Info("\n\n====== Service Running... ======\n\n");
                foreach (var query in _querysToSearch)
                {
                    var posts = _searchEngineManager.Search(query.Query, query.SearchEnginesNamesList);
                    foreach (var post in posts)
                    {
                        _serviceManager.SavePost(post);
                    }

                    _logger.Info("\n" + posts.Count + " Posts Found \n");
                }
                _logger.Info("\n\n====== Service Finish Work ======\n\n");
            }
            catch (Exception e)
            {
                _logger.Error(e);
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
