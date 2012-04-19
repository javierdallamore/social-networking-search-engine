using System;
using System.Collections.Generic;
using Core.Domain;
using Core.RepositoryInterfaces;
using NHibernate.Criterion;

namespace DataAccess.DAO
{
    public class QueryDefRepository : RepositoryBase<QueryDef, Guid>, IQueryDefRepository
    {
        #region Implementation of IQueryDefRepository

        public IEnumerable<QueryDef> GetActiveQuerys()
        {
            return Session.QueryOver<QueryDef>().Where(x=>x.Enabled).List();
        }

        public IEnumerable<QueryDef> GetActiveQuerysWithMinQuequeLenghtViolated()
        {
            //TODO Buscar en las enabled y las que tengas una cantidad de post menor a MinQuequeLenght
            return Session.QueryOver<QueryDef>().Where(x=>x.Enabled).List();
        }

        #endregion
    }
}
