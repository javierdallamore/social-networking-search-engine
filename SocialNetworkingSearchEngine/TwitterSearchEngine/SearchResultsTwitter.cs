using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TwitterSearchEngine
{
    [DataContract]
    public class SearchResultsTwitter
    {
        public SearchResultsTwitter()
        {
            Results = new List<ItemSearchResultTwitter>();
        }

        [DataMember(Name = "results")]
        public List<ItemSearchResultTwitter> Results { get; set; }
    }
}
