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
        private readonly IPostRepository _postRepository;
        private readonly IWordRepository _wordRepository;

        public ServicesManager()
        {
            _profileRepository = new ProfileRepository();
            _tagRepository = new TagRepository();
            _postRepository = new PostRepository();
            _wordRepository = new WordRepository();
        }

        #region IServicesManager Members

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

        public Post SavePost(Post post)
        {
            bool begin = NHSessionManager.Instance.BeginTransaction();
            try
            {
                SetTags(post);
                post = _postRepository.SaveOrUpdate(post);
                if (begin)
                    NHSessionManager.Instance.CommitTransaction();
                return post;
            }
            catch (Exception)
            {
                if (begin)
                    NHSessionManager.Instance.RollbackTransaction();
                return null;
            }
        }

        private void SetTags(Post post)
        {
            post.Tags = new List<Tag>();
            if (!String.IsNullOrEmpty(post.CurrentTags))
            {
                var values = post.CurrentTags.Split(',');
                foreach (var tagName in values)
                {
                    if(!String.IsNullOrEmpty(tagName) && String.Compare(tagName,"null")!=0)
                    {
                        var tag = new TagRepository().GetByName(tagName);
                        post.Tags.Add(tag ?? new Tag() { Name = tagName });
                    }
                }
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

        public Post TagPost(Post post, Guid tagId)
        {
            var tag = _tagRepository.GetById(tagId);
            return TagPost(post, tag);
        }

        public Post TagPost(Post post, string tagName)
        {
            var tag = _tagRepository.GetByName(tagName) ?? new Tag() { Name = tagName };
            return TagPost(post, tag);
        }

        private Post TagPost(Post post, Tag tag)
        {
            bool begin = NHSessionManager.Instance.BeginTransaction();
            try
            {
                if (tag != null && !post.Tags.Any(x => x.Id == tag.Id))
                {
                    post.Tags.Add(tag);
                    _postRepository.SaveOrUpdate(post);
                }

                if (begin)
                    NHSessionManager.Instance.CommitTransaction();
                return post;
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
            return _postRepository.GetAll();
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
            return _postRepository.GetAllByTagName(tagName);
        }

        public List<Post> GetAllEntitiesByTag(Guid tagId)
        {
            return _postRepository.GetAllByTagNameId(tagId);
        }

        public List<Post> GetAllEntitiesByProfile(Guid profileId)
        {
            return _postRepository.GetAllByProfile(profileId);
        }

        public void SendMail(string to, string address, string displayName, string subject, string body, string userName, string password, int port, string host)
        {
            Utils.SendMail(to, address, displayName, subject, body, userName, password, port, host);
        }

        public void SendPostToMail(string to, string address, string displayName, string subject, string userName, string password, int port, string host, Post post)
        {
            var body = Utils.GenerateBodyHtml(post);
            Utils.SendMail(to, address, displayName, subject, body, userName, password, port, host);
        }       

        public List<Word> GetAllWords()
        {
            return _wordRepository.GetAll();
        }

        #endregion
    }
}