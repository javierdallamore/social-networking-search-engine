using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Core.Domain;

namespace SocialNetWorkingSearchEngine.Models
{ 
    public class TagRepository : ITagRepository
    {
        SocialNetWorkingSearchEngineContext context = new SocialNetWorkingSearchEngineContext();

        public IQueryable<Tag> All
        {
            get { return context.Tags; }
        }

        public IQueryable<Tag> AllIncluding(params Expression<Func<Tag, object>>[] includeProperties)
        {
            IQueryable<Tag> query = context.Tags;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Tag Find(System.Guid id)
        {
            return context.Tags.Find(id);
        }

        public void InsertOrUpdate(Tag tag)
        {
            if (tag.Id == default(System.Guid)) {
                // New entity
                tag.Id = Guid.NewGuid();
                context.Tags.Add(tag);
            } else {
                // Existing entity
                context.Entry(tag).State = EntityState.Modified;
            }
        }

        public void Delete(System.Guid id)
        {
            var tag = context.Tags.Find(id);
            context.Tags.Remove(tag);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

    public interface ITagRepository
    {
        IQueryable<Tag> All { get; }
        IQueryable<Tag> AllIncluding(params Expression<Func<Tag, object>>[] includeProperties);
        Tag Find(System.Guid id);
        void InsertOrUpdate(Tag tag);
        void Delete(System.Guid id);
        void Save();
    }
}