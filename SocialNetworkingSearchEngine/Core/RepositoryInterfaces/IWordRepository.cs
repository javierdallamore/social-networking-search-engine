using System;
using Core.Domain;

namespace Core.RepositoryInterfaces
{
    public interface IWordRepository : IRepositoryBase<Word, Guid>
    {
    }
}
