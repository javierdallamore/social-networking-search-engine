using Core.Domain;
using FluentNHibernate.Mapping;

namespace DataAccess.Mapping
{
    public class WordMap : ClassMap<Word>
    {
        public WordMap()
        {
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.Name).Not.Nullable();
            Map(x => x.Sentiment).Not.Nullable();
        }
    }
}