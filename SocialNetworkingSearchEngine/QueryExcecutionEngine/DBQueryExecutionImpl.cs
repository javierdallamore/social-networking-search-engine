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
        private SentimentValuator _sentimentValuator;

        #region Implementation of IExcecutionEngineService

        public void Initialize()
        {
            log4net.Config.XmlConfigurator.Configure(); 

            _logger = LogManager.GetLogger(GetType());
            _logger.Info("====== Initializing Service ======\n\n");
            _serviceManager = new ServicesManager();
            _querysToSearch = _serviceManager.GetTopActiveQuerys(10);
            _searchEngineManager = new SearchEngineManager();

            _logger.Info("\n\n====== SETTING UP SENTIMENT EVALUATOR ======\n\n");
            var negativeWords = _serviceManager.GetAllWords().Where(x => x.Sentiment == "Negativo").Select(x => x.Name).ToList();
            var positiveWords = _serviceManager.GetAllWords().Where(x => x.Sentiment == "Positivo").Select(x => x.Name).ToList();
            var ignoreList = new List<string>() { ".", "," };
            _sentimentValuator = new SentimentValuator
            {
                NegativeWords = negativeWords,
                PositiveWords = positiveWords,
                IgnoreChars = ignoreList
            };
            _logger.Info("\n\n====== SENTIMENT EVALUATOR SET UP SUCCESSFULLY ======\n\n");

            SearchEngineManager.ConfigureAddins();
            _logger.Info("\n\n====== Service Initialized ======\n\n");
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
                        _sentimentValuator.ProcessItem(post);
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
