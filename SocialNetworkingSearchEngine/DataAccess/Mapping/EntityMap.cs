using System;
using Core.Domain;
using FluentNHibernate.Mapping;

namespace DataAccess.Mapping
{

    public class EntityMap : ClassMap<Entity>
    {
        public EntityMap()
        {
            Id(x => x.Id).GeneratedBy.Assigned();
        }
    }
}