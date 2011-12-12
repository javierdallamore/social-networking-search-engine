using System;
using Core.Domain;
using Core.RepositoryInterfaces;

namespace DataAccess.DAO
{
    public class WordRepository : RepositoryBase<Word, Guid>, IWordRepository
    {
    }
}