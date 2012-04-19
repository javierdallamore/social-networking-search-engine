using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Domain;
using FluentNHibernate.Mapping;

namespace DataAccess.Mapping
{
    public class QueryDefMap:ClassMap<QueryDef>
    {
        public QueryDefMap()
        {
            Id(x => x.Id).GeneratedBy.Native();
            Map(x => x.Query);
            Map(x => x.Enabled);
            Map(x => x.DaysOldestPost);
            Map(x => x.MinQueueLength);
            Map(x => x.SearchEnginesNames);

            HasMany(x => x.Posts);
        }
    }
}
