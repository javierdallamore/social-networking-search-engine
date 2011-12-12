using System;
using System.Collections.Generic;
using Core.Domain;

namespace Core.RepositoryInterfaces
{
    public interface IPostRepository : IRepositoryBase<Post, Guid>
    {
        List<Post> GetAllByTagName(string tagName);
        List<Post> GetAllByProfile(Guid profileId);
        List<Post> GetAllByTagNameId(Guid tagId);
        List<Post> GetByQuery(string query);
    }
}
