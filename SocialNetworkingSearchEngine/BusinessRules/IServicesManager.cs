using System;
using System.Collections.Generic;
using Core.Domain;

namespace BusinessRules
{
    public interface IServicesManager
    {
        Profile SaveProfile(Profile profile);
        Post SavePost(Post post);
        Tag SaveTag(Tag tag);

        Post TagPost(Post post, Guid tagId);
        Post TagPost(Post post, string tagName);

        List<Tag> GetAllTags();
        List<Profile> GetAllProfiles();
        List<Post> GetAllEntities();               
        List<Post> GetAllEntitiesByTag(string tagName);
        List<Post> GetAllEntitiesByTag(Guid tagId);
        List<Post> GetAllEntitiesByProfile(Guid profileId);
        List<Word> GetAllWords();
        List<Query> GetTopActiveQuerys(int top);

        void SendMail(string to, string address, string displayName, string subject, string body, string userName, string password, int port, string host);
        void SendPostToMail(string to, string address, string displayName, string subject, string userName, string password, int port, string host, Post post);
    }
}