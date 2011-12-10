using Core.Domain;
using FluentNHibernate.Mapping;

namespace DataAccess.Mapping
{
    public class TagMap : ClassMap<Tag>
    {
        public TagMap()
        {
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.Name).Not.Nullable();
        }
    }
}