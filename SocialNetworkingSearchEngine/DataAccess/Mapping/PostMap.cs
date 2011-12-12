using Core.Domain;
using FluentNHibernate.Mapping;

namespace DataAccess.Mapping
{
    public class EntityMap : ClassMap<Post>
    {
        public EntityMap()
        {
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.SocialNetworkName, "SOCIAL_NET_NAME").Length(50);
            Map(x => x.Sentiment, "SENTIMENT").Length(50);
            Map(x => x.UserName, "USER_NAME").Length(50);
            Map(x => x.ProfileImage, "PROFILE_IMAGE").Length(100);
            Map(x => x.CreatedAt, "CREATE_AT").Length(50);
            Map(x => x.Content, "CONTENT").Length(50);
            Map(x => x.UrlPost, "URLPOST").Length(100);
            Map(x => x.UrlProfile, "URLPROFILE").Length(100);
            Map(x => x.Source, "SOURCE").Length(50);
            
            HasManyToMany(x => x.Tags).AsBag().Cascade.All();
            References(x => x.Profile).Cascade.SaveUpdate();
        }
    }
}