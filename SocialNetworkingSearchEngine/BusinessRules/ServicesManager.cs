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
        private readonly IQueryDefRepository _queryDefRepository;

        public ServicesManager()
        {
            _profileRepository = new ProfileRepository();
            _tagRepository = new TagRepository();
            _postRepository = new PostRepository();
            _wordRepository = new WordRepository();
            _queryDefRepository = new QueryDefRepository();
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

        public Post UpdatePost(string idPost, int rating, string sentiment, List<string> tags, User user)
        {
            bool begin = NHSessionManager.Instance.BeginTransaction();
            try
            {
                var post = _postRepository.GetById(new Guid(idPost));
                if (post == null) return null;

                post.Sentiment = sentiment;
                post.Calification = rating;
                post.CurrentTags = string.Join(",", tags);
                SetTagToPost(post, user);

                var postSaved = _postRepository.SaveOrUpdate(post);
                if (begin)
                    NHSessionManager.Instance.CommitTransaction();
                return postSaved;
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

        private void SetTagToPost(Post post, User user)
        {
            if ((string.IsNullOrEmpty(post.CurrentTags))) return;
            var values = post.CurrentTags.Split(',');
            //TODO Improve this
            //Elimino tags que borrados
            var tagsToDelete = new List<PostTag>();
            foreach (var postTag in post.PostTags)
            {
                var delete = true;
                foreach (var value in values)
                {
                    if (string.CompareOrdinal(postTag.Tag.Name.ToLower(), value.ToLower())==0)
                    {
                        delete = false;
                    }
                }
                if (delete) tagsToDelete.Add(postTag);
            }
            foreach (var postTag in tagsToDelete)
            {
                post.PostTags.Remove(postTag);
            }


            foreach (var tagName in values)
            {
                //Agrego los tags nuevos
                if (string.IsNullOrEmpty(tagName) || string.CompareOrdinal(tagName, "null") == 0) continue;

                var postTagsAlreadyAssigned =
                    post.PostTags.Where(x => String.CompareOrdinal(x.Tag.Name.ToLower(), tagName.ToLower()) == 0);
                if (!postTagsAlreadyAssigned.Any())
                {
                    var tagSaved = _tagRepository.GetByName(tagName);
                    if (tagSaved == null)
                    {
                        tagSaved = new Tag { Name = tagName };
                    }
                    var postTag = new PostTag { Post = post, Tag = tagSaved, User = user };
                    post.PostTags.Add(postTag);
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

        public bool ExistPost(string postUrl)
        {
            return _postRepository.ExistPost(postUrl);
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

        public List<QueryDef> GetActiveQuerys()
        {
            return _queryDefRepository.GetActiveQuerys().ToList();
        }

        public List<QueryDef> GetActiveQuerysWithMinQuequeLenghtViolated()
        {
            return _queryDefRepository.GetActiveQuerysWithMinQuequeLenghtViolated().ToList();
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

        public IEnumerable<Post> GetUserAssignedPost(User user)
        {
            return _postRepository.GetByAssignedUser(user);
        }

        public IEnumerable<Post> GetNotProcessedUserAssignedPost(User user)
        {
            var posts = _postRepository.GetNotProcessedByAssignedUser(user);
            return posts;
        }

        public IEnumerable<Post> AssignPostToUser(int cant, User user)
        {
            var begin = NHSessionManager.Instance.BeginTransaction();
            try
            {
                var unAssignedPost = _postRepository.GetNotAssigned(cant).ToList();
                foreach (var post in unAssignedPost)
                {
                    post.CurrentOwner = user;
                }
                if (begin)
                    NHSessionManager.Instance.CommitTransaction();
                return unAssignedPost;
            }
            catch (Exception)
            {
                if (begin)
                    NHSessionManager.Instance.RollbackTransaction();
                return null;
            }
        }

        #endregion
    }
}