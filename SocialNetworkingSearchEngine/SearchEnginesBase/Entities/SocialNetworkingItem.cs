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
        public virtual string UserName { get; set; }
        public virtual string ProfileImage { get; set; }
        public virtual DateTime StatusDate { get; set; }
        public virtual string Content { get; set; }
        public virtual string UrlPost { get; set; }
        public virtual string UrlProfile { get; set; }
    }
}