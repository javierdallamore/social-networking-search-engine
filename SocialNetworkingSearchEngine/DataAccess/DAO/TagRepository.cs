using System;
using Core.Domain;
using Core.RepositoryInterfaces;

namespace DataAccess.DAO
{
    public class TagRepository : RepositoryBase<Tag, Guid>, ITagRepository
    {
        public Tag GetByName(string tagName)
        {
            return Session.QueryOver<Tag>().Where(x => x.Name == tagName).SingleOrDefault<Tag>();
        }
    }
}
