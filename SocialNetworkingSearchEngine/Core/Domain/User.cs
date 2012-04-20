using System.Collections.Generic;

namespace Core.Domain
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string Login { get; set; }
        public virtual string Name { get; set; }     
        public virtual bool IsAdmin { get; set; }     
        public virtual string HashedPass { get; set; }
  
    #region Not Mapped Properties

        public virtual IEnumerable<Post> AssignedPosts { get; set; }

    #endregion
    }
}