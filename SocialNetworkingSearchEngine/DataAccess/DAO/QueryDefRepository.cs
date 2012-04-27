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
            var query = "SELECT q.[Id] " +
                               ",q.[Query] " +
                               ",q.[Enabled] " +
                               ",q.[DaysOldestPost] " +
                               ",q.[MinQueueLength] " +
                               ",q.[SearchEnginesNames] " +
                           "FROM QueryDef q, Post p " +
                           "WHERE q.Enabled = 'true' " +
                               "and p.QueryDef_id = q.Id " +
                           "GROUP BY q.Id, q.Query, q.Enabled, q.DaysOldestPost, q.MinQueueLength, q.SearchEnginesNames " +
                           "HAVING COUNT (p.Id) < q.MinQueueLength";

            var resutl = Session.CreateSQLQuery(query).AddEntity(typeof(QueryDef)).List<QueryDef>();
            return resutl;
        }

        #endregion
    }
}
