using System;

namespace SearchEnginesBase.Entities
{
    public class SocialNetworkingItem
    {
        public SocialNetworkingItem()
        {
            Id = Guid.NewGuid();
        }

        public virtual Guid Id { get; set; }
        public string UserName { get; set; }
        public string ProfileImage { get; set; }
        public DateTime StatusDate { get; set; }
        public virtual string Content { get; set; }                
    }
}