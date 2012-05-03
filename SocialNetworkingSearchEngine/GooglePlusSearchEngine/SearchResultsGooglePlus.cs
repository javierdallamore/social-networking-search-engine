using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GooglePlusSearchEngine
{
    [DataContract]
    public class SearchResultsGooglePlus
    {
        public SearchResultsGooglePlus()
        {
            Results = new List<ItemSearchResultGooglePlus>();
        }

        [DataMember(Name = "items")]
        public List<ItemSearchResultGooglePlus> Results { get; set; }
        [DataMember(Name = "nextPageToken")]
        public string NextPageToken { set; get; }
    }
}
