using System;
using System.Collections.Generic;
using System.Linq;
using Core.Domain;
using Core.RepositoryInterfaces;
using DataAccess;
using DataAccess.DAO;

namespace BusinessRules
{
    public class ServicesManager : IServicesManager
    {
        private readonly IProfileRepository _profileRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IPostRepository _entityRepository;

        public ServicesManager()
        {
            _profileRepository = new ProfileRepository();
            _tagRepository = new TagRepository();
            _entityRepository = new PostRepository();
        }

        public Profile SaveProfile(Profile profile)
        {
            bool begin = NHSessionManager.Instance.BeginTransaction();
            try
            {
                profile = _profileRepository.SaveOrUpdate(profile);
                if (begin)
                    NHSessionManager.Instance.CommitTransaction();
                return profile;
            }
            catch (Exception)
            {
                if (begin)
                    NHSessionManager.Instance.RollbackTransaction();
                return null;
            }
        }

        public Post SaveEntity(Post entity)
        {
            bool begin = NHSessionManager.Instance.BeginTransaction();
            try
            {
                entity = _entityRepository.SaveOrUpdate(entity);
                if (begin)
                    NHSessionManager.Instance.CommitTransaction();
                return entity;
            }
            catch (Exception)
            {
                if (begin)
                    NHSessionManager.Instance.RollbackTransaction();
                return null;
            }
        }

        public Tag SaveTag(Tag tag)
        {
            bool begin = NHSessionManager.Instance.BeginTransaction();
            try
            {
                tag = _tagRepository.SaveOrUpdate(tag);
                if (begin)
                    NHSessionManager.Instance.CommitTransaction();
                return tag;
            }
            catch (Exception)
            {
                if (begin)
                    NHSessionManager.Instance.RollbackTransaction();
                return null;
            }
        }

        public Post TagEntity(Post entity, Guid tagId)
        {
            var tag = _tagRepository.GetById(tagId);
            return TagEntity(entity, tag);
        }

        public Post TagEntity(Post entity, string tagName)
        {
            var tag = _tagRepository.GetByName(tagName) ?? new Tag() { Name = tagName };
            return TagEntity(entity, tag);
        }

        private Post TagEntity(Post entity, Tag tag)
        {
            bool begin = NHSessionManager.Instance.BeginTransaction();
            try
            {
                if (tag != null && !entity.Tags.Any(x => x.Id == tag.Id))
                {
                    entity.Tags.Add(tag);
                    _entityRepository.SaveOrUpdate(entity);
                }

                if (begin)
                    NHSessionManager.Instance.CommitTransaction();
                return entity;
            }
            catch (Exception)
            {
                if (begin)
                    NHSessionManager.Instance.RollbackTransaction();
                return null;
            }
        }

        public List<Post> GetAllEntities()
        {
            return _entityRepository.GetAll();
        }

        public List<Tag> GetAllTags()
        {
            return _tagRepository.GetAll();
        }

        public List<Profile> GetAllProfiles()
        {
            return _profileRepository.GetAll();
        }

        public List<Post> GetAllEntitiesByTag(string tagName)
        {
            return _entityRepository.GetAllByTagName(tagName);
        }

        public List<Post> GetAllEntitiesByTag(Guid tagId)
        {
            return _entityRepository.GetAllByTagNameId(tagId);
        }

        public List<Post> GetAllEntitiesByProfile(Guid profileId)
        {
            return _entityRepository.GetAllByProfile(profileId);
        }

        public void SendMail(string to, string address, string displayName, string subject, string body, string userName, string password, int port, string host)
        {
            Utils.SendMail(to, address, displayName, subject, body, userName, password, port, host);
        }
    }
}