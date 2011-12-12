using System;
using System.Collections.Generic;

namespace Core.Domain
{
    public class Post
    {
        public Post()
        {
            Tags = new List<Tag>();
        }

        public virtual Guid Id { get; set; }
        public virtual List<Tag> Tags { get; set; }
        public virtual Profile Profile { get; set; }

        public virtual string SocialNetworkName { get; set; }
        public virtual string Sentiment { get; set; }
        public virtual string UserName { get; set; }
        public virtual string ProfileImage { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual string Content { get; set; }
        public virtual string UrlPost { get; set; }
        public virtual string UrlProfile { get; set; }
        public virtual string Source { get; set; }

        #region No Mapeado

        public virtual string CreatedAtShort
        {
            get { return CreatedAt.ToLongDateString() + " " + CreatedAt.ToShortTimeString(); }
        }

        #endregion
    }
}