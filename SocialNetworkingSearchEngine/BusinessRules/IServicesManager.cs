using System;
using System.Collections.Generic;
using Core.Domain;
using SearchEnginesBase.Entities;

namespace BusinessRules
{
    public interface IServicesManager
    {
        Profile SaveProfile(Profile profile);
        Entity SaveEntity(Entity entity);
        Tag SaveTag(Tag tag);

        Entity TagEntity(Entity entity, Guid tagId);
        Entity TagEntity(Entity entity, string tagName);

        List<Entity> GetAllEntities();
        List<Tag> GetAllTags();
        List<Profile> GetAllProfiles();
        List<Entity> GetAllEntitiesByTag(string tagName);
        List<Entity> GetAllEntitiesByTag(Guid tagId);
        List<Entity> GetAllEntitiesByProfile(Guid profileId);
    }
}