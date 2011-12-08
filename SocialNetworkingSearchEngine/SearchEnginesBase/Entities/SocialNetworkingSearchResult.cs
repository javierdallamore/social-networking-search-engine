using System.Collections.Generic;

namespace SearchEnginesBase.Entities
{
    public class SocialNetworkingSearchResult
    {
        public SocialNetworkingSearchResult()
        {
            SocialNetworkingItems = new List<SocialNetworkingItem>();
        }

        public virtual string SocialNetworkingName { get; set; }
        public virtual List<SocialNetworkingItem> SocialNetworkingItems { get; set; }
    }
}
