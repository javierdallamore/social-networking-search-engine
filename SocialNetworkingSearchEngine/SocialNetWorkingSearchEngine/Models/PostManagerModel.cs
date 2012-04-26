using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessRules;
using Core.Domain;

namespace SocialNetWorkingSearchEngine.Models
{
    public class PostManagerModel
    {
        public PostManagerModel()
        {
            var servicesManager = new ServicesManager();
            Tags = servicesManager.GetAllTags();
        }
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public IEnumerable<string> TagsStringsArray
        {
            get
            {
                var tags = Tags.Select(x => "'"+x.Name+"'").ToArray();
                return tags;
            }
        }

        public IEnumerable<Word> NegativeWords { get; set; }
        public IEnumerable<Word> PositiveWords { get; set; }
    }
}