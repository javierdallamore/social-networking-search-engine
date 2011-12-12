using System;
using Core.Domain;

namespace Core.RepositoryInterfaces
{
    public interface ITagRepository : IRepositoryBase<Tag, Guid>
    {
        Tag GetByName(string tagName);
    }
}
