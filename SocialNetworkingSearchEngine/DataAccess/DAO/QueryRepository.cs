using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Domain;
using Core.RepositoryInterfaces;

namespace DataAccess.DAO
{
    public class QueryRepository : RepositoryBase<Query, Guid>, IQueryRepository
    {
        #region Implementation of IQueryRepository

        public IEnumerable<Query> GetTopActiveQuerys(int top)
        {
            return Session.QueryOver<Query>().Where(x=>x.Active).Take(top).List();
        }

        #endregion
    }
}
