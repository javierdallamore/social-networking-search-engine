using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
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

        private const int TIME_INTERVAL_TO_CHECK_FOR_NEW_QUERYS = 5*60*1000;

        #region Implementation of IExcecutionEngineService

        public void Initialize()
        {
            log4net.Config.XmlConfigurator.Configure(); 

            _logger = LogManager.GetLogger(GetType());
            _logger.Info("====== Initializing Service ======\n\n");
            _serviceManager = new ServicesManager();
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
                _logger.Info("\n\n====== SERVICE RUNNING ======\n\n");

                var timerToSearchForQuerys = new Timer(TIME_INTERVAL_TO_CHECK_FOR_NEW_QUERYS);
                timerToSearchForQuerys.Elapsed += ((sender, e) =>
                                                       {
                                                           _querysToSearch = _serviceManager.GetActiveQuerysWithMinQuequeLenghtViolated();
                                                           foreach (var queryDef in _querysToSearch)
                                                           {
                                                               StartSearch(queryDef);
                                                           }
                                                       });
                timerToSearchForQuerys.Start();
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
        }

        private void StartSearch(QueryDef query)
        {
            _logger.Info("\n\n====== START SEARCH " + DateTime.Now + " ======\n\n");

            //Por cada red social a la que apunta la query
            foreach (var searchEngineName in query.SearchEnginesNamesList)
            {
                _logger.Info("\n\n====== START SEARCH FOR (" + searchEngineName + ") " + DateTime.Now + " ======\n\n");

                var postsResult = _searchEngineManager.Search(query.Query, new List<string> { searchEngineName });
                foreach (var post in postsResult)
                {
                    if (_serviceManager.ExistPost(post.UrlPost)) continue;
                    _sentimentValuator.ProcessItem(post);
                    _serviceManager.SavePost(post);
                }
                _logger.Info("\n\n===" + postsResult.Count + " POSTs FOUND IN " + searchEngineName + "\n");
            }

            _logger.Info("\n\n====== END QUERY SEARCH " + DateTime.Now + " ======\n\n");
            _logger.Info("\n\n====== WAITING FOR ANOTHER TIME ELAPSE ======\n\n");
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
