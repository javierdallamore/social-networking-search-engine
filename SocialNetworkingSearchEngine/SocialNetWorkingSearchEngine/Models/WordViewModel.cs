using System.Collections.Generic;
using Core.Domain;

namespace SocialNetWorkingSearchEngine.Models
{
    public class WordViewModel
    {
        public IEnumerable<Word> List { get; set; }
        public int Page { get; set; }
        public string Filter { get; set; }
    }
}