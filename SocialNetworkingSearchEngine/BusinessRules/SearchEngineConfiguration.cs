using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SearchEnginesBase.Interfaces;

namespace BusinessRules
{
    public class SearchEngineConfiguration
    {
        public Type Type { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public ISearchEngine Instance { get; set; }
    }
}
