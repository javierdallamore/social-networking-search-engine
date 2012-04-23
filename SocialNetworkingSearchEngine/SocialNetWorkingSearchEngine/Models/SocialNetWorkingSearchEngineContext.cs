using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SocialNetWorkingSearchEngine.Models
{
    public class SocialNetWorkingSearchEngineContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<SocialNetWorkingSearchEngine.Models.SocialNetWorkingSearchEngineContext>());

        public DbSet<Core.Domain.Word> Words { get; set; }

        public DbSet<Core.Domain.Tag> Tags { get; set; }

        public DbSet<Core.Domain.User> Users { get; set; }
    }
}