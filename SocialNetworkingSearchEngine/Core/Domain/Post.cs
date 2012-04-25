using System;
using System.Collections.Generic;

namespace Core.Domain
{
    public class Post
    {
        public Post()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
            PostTags = new List<PostTag>();
            Tags = new List<Tag>();
        }

        public virtual Guid Id { get; set; }
        
        //Por compatibilidad con lo anterior
        public virtual IList<Tag> Tags { get; set; }
        
        public virtual IList<PostTag> PostTags { get; set; }
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
        public virtual int Calification { get; set; }
        public virtual string Query { get; set; }
        public virtual QueryDef QueryDef { get; set; }

        public virtual User CurrentOwner { get; set; }
        public virtual DateTime DateTimeAssinedToOwner { get; set; }


        #region No Mapeado
        public virtual string CurrentTags { get; set; } 
        public virtual string CreatedAtShort
        {
            get { return CreatedAt.ToLongDateString() + " " + CreatedAt.ToShortTimeString(); }
        }

        public virtual string UrlImgSentiment { get; set; }
        public virtual string UrlImgNetwork { get; set; }

        #endregion
    }
}