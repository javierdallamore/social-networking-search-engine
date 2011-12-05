using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Domain;

namespace Core.RepositoryInterfaces
{
    public interface IProfileRepository : IRepositoryBase<Profile, Guid>
    {
    }
}
