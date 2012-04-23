using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Domain;
using FluentNHibernate.Mapping;

namespace DataAccess.Mapping
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id).GeneratedBy.Native();
            Map(x => x.Login).Not.Nullable();
            Map(x => x.Name);
            Map(x => x.IsAdmin);
            Map(x => x.HashedPass);

            HasMany(x => x.AssignedPosts).Inverse().KeyColumn("CurrentOwner_id").Cascade.All();
        }
    }
}