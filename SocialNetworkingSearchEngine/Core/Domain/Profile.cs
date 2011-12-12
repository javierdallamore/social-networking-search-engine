using System;

namespace Core.Domain
{
    public class Profile
    {
        public Profile()
        {
            Id = Guid.NewGuid();
        }

        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
    }
}
