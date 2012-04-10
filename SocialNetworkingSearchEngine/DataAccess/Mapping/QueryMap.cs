using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Domain;
using FluentNHibernate.Mapping;

namespace DataAccess.Mapping
{
    public class QueryMap:ClassMap<Query>
    {
        public QueryMap()
        {
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.QueryString);
            Map(x => x.Active);
            Map(x => x.LastExecutionDate);
            Map(x => x.SearchEnginesNames);
        }
    }
}
