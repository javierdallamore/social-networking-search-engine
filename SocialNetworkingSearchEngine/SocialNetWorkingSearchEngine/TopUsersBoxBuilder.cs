using System.Collections.Generic;
using System.Linq;
using Core.Domain;
using SocialNetWorkingSearchEngine.Models;

namespace SocialNetWorkingSearchEngine
{
    public class TopUsersBoxBuilder
    {
        public class Engine
        {
            public int Counter
            {
                get;
                set;
            }

            public string Name { get; set; }
        }

        public TopUsersBoxBuilder()
        {
            _engines = new Dictionary<string, Engine>();
        }

        private readonly Dictionary<string, Engine> _engines;

        public List<Engine> Engines { get { return _engines.Select(o => o.Value).ToList(); } }

        public string BoxTitle
        {
            get { return "Top Users"; }
        }

        public void BuildBox(SearchResultModel model)
        {
            foreach (var item in model.Items)
            {
                ProcessItem(item);
            }
            var box = new StatBox() { Title = BoxTitle };
            model.StatBoxs.Add(box);
            foreach (var engine in Engines.OrderByDescending(o=>o.Counter).Take(10))
            {
                var red = new StatItem() { Title = engine.Name, Value = engine.Counter, Link = "#" };
                box.StatItems.Add(red);
                if (model.Items.Count > 0)
                {
                    red.ValueText = ((decimal)engine.Counter / model.Items.Count * 100).ToString("f") + "%";
                    red.ValuePercent = ((decimal)engine.Counter / model.Items.Count * 100);
                }
                else
                {
                    red.ValueText = "0%";
                    red.ValuePercent = 0;
                }
            }
        }

        public void ProcessItem(Post item)
        {
            if (string.IsNullOrWhiteSpace(item.UserName)) return;
            if (!_engines.ContainsKey(item.UserName))
            {
                _engines.Add(item.UserName, new Engine() { Name = item.UserName });
            }
            _engines[item.UserName].Counter++;
        }
    }
}