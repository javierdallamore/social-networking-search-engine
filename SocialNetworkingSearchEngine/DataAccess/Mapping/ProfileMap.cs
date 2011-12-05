using System;
using Core.Domain;
using FluentNHibernate.Mapping;

namespace DataAccess.Mapping
{

    public class ProfileMap : ClassMap<Profile>
    {
        public ProfileMap()
        {
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.Name).Not.Nullable();
        }
    }
}