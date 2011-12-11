using System;
using System.Collections.Generic;
using Core.Domain;

namespace Core.RepositoryInterfaces
{
    public interface IEntityRepository : IRepositoryBase<Entity, Guid>
    {
        List<Entity> GetAllByTagName(string tagName);
        List<Entity> GetAllByProfile(Guid profileId);
        List<Entity> GetAllByTagNameId(Guid tagId);
        List<Entity> GetByQuery(string query);
    }
}
