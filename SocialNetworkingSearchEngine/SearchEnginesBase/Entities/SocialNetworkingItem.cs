using System;

namespace SearchEnginesBase.Entities
{
    public class SocialNetworkingItem
    {
        public SocialNetworkingItem()
        {
            
        }

        public virtual Guid Id { get; set; }
        public virtual string Content { get; set; }
        public virtual DateTime Date { get; set; }
    }
}