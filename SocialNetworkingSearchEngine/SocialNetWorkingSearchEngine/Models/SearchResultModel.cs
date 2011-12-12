using System.Collections.Generic;
using Core.Domain;

namespace SocialNetWorkingSearchEngine.Models
{
    public class SearchResultModel
    {
        public List<Post> Items { get; private set; }
        public List<StatBox> StatBoxs { get; private set; }
        public string OrderBy { get; set; }

        public SearchResultModel()
        {
            Items = new List<Post>();
            StatBoxs = new List<StatBox>();
        }
    }

    public class StatBox
    {
        public StatBox()
        {
            StatItems = new List<StatItem>();
        }

        public string Title { get; set; }
        public List<StatItem> StatItems { get; private set; }
    }

    public class StatItem
    {
        public string Title { get; set; }
        public int Value { get; set; }
        public decimal ValuePercent { get; set; }
        public string ValueText { get; set; }
        public string Link { get; set; }
    }
}