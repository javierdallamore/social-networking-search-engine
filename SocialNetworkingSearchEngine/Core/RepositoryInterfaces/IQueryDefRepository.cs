using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Domain;

namespace Core.RepositoryInterfaces
{
    public interface IQueryDefRepository:IRepositoryBase<QueryDef, Guid>
    {
        IEnumerable<QueryDef> GetActiveQuerys();
    }
}
