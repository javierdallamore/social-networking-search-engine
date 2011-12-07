using System;
using FluentNHibernate.Mapping;
using SearchEnginesBase.Entities;

namespace SearchEngineMock.Mapping
{

    public class SocialNetworkingItemMap : ClassMap<SocialNetworkingItem>
    {
        public SocialNetworkingItemMap()
        {
            Id(x => x.Id).GeneratedBy.Assigned();
            Map(x => x.Content);
            Map(x => x.UserName);
            Map(x => x.ProfileImage);
            Map(x => x.StatusDate);
        }
    }
}