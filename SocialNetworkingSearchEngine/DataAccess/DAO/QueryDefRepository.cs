using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Domain;
using Core.RepositoryInterfaces;

namespace DataAccess.DAO
{
    public class QueryDefRepository : RepositoryBase<QueryDef, Guid>, IQueryDefRepository
    {
        #region Implementation of IQueryDefRepository

        public IEnumerable<QueryDef> GetActiveQuerys()
        {
            return Session.QueryOver<QueryDef>().Where(x=>x.Enabled).List();
        }

        #endregion
    }
}
