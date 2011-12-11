using System.Collections.Generic;
using System.Linq;
using SearchEnginesBase.Entities;
using SocialNetWorkingSearchEngine.Models;

namespace SocialNetWorkingSearchEngine
{
    public class SearchEngineBoxBuilder
    {
        public class Engine
        {
            public int Counter
            {
                get; set; }

            public string Name { get; set; }
        }

        public SearchEngineBoxBuilder()
        {
            _engines = new Dictionary<string, Engine>();
        }

        private Dictionary<string,Engine> _engines;

        public List<Engine> Engines { get { return _engines.Select(o=>o.Value).ToList(); } }

        public void BuildBox(SearchResultModel model)
        {
            foreach (var item in model.Items)
            {
                ProcessItem(item);
            }
            var box = new StatBox() {Title = "Red Social"};
            model.StatBoxs.Add(box);
            foreach (var engine in Engines)
            {
                var red = new StatItem() {Title = engine.Name, Value = engine.Counter, Link = "#"};
                box.StatItems.Add(red);
                if (model.Items.Count > 0)
                {
                    red.ValueText = ((decimal) engine.Counter/model.Items.Count*100).ToString("f") + "%";
                }
                else
                {
                    red.ValueText = "0%";
                }
            }
        }

        public void ProcessItem(SocialNetworkingItem item)
        {
            if (string.IsNullOrWhiteSpace(item.SocialNetworkName)) return;
            if(!_engines.ContainsKey(item.SocialNetworkName))
            {
                _engines.Add(item.SocialNetworkName,new Engine(){Name = item.SocialNetworkName});
            }
            _engines[item.SocialNetworkName].Counter++;
        }
    }
}