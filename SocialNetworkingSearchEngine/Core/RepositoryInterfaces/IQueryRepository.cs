using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Domain;

namespace Core.RepositoryInterfaces
{
    public interface IQueryRepository:IRepositoryBase<Query, Guid>
    {
        IEnumerable<Query> GetTopActiveQuerys(int top);
    }
}
