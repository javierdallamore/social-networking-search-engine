using System;
using System.Collections.Generic;

namespace Core.Domain
{
    public class Entity
    {
        public Entity()
        {
            Tags = new List<Tag>();
        }

        public virtual Guid Id { get; set; }
        public virtual List<Tag> Tags { get; set; }
        public virtual Profile Profile { get; set; }
    }
}
