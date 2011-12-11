using System;
using System.Collections.Generic;

namespace SearchEnginesBase.Entities
{
    public class SocialNetworkingItem
    {
        public SocialNetworkingItem()
        {
            Id = Guid.NewGuid();
            Tags = new List<string>();
        }

        public virtual string SocialNetworkName { get; set; }
        
        public virtual string SocialNetworkIconPath
        {
            get { return "http://t3.gstatic.com/images?q=tbn:ANd9GcSz_BGVSl9aqMCjDA9Qwh5dbyMBE1hl2HPCaqwG6Y7JzSxlsrWr8g"; }
        }

        public virtual string SentimentIconPath
        {
            get { return "http://socialmention.com/assets/img/icons/sentiment_positive.png"; }
        }

        public virtual string Sentiment { get; set; }

        public virtual Guid Id { get; set; }

        public virtual string UserName { get; set; }
        
        public virtual string ProfileImage { get; set; }
        
        public virtual DateTime CreatedAt { get; set; }
        
        public virtual string CreatedAtShort
        {
            get { return CreatedAt.ToLongDateString() + " " + CreatedAt.ToShortTimeString(); }
        }
        
        public virtual string Content { get; set; }
        
        public virtual string UrlPost { get; set; }
        
        public virtual string UrlProfile { get; set; }
        
        public virtual string Source { get; set; }

        public virtual List<string> Tags { get; set; }
    }
}