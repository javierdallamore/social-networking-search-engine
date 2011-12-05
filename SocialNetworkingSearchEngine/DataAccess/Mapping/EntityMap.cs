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
            HasManyToMany(x => x.Tags).AsBag().Cascade.All();
            References(x => x.Profile).Cascade.SaveUpdate();
        }
    }
}