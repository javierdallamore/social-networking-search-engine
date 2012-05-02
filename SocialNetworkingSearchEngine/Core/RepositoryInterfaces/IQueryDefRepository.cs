using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Domain;

namespace Core.RepositoryInterfaces
{
    public interface IQueryDefRepository:IRepositoryBase<QueryDef, int>
    {
        IEnumerable<QueryDef> GetActiveQuerys();
        IEnumerable<QueryDef> GetActiveQuerysWithMinQuequeLenghtViolated();
    }
}
