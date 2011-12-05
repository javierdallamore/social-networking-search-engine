using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Domain;

namespace Core.RepositoryInterfaces
{
    public interface ITagRepository : IRepositoryBase<Tag, Guid>
    {
        Tag GetByName(string tagName);
    }
}
