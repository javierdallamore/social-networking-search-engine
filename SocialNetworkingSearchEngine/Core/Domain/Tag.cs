using System;

namespace Core.Domain
{
    public class Tag
    {
        public Tag()
        {
            Id = Guid.NewGuid();
        }

        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
    }
}
