using Core.Domain;
using FluentNHibernate.Mapping;

namespace DataAccess.Mapping
{
    public class PostMap : ClassMap<Post>
    {
        public PostMap()
        {
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.SocialNetworkName);
            Map(x => x.Sentiment);
            Map(x => x.UserName);
            Map(x => x.ProfileImage);
            Map(x => x.CreatedAt);
            Map(x => x.Content).Length(4000);
            Map(x => x.UrlPost);
            Map(x => x.UrlProfile);
            Map(x => x.Source);
            Map(x => x.Calification);
            Map(x => x.Query);
            
            HasManyToMany(x => x.Tags).AsBag().Cascade.All();
            References(x => x.Profile).Cascade.SaveUpdate();
        }
    }
}