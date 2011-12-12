using Core.Domain;
using FluentNHibernate.Mapping;

namespace DataAccess.Mapping
{
    public class PostMap : ClassMap<Post>
    {
        public PostMap()
        {
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.SocialNetworkName, "SOCIAL_NET_NAME").Length(50);
            Map(x => x.Sentiment, "SENTIMENT").Length(50);
            Map(x => x.UserName, "USER_NAME").Length(50);
            Map(x => x.ProfileImage, "PROFILE_IMAGE").Length(100);
            Map(x => x.CreatedAt, "CreatedAt").Length(50);
            Map(x => x.Content, "Content").Length(50);
            Map(x => x.UrlPost, "UrlPost").Length(100);
            Map(x => x.UrlProfile, "UrlProfile").Length(100);
            Map(x => x.Source, "Source").Length(50);
            
            HasManyToMany(x => x.Tags).AsBag().Cascade.All();
            References(x => x.Profile).Cascade.SaveUpdate();
        }
    }
}