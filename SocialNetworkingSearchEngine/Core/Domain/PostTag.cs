using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Domain
{
    public class PostTag
    {
        public virtual int Id { get; set; }
        public virtual Post Post { get; set; }
        public virtual Tag Tag { get; set; }
        public virtual User User { get; set; }
    }
}
