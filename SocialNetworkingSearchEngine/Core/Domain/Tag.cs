using System;

namespace Core.Domain
{
    public class Tag
    {
        public Tag()
        {
            
        }

        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
    }
}
