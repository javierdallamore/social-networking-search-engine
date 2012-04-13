using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain
{
    public class Tag
    {
        public Tag()
        {
            Id = Guid.NewGuid();
        }

        public virtual Guid Id { get; set; }
        [Required]
        [DisplayName("Tag")]
        public virtual string Name { get; set; }
    }
}
