using Core.Domain;
using FluentNHibernate.Mapping;

namespace DataAccess.Mapping
{
    public class PostTagMap : ClassMap<PostTag>
    {
        public PostTagMap()
        {
            Id(x => x.Id).GeneratedBy.Native();
            
            References(x => x.Post);
            References(x => x.Tag).Cascade.All();
            References(x => x.User);
        }
    }
}