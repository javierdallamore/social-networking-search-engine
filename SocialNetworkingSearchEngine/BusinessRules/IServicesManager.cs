using System;
using System.Collections.Generic;
using Core.Domain;

namespace BusinessRules
{
    public interface IServicesManager
    {
        Profile SaveProfile(Profile profile);
        Post SaveEntity(Post entity);
        Tag SaveTag(Tag tag);

        Post TagEntity(Post entity, Guid tagId);
        Post TagEntity(Post entity, string tagName);

        List<Tag> GetAllTags();
        List<Profile> GetAllProfiles();
        List<Post> GetAllEntities();               
        List<Post> GetAllEntitiesByTag(string tagName);
        List<Post> GetAllEntitiesByTag(Guid tagId);
        List<Post> GetAllEntitiesByProfile(Guid profileId);

        void SendMail(string to, string address, string displayName, string subject, string body, string userName, string password, int port, string host);
    }
}