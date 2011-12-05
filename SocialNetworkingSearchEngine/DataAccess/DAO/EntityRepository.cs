using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Domain;
using Core.RepositoryInterfaces;

namespace DataAccess.DAO
{
    public class EntityRepository: RepositoryBase<Entity,Guid>, IEntityRepository
    {
        public List<Entity> GetAllByTagName(string tagName)
        {
            return Session.QueryOver<Entity>().Left.JoinQueryOver<Tag>(x => x.Tags).Where(x => x.Name == tagName).List<Entity>() as List<Entity>;
        }

        public List<Entity> GetAllByProfile(Guid profileId)
        {
            return Session.QueryOver<Entity>().Left.JoinQueryOver<Profile>(x => x.Profile).Where(x => x.Id == profileId).List<Entity>() as List<Entity>;
        }

        public List<Entity> GetAllByTagNameId(Guid tagId)
        {
            return Session.QueryOver<Entity>().Left.JoinQueryOver<Tag>(x => x.Tags).Where(x => x.Id == tagId).List<Entity>() as List<Entity>;
        }
    }
}
